using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VDS.RDF;
using System.Globalization;
using VDS.RDF.Parsing;
using System.IO;

namespace ScheduleVis.BO
{
    public class StationFileControl: IFileController
    {
        public void ProcessHeader(StreamReader inStream, ref IGraph stationNameGraph, IUriNode generationActivity,string fileName)
        {
            string firstLine = inStream.ReadLine();
            if (!firstLine.StartsWith("A"))
                throw new ImportFileFormatException(fileName,
                    "First character of first line must be A",
                    0,
                    0
                    );
            if(firstLine.Substring(30,18).TrimEnd() != Properties.Settings.Default.FileSpecString)
                throw new ImportFileFormatException(fileName,
                    "Unexpected File version, may not read as expected",
                    0,
                    30
                    );
            string createTimeStr = firstLine.Substring(48, 17).Trim();
            CultureInfo provider = ProgramState.Provider;
            string formatString = @"dd/MM/yy HH.mm.ss";
            DateTime createdTime = DateTime.ParseExact(createTimeStr,formatString,provider);
            string sourceFileUri = Common.ImportFileUriBaseString + "#" + DateTime.Now.ToString("o") + "SourceFile_" + fileName;
            IUriNode sourceFileNode = stationNameGraph.CreateUriNode(UriFactory.Create(sourceFileUri));
            if(generationActivity != null)
                stationNameGraph.Assert(generationActivity, stationNameGraph.CreateUriNode(Properties.Settings.Default.provUsed), sourceFileNode);
            stationNameGraph.Assert(sourceFileNode, UriNodeExt.RdfType(stationNameGraph), stationNameGraph.CreateUriNode("prov:Entity"));
            stationNameGraph.Assert(sourceFileNode,stationNameGraph.CreateUriNode(UriFactory.Create(Properties.Settings.Default.provGeneratedTime)),
                stationNameGraph.CreateLiteralNode(createdTime.ToString(XmlSpecsHelper.XmlSchemaDateTimeFormat),UriFactory.Create(XmlSpecsHelper.XmlSchemaDataTypeDateTime)));
        }

        public IEnumerable<IImportedItem> ConvertItems(StreamReader inStream, out List<Exception> ErrorsEncountered)
        {
            ErrorsEncountered = new List<Exception>();
            List<Station> stations = new List<Station>();
            while (!inStream.EndOfStream)
            {
                try
                {
                    string line = inStream.ReadLine();
                    if (line.StartsWith("A"))//More complex filtering can go here, if needed for the file being imported
                    {
                        Station toAdd = new Station();
                        toAdd.PopulateFromLine(line);
                        stations.Add(toAdd);
                    }
                }
                catch (Exception ex)
                {
                    ErrorsEncountered.Add(ex);
                }
            }
            return stations;
        }    
    }
}
