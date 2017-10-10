using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScheduleVis.BO
{
    public interface IImportedItemFactory
    {
         IImportedItem Create(string line);
         string GetFieldCode();
    }
}
