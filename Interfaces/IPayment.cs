using Detailing.Models;

namespace Detailing.Interfaces
{
    public interface IPayment
    {
        void Pay(double amount, User payee);
    }
}