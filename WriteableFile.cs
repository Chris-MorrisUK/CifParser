using ScheduleVis.BO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using VDS.RDF;

namespace ScheduleVis
{
    public class ThreadWrittenGraph
    {
        public ThreadWrittenGraph(string FileNameFormat, List<IImportedItem> _itemsToWrite,long _fileNumber)
        {
            Started = false;
            Finished = false;
            fileNameFormat = FileNameFormat;
            itemsToWrite = _itemsToWrite;
            fileNumber = (long)_fileNumber;
        }

        public Exception Save()
        {
            try
            {
                Started = true;
                string fName = string.Format(fileNameFormat, fileNumber);
                IGraph graphToWrite = new Graph();
                graphToWrite.BaseUri = UriFactory.Create(Properties.Settings.Default.StationGraphBase);
                ontovis.Util.AddNamesSpaces(graphToWrite);
                foreach (IImportedItem element in itemsToWrite)
                {
                    element.SaveToGraph(graphToWrite, null);
                }

                int instance = 0;
                while (File.Exists(fName))
                {
                    fName = string.Format(fileNameFormat, (fileNumber.ToString() + "_" + instance++.ToString()));
                }

                graphToWrite.SaveToFile(fName);
                resetEverything(graphToWrite);
                
            }
            catch (Exception exp)
            {
                return exp;
            }
            finally
            {                
                Finished = true;
            }
            return null;
        }

        private void resetEverything(IGraph graphToWrite)
        {
            
                //int nItems = itemsToWrite.Count;
                //for (int idx = nItems - 1; idx > 0; idx--)
                //{
                //    itemsToWrite[idx] = null;
                //}
                graphToWrite.Clear();
                graphToWrite = null;
               // itemsToWrite = null;
                UriFactory.Clear();
                GC.Collect();            
        }

        private string fileNameFormat;
        private List<IImportedItem> itemsToWrite;
        public volatile bool Started;
        public volatile bool Finished;
        public long fileNumber;
    }
}
