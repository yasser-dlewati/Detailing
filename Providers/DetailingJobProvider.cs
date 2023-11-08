using System.Data;
using Detailing.Interfaces;
using Detailing.Models;

namespace Detailing.Providers;

public class DetailingJobProvider : BaseProvider<DetailingJob>
{
    public DetailingJobProvider(IDatabaseService dbService, IDataMapper<DetailingJob> dataMapper) : base(dbService, dataMapper)
    {
    }

    public override string SelectAllStoredProcedureName => "sp_Job_select_all";

    public override string SelectByIdStoredProcedureName => "sp_Job_select_all_by_Id";

    public override string InsertStoredProcedureName => "sp_Job_insert";

    public override string UpdateStoredProcedureName => "sp_Job_update";

    public override string DeleteByIdStoredProcedureName => "sp_Job_delete_by_Id";

    public override IDbDataParameter[] GetIdDataParameter(int id)
    {
        var parameters = new IDbDataParameter[]
        {
            new DatabaseParameter("JobId", id)
        };

        return parameters;
    }
}