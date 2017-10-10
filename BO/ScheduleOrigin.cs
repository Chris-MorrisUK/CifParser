using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScheduleVis.BO
{
    public class ScheduleOrigin: ServiceNodeBase, IImportedItem
    {
        public ScheduleOrigin()
        {
            
        }
        public override void PopulateFromLine(string line)
        {
            base.PopulateFromLine(line);
            ScheduledDep = ScheduleTimeExtractor.GetTime(line, 10);
            PublicDep = ScheduleTimeExtractor.GetTimeNoHalf(line, 15);
            Platform = line.Substring(19, 3).Trim();
            Line = line.Substring(22, 3).Trim();
            Engineering = ScheduleTimeExtractor.TwoCharacterDigitTime(line, 25);
            Pathing = ScheduleTimeExtractor.TwoCharacterDigitTime(line, 27);
            Activies = ActiviesCollection.CreateFromString(line.Substring(29, 12), ProgramState.PossibleActivities);
            Performance = ScheduleTimeExtractor.TwoCharacterDigitTime(line, 41);
        }

        public override void SaveToGraph(VDS.RDF.IGraph target, VDS.RDF.IUriNode provCreatingAction)
        {
            base.SaveToGraph(target, provCreatingAction);
        }

        
    }
}
