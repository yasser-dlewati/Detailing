using System.Data;
using System.Data.Common;
using Detailing.Interfaces;
using Detailing.Models;

namespace Detailing.Services
{
    public abstract class RepositorySerivce<T> : IEntityRepository<T> where T : IModel
    {
        private readonly IDatabaseService _dbService;
        private readonly IDataMapper<T> _dataMapper;
        private DatabaseService dbService;

        protected abstract string SelectAllStoredProcedureName {get;}

        protected abstract string SelectByIdStoredProcedureName {get;}

        protected abstract string InsertStoredProcedureName {get;}

        protected abstract string UpdateStoredProcedureName {get;}

        protected abstract string DeleteByIdStoredProcedureName {get;}


        public RepositorySerivce(IDatabaseService dbService, IDataMapper<T> dataMapper)
        {
            _dbService = dbService;
            _dataMapper = dataMapper;
        }

        public IEnumerable<T> GetAll()
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

        public IModel GetById(int id)
        {
           try
            {
                if (id > 0)
                {
                    var spParameters = new IDbDataParameter[]
                    {
                        new DatabaseParameter("Id", id),
                    };

                    var dt = _dbService.ExecuteQueryStoredProcedure(SelectByIdStoredProcedureName, spParameters);
                    var model = _dataMapper.MapToModel(dt.Rows[0]);
                    return model;
                }
            }
            catch (Exception)
            {

            }

            return null;
        }

        public bool TryDelete(int id)
        {
            try
            {
                if (id > 0)
                {
                    var spParameters = new IDbDataParameter[]
                    {
                        new DatabaseParameter("Id", id),
                    };
                    var rowsAffectd = _dbService.ExecuteNonQueryStoredProcedure(DeleteByIdStoredProcedureName, spParameters);
                    return rowsAffectd == 1;
                }
            }
            catch (Exception)
            {
            }

            return false;
        }

        public abstract IDbDataParameter[] GetDbParameters(T data);

        public bool TryInsert(T model, out int insertedId)
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

        public bool TryUpdate(T model)
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