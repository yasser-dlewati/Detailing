using System.Data;

namespace Detailing.Interfaces
{
    public interface IDataMapper<T>
    {
        T MapToModel(DataRow row);
    }
}