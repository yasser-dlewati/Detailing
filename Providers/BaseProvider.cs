using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using Detailing.Interfaces;
using Detailing.Models;
using Microsoft.Extensions.Caching.Memory;

namespace Detailing.Providers
{
    public abstract class BaseProvider<T> : IModelProvider<T> where T : IModel
    {
        private readonly IDatabaseService _dbService;
        private readonly IDataMapper<T> _dataMapper;
        private readonly IMemoryCache _cache;
        public readonly string _cacheKey;

        public abstract string SelectAllStoredProcedureName { get; }

        public abstract string SelectByIdStoredProcedureName { get; }

        public abstract string InsertStoredProcedureName { get; }

        public abstract string UpdateStoredProcedureName { get; }

        public abstract string DeleteByIdStoredProcedureName { get; }

        public BaseProvider(IDatabaseService dbService, IDataMapper<T> dataMapper, IMemoryCache cache)
        {
            _dbService = dbService;
            _dataMapper = dataMapper;
            _cache = cache;
            _cacheKey = typeof(T).ToString();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            var dt = await _dbService.ExecuteQueryStoredProcedureAsync(SelectAllStoredProcedureName);
            var models = new List<T>();
            for (var i = 0; i < dt.Rows.Count; i++)
            {
                var model = _dataMapper.MapToModel(dt.Rows[i]);
                models.Add(model);
            }
            return models;
        }

        public abstract IDbDataParameter[] GetIdDataParameter(int id);

        public virtual async Task<T> GetByIdAsync(int id)
        {
            if(_cache.TryGetValue(_cacheKey[id], out T model))
            {
                Console.WriteLine($"Retreiving {_cacheKey[id]} data from cache.");
                return model;
            }

            var idParameter = GetIdDataParameter(id);
            var dt = await _dbService.ExecuteQueryStoredProcedureAsync(SelectByIdStoredProcedureName,idParameter);
            if (dt != null && dt.Rows != null && dt.Rows.Count == 1)
            {
                model = _dataMapper.MapToModel(dt.Rows[0]);
                _cache.Set(_cacheKey[id], model);
                return model;
            }

            return default(T);
        }

        public virtual async Task<bool> TryDeleteAsync(int id)
        {
            try
            {
                var idParameter = GetIdDataParameter(id);
                var rowsAffectd = await _dbService.ExecuteNonQueryStoredProcedureAsync(DeleteByIdStoredProcedureName, idParameter);
                _cache.Remove(_cacheKey);
                return rowsAffectd == 1;
            }
            catch (Exception)
            {
            }

            return false;
        }

        public virtual IDbDataParameter[] GetDbParameters(T data)
        {
            var properties = data.GetType().GetProperties();
            var dbParamerters = new IDbDataParameter[properties.Count()];
            for (var i = 0; i < properties.Count(); i++)
            {
                var prop = properties[i];
                var columnName = (prop.GetCustomAttributes(true).Where(a => a is ColumnAttribute).FirstOrDefault() as ColumnAttribute).Name;
                dbParamerters[i] = new DatabaseParameter(columnName, data.GetType().GetProperty(prop.Name).GetValue(data));
            }

            return dbParamerters;
        }

        public virtual bool TryInsert(ref T model)
        {
            try
            {
                var spParameters = GetDbParameters(model);
                var dt = _dbService.ExecuteQueryStoredProcedureAsync(InsertStoredProcedureName, spParameters).Result;
                if (dt != null && dt.Rows != null && dt.Rows.Count == 1)
                {
                    model = _dataMapper.MapToModel(dt.Rows[0]);
                    _cache.Remove(_cacheKey);
                    return true;
                }
            }
            catch (Exception ex)
            {
            }

            return false;
        }

        public virtual async Task<bool> TryUpdateAsync(T model)
        {
            try
            {
                var spParameters = GetDbParameters(model);
                var rowsAffectd = await _dbService.ExecuteNonQueryStoredProcedureAsync(UpdateStoredProcedureName, spParameters);
                _cache.Remove(_cacheKey);
                return rowsAffectd == 1;
            }
            catch (Exception ex)
            {
            }

            return false;
        }

        public async Task<IEnumerable<T>> GetAllCachedAsync()
        {
            if(_cache.TryGetValue(_cacheKey, out IEnumerable<T> cachedData))
            {
                Console.WriteLine($"Retrieving {_cacheKey} data form cache.");
                return cachedData;
            }

            var data = await GetAllAsync();
            _cache.Set(_cacheKey, data);
            return data;
        }
    }
}