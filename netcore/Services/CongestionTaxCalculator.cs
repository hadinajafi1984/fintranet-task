using System;
using System.Collections.Generic;
using System.Linq;
using congestion.calculator.Domain.Context;


public class CongestionTaxCalculator
{
    /**
         * Calculate the total toll fee for one day
         *
         * @param vehicle - the vehicle
         * @param dates   - date and time of all passes on one day
         * @return - the total congestion tax for that day
         */
    private ApplicationContext _dbContext;

    public CongestionTaxCalculator(ApplicationContext dbContext)
    {
        _dbContext = dbContext;
    }

    public int GetTax(string vehicle, DateTime[] dates)
    {
        dates = dates.OrderBy(i => i.Date).ToArray();
        DateTime intervalStart = dates[0];
        int tempFee = GetTollFee(intervalStart, vehicle);
        int[] onhour = new int[24];
        int i = 1;
        int totalFee = 0;
        foreach (DateTime date in dates)
        { 
            int nextFee = GetTollFee(date, vehicle);
            var diffInMillies = date - intervalStart;
            //long minutes = diffInMillies.Milliseconds / 1000 / 60;
            double minutes = diffInMillies.TotalMinutes ;
            if (minutes <= 60)
            {
                //if (totalFee > 0) totalFee -= tempFee;
                if (nextFee >= tempFee) tempFee = nextFee;
                onhour[i] = tempFee;
            }
            else
            {
                i++;
                intervalStart = date;
                tempFee = GetTollFee(intervalStart, vehicle);
                onhour[i] = tempFee;
            }
        }
        //if (totalFee > 60) totalFee = 60;
        return onhour.Sum()>60?60: onhour.Sum();
    }

    private bool IsTollFreeVehicle(string vehicle)
    {
        if (vehicle == null) return false;
        //String vehicleType = vehicle.GetVehicleType();
        var freeVehicles = _dbContext.TaxExemptVehicles.ToList();
        if (freeVehicles.Any(i=>i.VehicleName== vehicle))
        {
            return true;
        }

        return false;
    }

    public int GetTollFee(DateTime date, string vehicle)
    {
        if (IsTollFreeDate(date) || IsTollFreeVehicle(vehicle)) return 0;

        int hour = date.Hour;
        int minute = date.Minute;
       
       
        var hoursAmounts = _dbContext.HoursAmounts.ToList();
        var amount1 = hoursAmounts.Where(i => hour <= i.EndHour && minute <= i.EndMinute).OrderBy(i => i.EndHour)
            .ThenBy(i => i.EndMinute);
        var amount = hoursAmounts.Where(i => hour <= i.EndHour  && minute<=i.EndMinute)?.OrderBy(i=>i.EndHour).ThenBy(i=>i.EndMinute).FirstOrDefault()?.Amount;
        return amount ?? 0;

        //if (hour == 6 && minute >= 0 && minute <= 29) return 8;
        //else if (hour == 6 && minute >= 30 && minute <= 59) return 13;
        //else if (hour == 7 && minute >= 0 && minute <= 59) return 18;
        //else if (hour == 8 && minute >= 0 && minute <= 29) return 13;
        //else if (hour >= 8 && hour <= 14 && minute >= 30 && minute <= 59) return 8;
        //else if (hour == 15 && minute >= 0 && minute <= 29) return 13;
        //else if (hour == 15 && minute >= 0 || hour == 16 && minute <= 59) return 18;
        //else if (hour == 17 && minute >= 0 && minute <= 59) return 13;
        //else if (hour == 18 && minute >= 0 && minute <= 29) return 8;
        //else return 0;
    }

    private Boolean IsTollFreeDate(DateTime date)
    {
        var freeDates = _dbContext.FreeDates.ToList();
        if (freeDates.Any(i=>i.FreeDate== date))
        {
            return true;
        }

        //int year = date.Year;
        //int month = date.Month;
        //int day = date.Day;

        //if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday) return true;

        //if (year == 2013)
        //{
        //    if (month == 1 && day == 1 ||
        //        month == 3 && (day == 28 || day == 29) ||
        //        month == 4 && (day == 1 || day == 30) ||
        //        month == 5 && (day == 1 || day == 8 || day == 9) ||
        //        month == 6 && (day == 5 || day == 6 || day == 21) ||
        //        month == 7 ||
        //        month == 11 && day == 1 ||
        //        month == 12 && (day == 24 || day == 25 || day == 26 || day == 31))
        //    {
        //        return true;
        //    }
        //}
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