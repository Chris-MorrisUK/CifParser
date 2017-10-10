using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VDS.RDF;

namespace ScheduleVis.BO
{
    public interface IImportedItem
    {
        void PopulateFromLine(string line);
        void SaveToGraph(IGraph target, IUriNode provCreatingAction);
       // IEnumerable<string> GetAsSparqlLines();
    }
}
