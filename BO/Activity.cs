using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScheduleVis.BO
{
    public class Activity
    {
        public Activity()
        {
            Activities = new Dictionary<string, string>();
            populate();
        }

        private void populate()
        {
            Activities.Add("A", "Stops or Shunts for other trains to pass");
            Activities.Add("AE","Attach/detach assisting locomotive");
            Activities.Add("BL","Stops for banking locomotive");
            Activities.Add("C","Stops to change trainmen");
            Activities.Add("D", "Stops to set down passengers");
            Activities.Add("-D", "Stops to detach vehicles");
            Activities.Add("E","Stops for examination");
            Activities.Add("G","National Rail Timetable data to add");
            Activities.Add("H","Notional activity to prevent WTT timing columns merge");
            Activities.Add("HH","As H, where a third column is involved");
            Activities.Add("K","Passenger count point");
            Activities.Add("KC","Ticket collection and examination point");
            Activities.Add("KE", "Ticket examination point");
            Activities.Add("KF","Ticket Examination Point, 1st Class only");
            Activities.Add("KS","Selective Ticket Examination Point");
            Activities.Add("L","Stops to change locomotives");
            Activities.Add("N","Stop not advertised");
            Activities.Add("OP","Stops for other operating reasons");
            Activities.Add("OR","Train Locomotive on rear");
            Activities.Add("PR","Propelling between points shown");
            Activities.Add("R","Stops when required");
            Activities.Add("RM","Reversing movement, or driver changes ends");
            Activities.Add("RR","Stops for locomotive to run round train");
            Activities.Add("S","Stops for railway personnel only");
            Activities.Add("T","Stops to take up and set down passengers");
            Activities.Add("-T","Stops to attach and detach vehicles");
            Activities.Add("TB","Train begins (Origin)");
            Activities.Add("TF","Train finishes (Destination)");            
            Activities.Add("TS","Detail Consist for TOPS Direct requested by EWS");
            Activities.Add("TW","Stops (or at pass) for tablet, staff or token.");
            Activities.Add("U","Stops to take up passengers");
            Activities.Add("-U","Stops to attach vehicles");
            Activities.Add("W","Stops for watering of coaches");
            Activities.Add("X", "Passes another train at crossing point on single line");
        }
            public Dictionary<string, string> Activities;
    }

  
}
