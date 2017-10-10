using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScheduleVis.BO
{
    public class ScheduleOriginFactory: IImportedItemFactory
    {
        public IImportedItem Create(string line)
        {
            ScheduleOrigin result = new ScheduleOrigin();
            result.PopulateFromLine(line);
            return result;
        }

        public string GetFieldCode()
        {
            return "LO";
        }
    }
}
