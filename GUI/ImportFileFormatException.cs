using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScheduleVis
{
    public class ImportFileFormatException : Exception
    {
        public ImportFileFormatException(string fileName, string message, long line, int col) :base (message)
        {
            FileName = fileName;
            Line = line;
            Column = col;
        }
        public string FileName;
        public long Line;
        public int Column; 
    }
}
