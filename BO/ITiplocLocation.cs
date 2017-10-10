using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScheduleVis.BO
{
    public interface ITiplocLocation
    {
         string GetTiploc();
         Station LinkedStation();
    }
}
