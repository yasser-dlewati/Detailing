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

    public async Task<IEnumerable<Car>> GetByCustomerIdAsync(int customerId)
    {
        var cars = await (_provider as CarProvider).GetByCustomerIdAsync(customerId);
        return cars;
    }

    public async Task<Car> GetCarByJobIdAsync(int jobId)
    {
        var car = await (_provider as CarProvider).GetCarByJobIdAsync(jobId);
        return car;
    }

    public async Task<bool> TryDeleteCustomerCarAsync(int customerId, int carId)
    {
        var isDeleted = await (_provider as CarProvider).TryDeleteCustomerCarAsync(customerId, carId);
        return isDeleted;
    }

    public async Task<bool> TryUpdateCustomerCarAsync(int customerId, int carId, Car car)
    {
        car.Id = carId;
        car.OwnerId = customerId;
        var provider = _provider as CarProvider;
        var isUpdated = await provider.TryUpdateAsync(car);
        return isUpdated;
    }
}