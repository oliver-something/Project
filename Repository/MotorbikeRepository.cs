using Models;
using Repository.Contracts;

namespace Repository;

public class MotorbikeRepository(Contexts contexts) : IVehicleRepository<Motorbike>
{
    private bool _disposed;
    
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

    public void InsertVehicle(Motorbike vehicle)
    {
        contexts.Motorbikes?.Add(vehicle);
    }

    public Motorbike? GetVehicleById(int id)
    {
        if (contexts.Motorbikes is null)
            throw new Exception("Not Found");
        return contexts.Motorbikes.Find(id);
    }

    public void Save()
    {
        contexts.SaveChanges();
    }
}