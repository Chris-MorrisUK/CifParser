using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DataAccess;
using System.Threading;
using VDS.RDF;
using System.Threading.Tasks;
using VDS.RDF.Parsing;
using System.IO;
using System.Globalization;
using ScheduleVis.BO;
using System.ComponentModel;

namespace ScheduleVis
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IWindowWithProgress
    {
        
        BackgroundWorker worker;

        public MainWindow()
        {
            InitializeComponent();
            worker = new BackgroundWorker();
            worker.DoWork += combinedImportWorker;
        }


        private MainWindowViewModel viewModel
        {
            get
            {
                MainWindowViewModel result = null;
                this.Dispatcher.Invoke(new Action(() =>
                { result = this.DataContext as MainWindowViewModel; }));

                return result;
            }
            set
            {
                this.Dispatcher.Invoke(new Action(() =>
                { this.DataContext = value; }));
                
            }
        }


        private StarDogLinkedDataSource theDataSource
        {
            get
            {
                return ProgramState.TheDataSource;
            }
            set
            {
                ProgramState.TheDataSource = value;
            }
        }

        private void importStationList_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openDlg = new Microsoft.Win32.OpenFileDialog();
            openDlg.DefaultExt = ".msn";
            openDlg.Filter = "Master Station Name List (msn)|*.msn";
            openDlg.Title = "Station List to import";
            if (openDlg.ShowDialog(this) == true)
            {
                IFileController cntrl = new StationFileControl();
                FileParseBase parser = new FileParseBase();
                parser.MessageToDisplay += new FileParseBase.MessageDisplayDel(parser_MessageToDisplay);
                ProvInfo provInfo = new ProvInfo(txtName.Text, this.rbUri.IsChecked == true);
                List<Exception> Errors;
                IGraph stationNameGraph = parser.ParseFile(openDlg.FileName, provInfo, cntrl, out Errors,this.viewModel,Properties.Settings.Default.fNameFormat );
                saveGraphToTurtle(stationNameGraph);
                saveGraphToRDF(stationNameGraph);
            }

        }

        void parser_MessageToDisplay(string msg, string title, MessageBoxImage img)
        {
            displayMessage(msg, title, img);
        }

        private void saveGraphToTurtle(IGraph graphToSave)
        {
            Microsoft.Win32.SaveFileDialog saveDlg = new Microsoft.Win32.SaveFileDialog();
            saveDlg.DefaultExt = ".ttl";
            saveDlg.Filter = "Turtle Files|*.ttl";
            saveDlg.Title = "Output file";
            if (saveDlg.ShowDialog(this) == true)
            {
                graphToSave.SaveToFile(saveDlg.FileName);
            }
        }

        private void saveGraphToRDF(IGraph graphToSave)
        {
            Microsoft.Win32.SaveFileDialog saveDlg = new Microsoft.Win32.SaveFileDialog();
            saveDlg.DefaultExt = ".OWL";
            saveDlg.Filter = "OWL Files|*.OWL";
            saveDlg.Title = "Output file";
            if (saveDlg.ShowDialog(this) == true)
            {
                graphToSave.SaveToFile(saveDlg.FileName);
            }
        }



        private Uri generateTIPLOCUri(string tiploc)
        {
            string res = Properties.Settings.Default.ResourceBaseURI + tiploc;
            return UriFactory.Create(res);
        }





        //Saves invoking it every time, also allows for logging in the future, should it be deemed usefull
        private void displayMessage(string msg, string title, MessageBoxImage img)
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                MessageBox.Show(msg, title, MessageBoxButton.OK, img);
            }));
        }



        private void btnImportSchedules_Click(object sender, RoutedEventArgs e)
        {

            Microsoft.Win32.OpenFileDialog openDlg = new Microsoft.Win32.OpenFileDialog();
            openDlg.DefaultExt = ".mca";
            openDlg.Filter = "Complete schedule file|*.mca";
            openDlg.Title = "Schedule to Import";
            if (openDlg.ShowDialog(this) == true)
            {
                IFileController cntrl = new ScheduleFileControl();
                FileParseBase parser = new FileParseBase();
                parser.MessageToDisplay += new FileParseBase.MessageDisplayDel(parser_MessageToDisplay);
                ProvInfo provInfo = new ProvInfo(txtName.Text, this.rbUri.IsChecked == true);
                List<Exception> Errors;
                IGraph stationNameGraph = parser.ParseFile(openDlg.FileName, provInfo, cntrl, out Errors,this.viewModel, Properties.Settings.Default.ScheduleFNameFormat);
                saveGraphToTurtle(stationNameGraph);
            }
        }
        private struct CominedImportArgs
        {
            public CominedImportArgs(string stationName, string schedule, ProvInfo prov)
            {
                StationNameList = stationName;
                ScheduleFile = schedule;
                Prov = prov;               
            }
            public string StationNameList;
            public string ScheduleFile;
            public ProvInfo Prov;
            
        }
        private void btnCombinedImport_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openDlg = new Microsoft.Win32.OpenFileDialog();
            openDlg.DefaultExt = ".msn";
            openDlg.Filter = "Master Station Name List (msn)|*.msn";
            openDlg.Title = "Station List to import";
            if (openDlg.ShowDialog(this) == true)
            {
                openDlg.DefaultExt = ".cif";
                openDlg.Filter = "Complete schedule file|*.cif";
                openDlg.Title = "Schedule to Import";
                string stationName = openDlg.FileName;
                if (openDlg.ShowDialog(this) == true)
                {
                    ProvInfo provInfo = null;
                    if (chkIncludeProv.IsChecked.Value)
                    {
                        provInfo = new ProvInfo(txtName.Text, this.rbUri.IsChecked == true);
                    }
                    CominedImportArgs toPass = new CominedImportArgs(stationName,openDlg.FileName,provInfo);                    
                    worker.RunWorkerAsync(toPass);
                }
            }
        }

        private void combinedImportWorker(object sender, DoWorkEventArgs e)
        {
            combinedImport(e.Argument);
        }

        private void combinedImport(object args)
        {
            IFileController cntrl = new StationFileControl();

            FileParseBase parser = new FileParseBase();
            parser.MessageToDisplay += new FileParseBase.MessageDisplayDel(parser_MessageToDisplay);
            this.viewModel = new MainWindowViewModel();
            List<Exception> Errors;
            IGraph combinedGraph = parser.ParseFile(((CominedImportArgs)args).StationNameList, ((CominedImportArgs)args).Prov, cntrl, out Errors,this.viewModel, Properties.Settings.Default.fNameFormat);
            IFileController scheduledCntrl = new ScheduleFileControl();
            List<Exception> ErrorsTwo;
            IGraph resultingGraph = parser.ParseFile(((CominedImportArgs)args).ScheduleFile, ((CominedImportArgs)args).Prov, scheduledCntrl, combinedGraph, out ErrorsTwo,this.viewModel, Properties.Settings.Default.ScheduleFNameFormat );
            //Dispatcher.BeginInvoke(new Action(() =>
            //{
            //    saveGraphToTurtle(resultingGraph);
            //}));
            //  saveGraphToRDF(resultingGraph);


        }

        private void btnSaveOutput_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog svDlg = new Microsoft.Win32.SaveFileDialog();
            svDlg.OverwritePrompt = true;
            svDlg.Title = "Save output text location";
            if (svDlg.ShowDialog().Value == true)
            {
                File.WriteAllText(svDlg.FileName, (this.DataContext as MainWindowViewModel).OutputText);
            }
        }
    }
}
