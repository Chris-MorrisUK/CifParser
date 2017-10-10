using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScheduleVis.BO
{
    public class ScheduledStop:ServiceNodeBase,  IImportedItem
    {


        public override void PopulateFromLine(string line)
        {
            base.PopulateFromLine(line);
            //times
            ScheduledArrival = ScheduleTimeExtractor.GetTime(line, 10);
            ScheduledDep = ScheduleTimeExtractor.GetTime(line, 15);
            PassingTime = ScheduleTimeExtractor.GetTime(line, 20);
            PublicArrival = ScheduleTimeExtractor.GetTimeNoHalf(line, 25);
            PublicDep = ScheduleTimeExtractor.GetTimeNoHalf(line, 29);

            Platform = line.Substring(33, 3).TrimEnd();
            Line = line.Substring(36, 3).TrimEnd();
            Path = line.Substring(39, 3).TrimEnd();

            Activies = ActiviesCollection.CreateFromString(line.Substring(42, 12), ProgramState.PossibleActivities);

            Engineering = ScheduleTimeExtractor.TwoCharacterDigitTime(line, 54);
            Pathing = ScheduleTimeExtractor.TwoCharacterDigitTime(line, 56);            
            Performance = ScheduleTimeExtractor.TwoCharacterDigitTime(line, 58);
        }

        public override void SaveToGraph(VDS.RDF.IGraph target, VDS.RDF.IUriNode provCreatingAction)
        {
            base.SaveToGraph(target, provCreatingAction);
        }
    }
}
