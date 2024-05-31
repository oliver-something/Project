using Models;

namespace Presentation;

public static class TaxCalculator
{
    public static int GetTax(Vehicle vehicle, List<DateTime> dates)
    {
        if (dates.Count is 0)
            return 0;

        var intervalStart = dates[0];
        var maxFee = 0;
        foreach (var date in dates)
        {
            var currentFee = GetTollFee(date, vehicle);
            if (date - intervalStart < TimeSpan.FromMinutes(60))
            {
                maxFee = Math.Max(maxFee, currentFee);
            }
            else
            {
                maxFee += currentFee;
                intervalStart = date;
            }
        }
        
        maxFee += GetTollFee(dates[^1], vehicle);

        return maxFee > 60 ? 60 : maxFee;
    }

    
    private static int GetTollFee(DateTime date, Vehicle vehicle)
    {
        if (IsTollFreeDate(date) || IsTollFreeVehicle(vehicle))
            return 0;

        int hour = date.Hour;
        int minute = date.Minute;
        int totalMinutes = hour * 60 + minute;

        return totalMinutes switch
        {
            >= 360 and < 420 => 8,
            >= 420 and < 480 => 13,
            >= 480 and < 540 => 18,
            >= 540 and < 570 => 13,
            >= 570 and < 780 => 8,
            >= 780 and < 810 => 13,
            >= 810 and < 960 => 18,
            >= 960 and < 990 => 13,
            >= 990 and < 1050 => 8,
            _ => 0
        };
    }
    
    private static bool IsTollFreeVehicle(Vehicle vehicle)
    {
        var vehicleType = vehicle.GetVehicleType();
        return vehicleType.Equals(TollFreeVehicles.Motorcycle.ToString()) ||
               vehicleType.Equals(TollFreeVehicles.Tractor.ToString()) ||
               vehicleType.Equals(TollFreeVehicles.Emergency.ToString()) ||
               vehicleType.Equals(TollFreeVehicles.Diplomat.ToString()) ||
               vehicleType.Equals(TollFreeVehicles.Foreign.ToString()) ||
               vehicleType.Equals(TollFreeVehicles.Military.ToString());
    }
    
    private static bool IsTollFreeDate(DateTime date)
    {
        int year = date.Year;
        int month = date.Month;
        int day = date.Day;

        if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday) return true;

        if (year is 2013)
        {
            if (month == 1 && day == 1 ||
                month == 3 && (day == 28 || day == 29) ||
                month == 4 && (day == 1 || day == 30) ||
                month == 5 && (day == 1 || day == 8 || day == 9) ||
                month == 6 && (day == 5 || day == 6 || day == 21) ||
                month == 7 ||
                month == 11 && day == 1 ||
                month == 12 && (day == 24 || day == 25 || day == 26 || day == 31))
            {
                return true;
            }
        }
        return false;
    }
    
    private enum TollFreeVehicles
    {
        Motorcycle = 0,
        Tractor = 1,
        Emergency = 2,
        Diplomat = 3,
        Foreign = 4,
        Military = 5
    }
}