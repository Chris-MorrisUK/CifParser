using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScheduleVis.BO
{
    public class ScheduleTerminatesFactory: IImportedItemFactory
    {
        public IImportedItem Create(string line)
        {
            ScheduleTerminates result = new ScheduleTerminates();
            result.PopulateFromLine(line);
            return result;
        }

        public string GetFieldCode()
        {
            return "LT";
        }
    }
}
