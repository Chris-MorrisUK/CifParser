using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VDS.RDF;
using System.IO;

namespace ScheduleVis.BO
{
    public interface IFileController
    {
        void ProcessHeader(StreamReader inStream, ref IGraph targetGraph, IUriNode generationActivity,string fileName);
        IEnumerable<IImportedItem> ConvertItems(StreamReader inStream,out List<Exception> ErrorsEncountered);
    }
}
