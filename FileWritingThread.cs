using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ScheduleVis
{
    public class FileWritingThread
    {
       /* //Static bits for singulton
        static FileWritingThread ThreadController; */


        public FileWritingThread(MainWindowViewModel toUpdate)
        {
            ThreadPool.SetMaxThreads(16, 16);
            moreToAdd = true;
            toWrite = new List<ThreadWrittenGraph>();
            vmToUpdate = toUpdate;
            ThreadStart controlThreadStart = new ThreadStart(this.writeFilesAsNeeded);
            controlThread = new Thread(controlThreadStart);
            controlThread.Name = "File writing control thread";
            controlThread.IsBackground = true;            
        }
        

        private volatile bool moreToAdd;
        private List<ThreadWrittenGraph> toWrite;
        private const int Number_Of_Simulationous_Writes = 12;
        Thread controlThread;        
        private const int Thread_Sleep_Time_MS = 50;
        

        //GUI related
        MainWindowViewModel vmToUpdate;
        /// <summary>
        /// The Most items ever in the list
        /// </summary>
        private int maxLength = 0;
        /// <summary>
        /// The number removed from the list
        /// </summary>
        private int savesComplete = 0;

        /* public static void StartProcessingList(List<ThreadWrittenGraph> graphsToSave, IWindowWithProgress toUpdate)
         {
             //No concurrancy issue here: it gets call once
             ThreadController = new FileWritingThread(graphsToSave, toUpdate);
             ThreadController.Start();
         }
         */

        public void AddFileToWrite(ThreadWrittenGraph toAdd)
        {
            lock (toWrite)
                {
                toWrite.Add(toAdd);
            }
        }
        public void Start()
        {
            vmToUpdate.DisplayMessage("Starting file write");
            if (controlThread == null)
                throw new InvalidOperationException("Managed to start without creating control thread.");
            controlThread.Start();
        }

        public void Stop()
        {
            vmToUpdate.DisplayMessage("Stopping...");///may not be displayed for long - may go back to listing numbers
            moreToAdd = false;
            controlThread.Join();
            vmToUpdate.DisplayMessage("Complete");
        }

        private void writeFilesAsNeeded()
        {
            int nRunning = 0;
            while ((moreToAdd)
               ||(toWrite.Count > 0)
                )
            {
                //Just for display purposes
                if (toWrite.Count > maxLength)
                    maxLength = toWrite.Count;

                lock (toWrite)//if the collection changes mid loop it will cause an exception
                {
                    foreach (ThreadWrittenGraph graphToCheck in toWrite)
                    {
                        if (((!graphToCheck.Started)
                            && (!graphToCheck.Finished))
                            && (nRunning < Number_Of_Simulationous_Writes))
                        {
                            nRunning++;
                            ThreadPool.QueueUserWorkItem(
                     o =>
                     {
                         Exception err = graphToCheck.Save();
                         if(err!=null)
                            vmToUpdate.AddError(err);
                     });

                        }
                    }

                    List<ThreadWrittenGraph> GraphsToGo = new List<ThreadWrittenGraph>();
                    foreach (ThreadWrittenGraph graphToCheck in toWrite)
                    {
                        if ((graphToCheck.Started) && (graphToCheck.Finished))
                        {
                            GraphsToGo.Add(graphToCheck);
                            nRunning--;
                            double progress = (double)savesComplete++ / (double)maxLength;
                            vmToUpdate.DoWorkStep((byte)Math.Round(progress * 255));
                            vmToUpdate.DisplayMessage(savesComplete.ToString() + " Files saved");
                        }
                    };


                    foreach (ThreadWrittenGraph graph in GraphsToGo)
                        toWrite.Remove(graph);
                }
                Thread.Sleep(Thread_Sleep_Time_MS);
            }
        }
    }
}
