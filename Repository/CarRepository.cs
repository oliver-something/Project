using Models;
using Repository.Contracts;

namespace Repository;

public class CarRepository(Contexts contexts) : IVehicleRepository<Car>
{
    private bool _disposed;
    
    public void InsertVehicle(Car vehicle)
    {
        contexts.Cars?.Add(vehicle);
    }

    public Car? GetVehicleById(int id)
    {
        return contexts.Cars?.Find(id);
    }

    public void Save()
    {
        contexts.SaveChanges();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (_disposed)
            if (disposing)
                contexts.Dispose();
        _disposed = true;
    }
}