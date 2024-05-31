using Models;
using Repository.Contracts;

namespace Presentation;

public class Consumer(
    ITaxAmountRepository<CarTaxAmount> carTaxAmountRepository,
    IVehicleRepository<Car> carRepository,
    IVehicleRepository<Motorbike> bikeRepository,
    ITaxAmountRepository<MotorbikeTaxAmount> bikeTaxAmountRepository)
{

    public void AddCar()
    {
        Console.WriteLine("Please enter Id for car : ");
        var id = Convert.ToInt32(Console.ReadLine());
        carRepository.InsertVehicle(new Car {Id = id, VehicleType = "Car"});
        carRepository.Save();
    }

    public void AddMotorBike()
    {
        Console.WriteLine("Please enter Id for motor bike");
        var id = Convert.ToInt32(Console.ReadLine());
        bikeRepository.InsertVehicle(new Motorbike {Id = id, VehicleType = "Car"});
        bikeRepository.Save();
    }
    
    public void PrintCarTax(int carId)
    {
        var output = carTaxAmountRepository.GetTaxByBikeId(carId);
        Console.WriteLine($"{output.CarId} - {output.Tax}");
        Console.WriteLine("-------------------------------");
        if (output.Dates is null) return;
        foreach (var date in output.Dates)
        {
            if (output.Dates is null) return;
            Console.WriteLine($"Date : {date}");
        }
    }
    
    public void PrintMotorBikeTax(int bikeId)
    {
        var output = bikeTaxAmountRepository.GetTaxByBikeId(bikeId);
        Console.WriteLine($"{output.MotorbikeId} - {output.Tax}");
        Console.WriteLine("-------------------------------");
        if (output.Dates is null) return;
        foreach (var date in output.Dates)
        {
            if (output.Dates is null) return;
            Console.WriteLine($"Date : {date}");
        }
    }
    
    public void AddRecordForMotorBike()
    {
        Console.WriteLine("Enter ID for the car you want to add : ");
        var id = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("How many dates do you want to add ?");
        var datesCount = Convert.ToInt32(Console.ReadLine() ?? "0");
        var motorbike = bikeRepository.GetVehicleById(id) ?? new Motorbike();
        bikeTaxAmountRepository.InsertRecordTax(new MotorbikeTaxAmount
        {
            MotorbikeId = motorbike.Id,
            Dates = GetDates(datesCount),
            Tax = TaxCalculator.GetTax(motorbike, GetDates(datesCount))
        });
        bikeTaxAmountRepository.Save();
    }
    
    public void AddRecordForCars()
    {
        Console.WriteLine("Enter ID for the car you want to add : ");
        var id = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("How many dates do you want to add ?");
        var datesCount = Convert.ToInt32(Console.ReadLine() ?? "0");
        var car = carRepository.GetVehicleById(id) ?? new Car();
        var dates = GetDates(datesCount);
        carTaxAmountRepository.InsertRecordTax(new CarTaxAmount
        {
            CarId = car.Id,
            Dates = dates,
            Tax = TaxCalculator.GetTax(car, dates)
        });
        carTaxAmountRepository.Save();
    }

    private static List<DateTime> GetDates(int count)
    {
        List<DateTime> dateTimes = [];
        for (var i = 0; i < count; i++)
        {
            dateTimes.Add(GetDateTime());
        }
        return dateTimes;
    }

    private static DateTime GetDateTime()
    {
        Console.WriteLine("Please Write down the Date exp: 12-11-2022, DD-MM-YYYY");
        var input = Console.ReadLine() ?? "01-01-2013";
        var stringDate = input.Split('-');
        Console.WriteLine("Please Write down the Time exp: 18:22:00, HH:MM:SS");
        var timeInput = Console.ReadLine() ?? "18:22:00";
        var stringTime = timeInput.Split(":");
        DateTime dateTime = new(Convert.ToInt32(stringDate[2]),
            Convert.ToInt32(stringDate[1]),
            Convert.ToInt32(stringDate[0]),
            Convert.ToInt32(stringTime[0]),
            Convert.ToInt32(stringTime[1]),
            Convert.ToInt32(stringTime[2]));
        return dateTime;
    }
}