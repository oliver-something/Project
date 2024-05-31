using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Models;
using Repository;
using Repository.Contracts;

namespace Presentation;

public static class Configurations
{
    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder()
            .ConfigureServices(service =>
            {
                //Change the path of Database!
                service.AddSqlite<Contexts>(@"Data Source=/Users/oliver/RiderProjects/Project/Models/db/database.db;");
                service.AddScoped<IVehicleRepository<Motorbike>, MotorbikeRepository>();
                service.AddScoped<IVehicleRepository<Car>, CarRepository>();
                service.AddScoped<ITaxAmountRepository<MotorbikeTaxAmount>, MotorbikeTaxAmountRepository>();
                service.AddScoped<ITaxAmountRepository<CarTaxAmount>, CarTaxAmountRepository>();
                service.AddScoped<Consumer>();
            })
            .ConfigureLogging((context, logging) =>
            {
                var config = context.Configuration.GetSection("Logging");
                logging.AddConfiguration(config);
                logging.AddConsole();
                logging.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning);
            });
    }
}