using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScheduleVis.BO
{
    public class BasicScheduleItemFactory: IImportedItemFactory
    {
        public IImportedItem Create(string line)
        {
          /*  char recordType = line[2];
            switch(recordType)
            {
                case 'N':*/
            ScheduledRoute route = new ScheduledRoute();
            route.PopulateFromLine(line);
            return route;
            /*    default:
                    return null;//TODO: handle Updates and deletes
        }*/
        }

        public string GetFieldCode()
        {
            return "BSN"; //Only interested in New records here
        }
    }
}
