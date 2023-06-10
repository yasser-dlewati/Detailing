using Detailing.Entities;

namespace Detailing.Interfaces
{
    public interface IPayment
    {
        void Pay(double amount, Person payee);
    }
}