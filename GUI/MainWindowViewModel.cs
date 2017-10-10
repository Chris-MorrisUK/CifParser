using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace ScheduleVis
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public MainWindowViewModel()
        {
            outputText = "Ready to Parse files";
            progressVisible = false;
        }


        /// <summary>
        /// Updates the progress bar
        /// </summary>
        /// <param name="fractionDoneOver255"> A number between 0 and 255 showing the current progress, within the range of byte
        /// /// </param>
        public void DoWorkStep(byte fractionOf255done)
        {
            Progress = fractionOf255done;
            ProgressVisible = true;
        }
        public void Complete(string message)
        {
            DisplayMessage("Task Complete");
            ProgressVisible = false;
        }

        private bool progressVisible;
        private string outputText;
        private double progress;

        public void DisplayMessage(string msg)
        {
            outputText += DateTime.Now.ToLongTimeString() + " - " +  msg + Environment.NewLine;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("OutputText"));
        }

        public void AddError(Exception exp,bool update=true)
        {
            outputText += exp.ToString() + Environment.NewLine;
            outputText += "at:" + Environment.NewLine;
            outputText += exp.StackTrace.ToString() + Environment.NewLine;
            if (exp.InnerException!=null)
                outputText += exp.InnerException.ToString() + Environment.NewLine;
            if(update)
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("OutputText"));
        }

        public void AddErrorBulk(List<Exception> errors)
        {
            foreach (Exception exp in errors)
                AddError(exp, false);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("OutputText"));
        }

        
        public string OutputText
        {
            get
            {
                return outputText;
            }
        }

        public bool ProgressVisible
        {
            get
            {
                return progressVisible;
            }

            set
            {
                if (value != progressVisible)
                {
                    progressVisible = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ProgressVisible"));
                }
            }
        }
        public double Progress
        {
            get
            {
                return progress;
            }

            set
            {
                if (progress != value)
                {
                    progress = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Progress"));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
