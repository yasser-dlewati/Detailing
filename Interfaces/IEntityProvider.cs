namespace Detailing.Interfaces
{
    public interface IEntityProvider<T>
    {
        List<T> GetAll();
    }
}