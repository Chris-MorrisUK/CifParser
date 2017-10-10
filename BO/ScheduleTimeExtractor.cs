using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScheduleVis.BO
{
    public static class ScheduleTimeExtractor
    {
        public static DateTime? GetTime(string line, int offset)
        {
            string timePart = line.Substring(offset, 4);
            if (string.IsNullOrWhiteSpace(timePart))
                return null;
           DateTime result = getTimeCommon(line, offset);
            if (line[offset + 5] == 'h')
                result.AddSeconds(30);
            return result;
        }

        private static DateTime getTimeCommon(string timePart, int offset)
        {
            
            //DateTime result = DateTime.ParseExact(timePart, TimeFormat, ProgramState.Provider);
            char[] timeChars = timePart.ToCharArray();
            char[] hourChars = { timeChars[0], timeChars[1] };
            char[] minChars = { timeChars[2], timeChars[3] };
            DateTime result = new DateTime();
            int hours = twoDigitParse(hourChars);
            int mins = twoDigitParse(minChars);
            result = result.AddHours(hours);
            result = result.AddMinutes(mins);

            return result;
        }

        private static int twoDigitParse(char[] digits)
        {
            int result = (digits[0] - 0x30) * 10;
            result += (digits[1] - 0x30);
            return result;
        }
        public static DateTime? GetTimeNoHalf(string line, int offset)
        {
            string timePart = line.Substring(offset, 4);
            if (string.IsNullOrWhiteSpace(timePart))
                return null;
            return getTimeCommon(line, offset);
        }

        public static TimeSpan? TwoCharacterDigitTime(string line, int offset)
        {            
            if (!char.IsDigit(line[offset + 1]))
            {
                string timeCode = line.Substring(offset, 1);
                if (string.IsNullOrEmpty(timeCode))
                    return null;
                if (string.IsNullOrWhiteSpace(timeCode))
                    return null;
                double nMinutes = double.Parse(timeCode);
                if (line[offset + 1] == 'h')
                {
                    TimeSpan res = TimeSpan.FromMinutes(nMinutes + 0.5);
                    return res;                    
                }
                return TimeSpan.FromMinutes(nMinutes);
            }
            else
            {  
                //No half minutes above 9
                string timeCode = line.Substring(offset, 2);
                int nMinutes = int.Parse(timeCode);
                return TimeSpan.FromMinutes(nMinutes);
            }

        }
        const string TimeFormat = "HHmm";
    }
}
