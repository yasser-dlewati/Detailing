using Detailing.Interfaces;
using Detailing.Models;

namespace Detailing.Providers;

public class DetailerProvider : IUserTypeProvider<Detailer>
{
    public string SelectTypeStoredProcedureName => "sp_User_select_by_Type_Detailer";

    public IEnumerable<Detailer> GetUsersOfType()
    {
        throw new NotImplementedException();
    }
}
