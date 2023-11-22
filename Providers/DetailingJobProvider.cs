using System.Data;
using Detailing.Interfaces;
using Detailing.Models;
using Microsoft.Extensions.Caching.Memory;

namespace Detailing.Providers;

public class DetailingJobProvider : BaseProvider<DetailingJob>
{
    public DetailingJobProvider(IDatabaseService dbService, IDataMapper<DetailingJob> dataMapper, IMemoryCache cache) : base(dbService, dataMapper, cache)
    {
    }

    public override string SelectAllStoredProcedureName => "sp_Job_select_all";

    public override string SelectByIdStoredProcedureName => "sp_Job_select_all_by_Id";

    public override string InsertStoredProcedureName => "sp_Job_insert";

    public override string UpdateStoredProcedureName => "sp_Job_update";

    public override string DeleteByIdStoredProcedureName => "sp_Job_delete";

    public override IDbDataParameter[] GetIdDataParameter(int id)
    {
        var parameters = new IDbDataParameter[]
        {
            new DatabaseParameter("JobId", id)
        };

        return parameters;
    }
}