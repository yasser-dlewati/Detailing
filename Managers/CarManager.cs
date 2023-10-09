using Detailing.Interfaces;
using Detailing.Models;

namespace Detailing.Managers;
public class CarManager : BaseManager<Car>
{
    public CarManager(IModelProvider<Car> provider) : base(provider)
    {
    }
}