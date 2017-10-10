using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScheduleVis.BO
{
    public class BankHolidays
    {
        public static BankHolidayRunningDays ParseFromChar(char code)
        {
            if (char.IsWhiteSpace(code))
                return BankHolidayRunningDays.Normal;
            switch (code)
            { 
                case 'X':
                    return BankHolidayRunningDays.DoesNotRunOnBankHolidayMondays;
                case 'E':
                    return BankHolidayRunningDays.DoesNotRunOn_Edinburgh_Holiday_dates;
                case 'G':
                    return BankHolidayRunningDays.DoesNotRunOn_Glasgow_Holiday_dates;
                default:
                    return BankHolidayRunningDays.Unknown;
            }
        }
        public enum BankHolidayRunningDays
        {
            Normal,
            DoesNotRunOnBankHolidayMondays,
            DoesNotRunOn_Edinburgh_Holiday_dates,
            DoesNotRunOn_Glasgow_Holiday_dates,
            Unknown
        }
    }

    
}
