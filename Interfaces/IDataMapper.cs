using System.Data;

namespace Detailing.Interfaces
{
    public interface IDataMapper<T>
    {
        T MapToEntity(DataRow row);
    }
}