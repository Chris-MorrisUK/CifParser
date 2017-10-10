using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScheduleVis.BO
{
    public class ActiviesCollection: List<string>
    {
        public static ActiviesCollection CreateFromString(string source, Activity possibleActivities)
        {
            ActiviesCollection result = new ActiviesCollection();
            int lastChar =source.Length-2;
            for (int offset = 0; offset < lastChar; offset += 2)
            { 
                string key = source.Substring(offset,2).Trim();
                if (!string.IsNullOrWhiteSpace(key))
                {
                    if (possibleActivities.Activities.ContainsKey(key))
                        result.Add(key);
                    else
                        throw new ImportFileFormatException("Unkown", "Invalid Activities passed", 0, 0);
                }
            }
            return result;
        }
    }
}
