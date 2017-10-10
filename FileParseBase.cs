using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VDS.RDF;
using VDS.RDF.Parsing;
using System.IO;
using ScheduleVis.BO;
using System.Collections;
using System.Threading;

namespace ScheduleVis
{
    public class FileParseBase
    {
        public virtual IGraph ParseFile(string fName, ProvInfo prov, IFileController fileController, out List<Exception> Errors, MainWindowViewModel toUpdate,string outputFormat)
        {
            IGraph targetGraph = new Graph();
            targetGraph.BaseUri = UriFactory.Create(Properties.Settings.Default.StationGraphBase);
            ontovis.Util.AddNamesSpaces(targetGraph);
            return ParseFile(fName, prov, fileController, targetGraph,out Errors,toUpdate,outputFormat);
        }

        public virtual IGraph ParseFile(string fName, ProvInfo prov, IFileController fileController,IGraph startingGraph,out List<Exception> Errors, 
            MainWindowViewModel toUpdate,string outputFormat)
        {
            IUriNode provAction = null;
            if(prov != null) provAction = addProvenance(ref startingGraph, prov.Aurthor, prov.AurthorIsUri);
            ParseFileDetail(fName, ref startingGraph, provAction, fileController,out Errors,toUpdate, outputFormat);
            return startingGraph;
        }

        protected virtual IUriNode addProvenance(ref IGraph targetGraph, string responsiblePerson,bool aurthorIsUri)
        {
            string fileImportUri = Common.ImportFileUriBaseString + "#" + DateTime.Now.ToString(XmlSpecsHelper.XmlSchemaDateTimeFormat) + "FileImport";
            IUriNode fileParseActionNode = targetGraph.CreateUriNode(UriFactory.Create(fileImportUri));
            targetGraph.Assert(fileParseActionNode, UriNodeExt.RdfType(targetGraph), targetGraph.CreateUriNode(Properties.Settings.Default.ProvActivity));
            ILiteralNode startTimeNode = targetGraph.CreateLiteralNode(DateTime.Now.ToString(XmlSpecsHelper.XmlSchemaDateTimeFormat), UriFactory.Create(XmlSpecsHelper.XmlSchemaDataTypeDateTime));
            targetGraph.Assert(fileParseActionNode, targetGraph.CreateUriNode(Properties.Settings.Default.ProvStartedAtTime), startTimeNode);
            INode aurthor = null;
            if (!aurthorIsUri)
                aurthor = targetGraph.CreateLiteralNode(responsiblePerson);//maybe sanitise this...            
            else
            {
                Uri aurthorUri;
                if (Uri.TryCreate(responsiblePerson, UriKind.Absolute, out aurthorUri))
                    aurthor = targetGraph.CreateUriNode(aurthorUri);
                else
                {
                    OnMessageToDisplay("Aurthors name Uri invalid", "Error", System.Windows.MessageBoxImage.Error);
                }
            }
            if (aurthor != null)
            {
                targetGraph.Assert(fileParseActionNode, targetGraph.CreateUriNode(Properties.Settings.Default.ProvWasAttributedTo), aurthor);
            }

            return fileParseActionNode;
        }

        
        const long saveFreq = 500;
        long fNumber = 0;
        protected virtual void ParseFileDetail(string fName, ref IGraph targetGraph, IUriNode provAction, IFileController fileController,out List<Exception> Errors, MainWindowViewModel toUpdate,string outputFormat)
        {            

            FileWritingThread writer = new FileWritingThread(toUpdate);
            using (MemoryStream memStream = new MemoryStream(File.ReadAllBytes(fName)))//Do the processing in Memory for speedier access
            using (StreamReader sr = new StreamReader(memStream))
            {
                if (!sr.EndOfStream)
                {
                    toUpdate.DisplayMessage("Loading in to memory");
                    fileController.ProcessHeader(sr, ref targetGraph, provAction, fName);
                }
                toUpdate.DisplayMessage("Converting Items");
                IEnumerable<IImportedItem> parsedElments = fileController.ConvertItems(sr, out Errors);
                writer.Start();

                long nElements = parsedElments.LongCount();
                long elmentNumber = 0;
                List<IImportedItem> chunk = new List<IImportedItem>();
                foreach (IImportedItem element in parsedElments)
                {
                    chunk.Add(element);
                    elmentNumber++;
                    if (elmentNumber % saveFreq == 0)
                    {
                        List<IImportedItem> copied = new List<IImportedItem>();
                        copied.AddRange(chunk);
                        chunk.Clear();
                        chunk = new List<IImportedItem>();
                        lock (fNumberLock)
                        {
                            fNumber++;
                        }
                        ThreadWrittenGraph toSave = new ThreadWrittenGraph(outputFormat, copied, fNumber);
                        writer.AddFileToWrite(toSave);                     
                    }
                }
                writer.Stop();
            }

        }

        private object fNumberLock = new object();

        public delegate void MessageDisplayDel(string msg, string title, System.Windows.MessageBoxImage img);
        public event MessageDisplayDel MessageToDisplay;

        protected void OnMessageToDisplay(string msg, string title, System.Windows.MessageBoxImage img)
        {
            MessageToDisplay?.Invoke(msg, title, img);
        }
    }
}
