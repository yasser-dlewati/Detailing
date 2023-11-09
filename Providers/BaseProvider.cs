using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using Detailing.Interfaces;
using Detailing.Models;

namespace Detailing.Providers
{
    public abstract class BaseProvider<T> : IModelProvider<T> where T : IModel
    {
        private readonly IDatabaseService _dbService;
        private readonly IDataMapper<T> _dataMapper;

        public abstract string SelectAllStoredProcedureName { get; }

        public abstract string SelectByIdStoredProcedureName { get; }

        public abstract string InsertStoredProcedureName { get; }

        public abstract string UpdateStoredProcedureName { get; }

        public abstract string DeleteByIdStoredProcedureName { get; }

        public BaseProvider(IDatabaseService dbService, IDataMapper<T> dataMapper)
        {
            _dbService = dbService;
            _dataMapper = dataMapper;
        }

        public virtual IEnumerable<T> GetAll()
        {
            var dt = _dbService.ExecuteQueryStoredProcedure(SelectAllStoredProcedureName);
            var models = new List<T>();
            for (var i = 0; i < dt.Rows.Count; i++)
            {
                var model = _dataMapper.MapToModel(dt.Rows[i]);
                models.Add(model);
            }
            return models;
        }

        public abstract IDbDataParameter[] GetIdDataParameter(int id);

        public virtual T GetById(int id)
        {
            try
            {
                var idParameter = GetIdDataParameter(id);
                var dt = _dbService.ExecuteQueryStoredProcedure(SelectByIdStoredProcedureName, idParameter);
                if (dt != null && dt.Rows != null && dt.Rows.Count == 1)
                {
                    var model = _dataMapper.MapToModel(dt.Rows[0]);
                    return model;
                }
            }
            catch (Exception)
            {

            }

            return default(T);
        }

        public virtual bool TryDelete(int id)
        {
            try
            {
                var idParameter = GetIdDataParameter(id);
                var rowsAffectd = _dbService.ExecuteNonQueryStoredProcedure(DeleteByIdStoredProcedureName, idParameter);
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

        public virtual bool TryInsert(T model, out int insertedId)
        {
            try
            {
                var spParameters = GetDbParameters(model);
                insertedId = _dbService.ExecuteNonQueryStoredProcedure(InsertStoredProcedureName, spParameters);
                return insertedId > 0;
            }
            catch (Exception ex)
            {
            }

            insertedId = 0;
            return false;
        }

        public virtual bool TryUpdate(T model)
        {
            try
            {
                var spParameters = GetDbParameters(model);
                var rowsAffectd = _dbService.ExecuteNonQueryStoredProcedure(UpdateStoredProcedureName, spParameters);
                return rowsAffectd == 1;
            }
            catch (Exception ex)
            {
            }

            return false;
        }
    }
}