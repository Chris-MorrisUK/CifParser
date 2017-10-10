using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScheduleVis.BO
{
    public class ChangesEnRouteFactory: IImportedItemFactory
    {
        public IImportedItem Create(string line)
        {
            throw new NotImplementedException();
        }

        public string GetFieldCode()
        {
            return "CR";
        }
    }
}
