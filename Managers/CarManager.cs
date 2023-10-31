using Detailing.Interfaces;
using Detailing.Models;
using Detailing.Providers;

namespace Detailing.Managers;

public class CarManager : BaseManager<Car>
{
    private readonly IModelProvider<Car> _provider;

    public CarManager(IModelProvider<Car> provider) : base(provider)
    {
        _provider = provider;
    }

    public IEnumerable<Car> GetByCustomerId(int customerId)
    {
        var provider = _provider as CarProvider;
        var cars = provider.GetByCustomerId(customerId);
        return cars;
    }

    internal bool TryDeleteCustomerCar(int customerId, int carId)
    {
        var provider = _provider as CarProvider;
        var isDeleted = provider.TryDeleteCustomerCar(customerId, carId);
        return isDeleted;
    }

    internal bool TryUpdateCustomerCar(int customerId, int carId, Car car)
    {
        car.Id = carId;
        car.OwnerId = customerId;
        var provider = _provider as CarProvider;
        var isUpdated = provider.TryUpdate(car);
        return isUpdated;
    }
}