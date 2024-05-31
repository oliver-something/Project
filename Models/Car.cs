namespace Models;

public class Car : Vehicle
{
    public override string? GetVehicleType()
    {
        return VehicleType ?? "Car";
    }
}