namespace Models;

public class Motorbike: Vehicle
{
    public override string GetVehicleType()
    {
        return VehicleType ?? "Motorbike";
    }
}