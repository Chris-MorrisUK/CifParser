using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VDS.RDF;
using VDS.RDF.Parsing;

namespace ScheduleVis.BO
{
    public class ScheduleFileControl : IFileController
    {
        public void ProcessHeader(System.IO.StreamReader inStream, ref VDS.RDF.IGraph targetGraph, VDS.RDF.IUriNode generationActivity, string fileName)
        {
            string firstLine = inStream.ReadLine();
            if (!firstLine.StartsWith("HD"))//header record type is HD
                throw new ImportFileFormatException(fileName,
                    "File must include a header, denoted HD",
                    0,
                    0
                    );
            string mainframe_id = firstLine.Substring(2, 20);
            DateTime date_extract = DateTime.ParseExact(firstLine.Substring(22, 10), dtFormat, ProgramState.Provider);
            string curr_file_ref = firstLine.Substring(32, 7);
            string last_file_ref = firstLine.Substring(39, 7);
           // char update_type = firstLine[46];//not going to use this
            DateTime extract_start = DateTime.ParseExact(firstLine.Substring(48, 6), dateFormat, ProgramState.Provider);
            DateTime extract_end = DateTime.ParseExact(firstLine.Substring(54, 6), dateFormat, ProgramState.Provider);

            //Now put that in the graph
            string sourceFileUri = Common.ImportFileUriBaseString + "#" + DateTime.Now.ToString("o") + "SourceFile_" + fileName;
            IUriNode sourceFileNode = targetGraph.CreateUriNode(UriFactory.Create(sourceFileUri));
            targetGraph.Assert(sourceFileNode, UriNodeExt.RdfType(targetGraph), targetGraph.CreateUriNode("prov:Entity"));
            if(generationActivity != null)
                targetGraph.Assert(generationActivity, targetGraph.CreateUriNode(Properties.Settings.Default.provUsed), sourceFileNode);
            ILiteralNode sourceID = targetGraph.CreateLiteralNode(mainframe_id);
            sourceFileNode.IdentifyNode(sourceID);
            //IUriNode idNode = targetGraph.CreateUriNode(Properties.Settings.Default.ID);
            //targetGraph.Assert(sourceFileNode, idNode, sourceID);
            targetGraph.Assert(sourceFileNode, targetGraph.CreateUriNode(UriFactory.Create(Properties.Settings.Default.provGeneratedTime)),
            targetGraph.CreateLiteralNode(date_extract.ToString(XmlSpecsHelper.XmlSchemaDateTimeFormat), UriFactory.Create(XmlSpecsHelper.XmlSchemaDataTypeDateTime)));
            IUriNode invalidatedNote = targetGraph.CreateUriNode(Properties.Settings.Default.provInvalidAtTime);
            ILiteralNode timeInvalid = targetGraph.CreateLiteralNode(extract_end.ToString(XmlSpecsHelper.XmlSchemaDateTimeFormat), UriFactory.Create(XmlSpecsHelper.XmlSchemaDataTypeDateTime));
            targetGraph.Assert(sourceFileNode, invalidatedNote, timeInvalid);
        }

        public IEnumerable<IImportedItem> ConvertItems(System.IO.StreamReader inStream,out List<Exception> Errors)
        {
            Errors = new List<Exception>();
            List<IImportedItem> schedules = new List<IImportedItem>();
            BasicScheduleItemFactory bsFactory = new BasicScheduleItemFactory();
            string scheduleStart = bsFactory.GetFieldCode();
            int lineN = 1; //header will have been parsed at the very least by this point
            //now parse all the LI lines                                
            ScheduledStopFactory interimFact = new ScheduledStopFactory();
            ChangesEnRouteFactory changesFact = new ChangesEnRouteFactory();
            ScheduleOriginFactory orginFact = new ScheduleOriginFactory();
            ScheduleTerminatesFactory termFact = new ScheduleTerminatesFactory();
            char interimStopFirstChar = interimFact.GetFieldCode()[0];
            while (!inStream.EndOfStream)
            {
                try
                {
                    string line = inStream.ReadLine();                    
                    if (line.StartsWith(scheduleStart))
                    {
                        int callNumber = 0;
                        while (line.EndsWith("C"))
                        {
                            //TODO: Cancelations
                            line = inStream.ReadLine();
                        }
                        ScheduledRoute route = (ScheduledRoute)bsFactory.Create(line);
                        line = inStream.ReadLine();
                        while (line.StartsWith("BX"))
                        {
                            line = inStream.ReadLine();
                        }
                        string loLine = line;

                        if (!loLine.StartsWith(orginFact.GetFieldCode()))
                            throw new ImportFileFormatException("", "Schedule start not found", 0, lineN);
                        ScheduleOrigin orgin = (ScheduleOrigin)orginFact.Create(loLine);
                        orgin.CallNumber = callNumber++;
                        route.ServiceNodes.Add(orgin);
                        line = inStream.ReadLine();
                        while (line.StartsWith(interimFact.GetFieldCode())
                            || line.StartsWith(changesFact.GetFieldCode()))
                        {
                            if (line[0] == interimStopFirstChar)//not for changes en route
                            {
                                ScheduledStop interimStop = (ScheduledStop)interimFact.Create(line);
                                interimStop.CallNumber = callNumber++;
                                route.ServiceNodes.Add(interimStop);
                                
                            }
                            line = inStream.ReadLine();
                            lineN++;
                        }
                        //LT line
                        string ltLine = line;
                        if (!ltLine.StartsWith(termFact.GetFieldCode()))
                            throw new ImportFileFormatException("", "Schedule end not found", 0, lineN);
                        ScheduleTerminates terminates = (ScheduleTerminates)termFact.Create(ltLine);
                        terminates.CallNumber = callNumber;
                        route.ServiceNodes.Add(terminates);
                        schedules.Add(route);

                    }
                                    }
                catch(Exception ex)
                {
                    Errors.Add(ex);
                }
                
            }
            return schedules;
        }

     /*   public IEnumerable<IImportedItem> ConvertItems(System.IO.StreamReader inStream)
        {
            List<IImportedItem> schedules = new List<IImportedItem>();
            Dictionary<string, IImportedItemFactory> importableItems = new Dictionary<string, IImportedItemFactory>();
            IImportedItemFactory bsItemFact = new  BasicScheduleItemFactory();
            importableItems.Add(bsItemFact.GetFieldCode(), bsItemFact);
            while (!inStream.EndOfStream)
            {
                string line = inStream.ReadLine();
                foreach (var i in importableItems)
                {
                    if (line.StartsWith(i.Key))//More complex filtering can go here, if needed for the file being imported
                    {
                        IImportedItem toAdd = i.Value.Create(line);                        
                        schedules.Add(toAdd);
                    }
                }
            }
            return schedules;
        }*/

        private const string dtFormat = "ddMMyyHHmm";//capital H for 24 hour clocks
        private const string dateFormat = "ddMMyy";
    }
}
