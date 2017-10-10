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
using System.Windows.Shapes;
using System.Net;
using DataAccess;

namespace ScheduleVis
{
    /// <summary>
    /// Interaction logic for frmStardogDetails.xaml
    /// </summary>
    public partial class frmStardogDetails : Window
    {
        public frmStardogDetails()
        {
            InitializeComponent();
        }

        public static StardogServerDetails GetServerDetails()
        {
            frmStardogDetails toShow = new frmStardogDetails();
            toShow.ShowDialog();
            if (!toShow.DialogResult ?? false)
                return new StardogServerDetails();
            else            
                return toShow.ServerDetails;              
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }


        private void btnDone_Click(object sender, RoutedEventArgs e)
        {
            foreach (var contr in this.grdOutter.Children)
            {
                TextBox control = contr as TextBox;
                if (control != null)
                {
                    control.Background = (Brush)this.Resources["TextboxNormalKey"];
                    control.Foreground = (Brush)this.Resources["TextboxNormalTextKey"];
                }
            }
            ServerDetails = new StardogServerDetails(
                    this.txtServer.Text,
                    this.txtOnt.Text,
                    this.txtUser.Text,
                    this.txtPassword.Text);
            List<string> controlsInError;
            if (ServerDetails.Valid(out controlsInError))
            {
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                foreach (string contr in controlsInError)
                {
                    TextBox control = (TextBox)this.FindName(contr);//it's all textboxes
                    control.Background = (Brush)this.Resources["TextboxErrorKey"];
                    control.Foreground = (Brush)this.Resources["TextboxErrorTextKey"];
                    control.Focus();
                }
            }
            
        }

        public StardogServerDetails ServerDetails;
    }
}
