using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScheduleVis.BO
{
    public class ScheduleTerminates: ServiceNodeBase,IImportedItem
    {

        public ScheduleTerminates()
        {
            
        }
        public override void PopulateFromLine(string line)
        {
            base.PopulateFromLine(line);
            //times
            ScheduledArrival = ScheduleTimeExtractor.GetTime(line, 10);
            PublicArrival = ScheduleTimeExtractor.GetTimeNoHalf(line, 15);    
            Platform = line.Substring(19, 3).TrimEnd();
            Path = line.Substring(22, 3).TrimEnd();
            Activies = ActiviesCollection.CreateFromString(line.Substring(25, 12), ProgramState.PossibleActivities);
        }

        public override void SaveToGraph(VDS.RDF.IGraph target, VDS.RDF.IUriNode provCreatingAction)
        {
            base.SaveToGraph(target, provCreatingAction);
        }
    }
}
