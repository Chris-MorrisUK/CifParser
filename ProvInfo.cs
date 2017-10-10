using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScheduleVis
{
    public class ProvInfo
    {
        public readonly string Aurthor;
        public readonly bool AurthorIsUri;

        public ProvInfo(string aurthor, bool isUri)
        {
            Aurthor = aurthor;
            AurthorIsUri = isUri;
        }
    }
}
