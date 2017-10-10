using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScheduleVis
{
    public class Common
    {
        public static string ImportFileUriBaseString
        {
            get
            {
                return Properties.Settings.Default.ResourceBaseURI + Properties.Settings.Default.ImportedFilesSubDir;
            }
        }
    }
}
