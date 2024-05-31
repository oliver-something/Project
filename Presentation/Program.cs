namespace Presentation;
using Microsoft.Extensions.DependencyInjection;

public static class Program
{
    public static void Main(string[] args)
    {
        var state = true;
        while (state)
        {
            Console.WriteLine("Welcome to Gutenberg Tax Calculation App");
            Launch(args);
            Console.WriteLine("Do you want to continue Yes[0], No[1]");
            var select = (Select) Convert.ToInt32(Console.ReadLine());
            switch (select)
            {
                case Select.Yes:
                    state = true;
                    continue;
                case Select.No:
                    state = false;
                    Console.WriteLine("Adios!");
                    break;
                default:
                    Console.WriteLine("Wrong Input!");
                    return;
            }
        }
    }

    private static void Launch(string[] args)
    {
        var app = Configurations.CreateHostBuilder(args).Build();
        var service = app.Services.GetService<Consumer>();
        Console.WriteLine("Please choose the action you are looking for : ");
        Console.WriteLine(" [0] Add Car \n [1] Add Motor Bike \n [2] Add Car Record Tax \n [3] Add Bike Record Tax \n [4] Print Car Record Task \n [5] Print Bike Record Task \n");
        var actionState = Convert.ToInt32(Console.ReadLine());
        var actions = (Actions) actionState;
        switch (actions)
        {
            case Actions.AddCar:
                service?.AddCar();
                break;
            case Actions.AddMotorbike:
                service?.AddMotorBike();
                break;
            case Actions.AddCarTaxRecord:
                service?.AddRecordForCars();
                break;
            case Actions.AddBikeTaxRecord:
                service?.AddRecordForMotorBike();
                break;
            case Actions.PrintCarRecordTax:
            {
                try
                {
                    Console.WriteLine("Please enter the id you are looking for : ");
                    var id = Convert.ToInt32(Console.ReadLine());
                    service?.PrintCarTax(id);
                }
                catch (Exception)
                {
                    Console.WriteLine("The Car you were looking for is not registered!");
                }
            } break;
            case Actions.PrintBikeRecordTax:
            {
                try
                {
                    Console.WriteLine("Please enter the id you are looking for : ");
                    var id = Convert.ToInt32(Console.ReadLine());
                    service?.PrintMotorBikeTax(id);
                }
                catch (Exception)
                {
                    Console.WriteLine("The Car you were looking for is not registered!");
                }
            } break;
            default:
            {
                Console.WriteLine("The Action you are looking for does not exist");
            } break;
        }
    }
}

public enum Actions
{
    AddCar = 0,
    AddMotorbike = 1,
    AddCarTaxRecord = 2,
    AddBikeTaxRecord = 3,
    PrintCarRecordTax = 4,
    PrintBikeRecordTax = 5
}

public enum Select
{
    Yes = 0,
    No = 1
}