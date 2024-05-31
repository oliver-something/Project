using Models;

namespace Repository.Contracts;

public interface IVehicleRepository<T> : IDisposable where T : Vehicle
{
   void InsertVehicle(T vehicle);
   T? GetVehicleById(int id);
   void Save();
}