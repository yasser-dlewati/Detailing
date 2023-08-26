namespace Detailing.Interfaces;

public interface IUserTypeProvider<T>
{
    abstract string SelectTypeStoredProcedureName { get; }

    IEnumerable<T> GetUsersOfType();
}