using System;
using System.Collections.Generic;
using MiddleWareBussinessObjects.LDLFileBO;
using System.Text;
using VDS.RDF;

namespace ScheduleVis.BO
{
    /// <summary>
    /// This correlates with a basic schedule i.e. BS record in the CIF data
    /// </summary>
    public class ScheduledRoute : IImportedItem//, IGraphConvertable
    {
        string trainUID;
        WeekDay daysRunning;
        DateTime startDate;
        DateTime endDate;
        ScheduleVis.BO.BankHolidays.BankHolidayRunningDays bankHolidayRunning;
        char status, portion_id, sleepers, reservations, stp_indicator;
        string categary,train_identity, headcode, service_code, power_type, timing_load, speed, operating_characteristics;//, train_class,  catering_code, service_branding;

        List<ServiceNodeBase> serviceNodes = new List<ServiceNodeBase>();

        public List<ServiceNodeBase> ServiceNodes
        {
            get { return serviceNodes; }
            set { serviceNodes = value; }
        }
        //These are not from the BS record
      //  string uic_code, atoc_code, ats_code, rsid, data_source;


        public char Stp_indicator
        {
            get { return stp_indicator; }
            set { stp_indicator = value; }
        }

        public char Reservations
        {
            get { return reservations; }
            set { reservations = value; }
        }

        public char Sleepers
        {
            get { return sleepers; }
            set { sleepers = value; }
        }

        public string Operating_characteristics
        {
            get { return operating_characteristics; }
            set { operating_characteristics = value; }
        }

        public string Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        public string Timing_load
        {
            get { return timing_load; }
            set { timing_load = value; }
        }

        public string Power_type
        {
            get { return power_type; }
            set { power_type = value; }
        }

        public char Portion_id
        {
            get { return portion_id; }
            set { portion_id = value; }
        }
        public string Service_code
        {
            get { return service_code; }
            set { service_code = value; }
        }

        public string Headcode
        {
            get { return headcode; }
            set { headcode = value; }
        }

        public string Train_identity
        {
            get { return train_identity; }
            set { train_identity = value; }
        }
        public string Categary
        {
            get { return categary; }
            set { categary = value; }
        }

        public char Status
        {
            get { return status; }
            set { status = value; }
        } 

        public ScheduleVis.BO.BankHolidays.BankHolidayRunningDays BankHolidayRunning
        {
            get { return bankHolidayRunning; }
            set { bankHolidayRunning = value; }
        }
        
        public DateTime EndDate
        {
            get { return endDate; }
            set { endDate = value; }
        }

        public DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }

        public WeekDay DaysRunning
        {
            get { return daysRunning; }
            set { daysRunning = value; }
        }

        public string TrainUID
        {
            get { return trainUID; }
            set { trainUID = value; }
        }

        public void PopulateFromLine(string line)
        {
            //0...1 = record type, BS
            //2 = N /U / D for new / update delete
            TrainUID = line.Substring(3, 6);
            StartDate = DateTime.ParseExact(line.Substring(9, 6), dateFormat, ProgramState.Provider);
            EndDate = DateTime.ParseExact(line.Substring(15, 6), dateFormat, ProgramState.Provider);
            daysRunning = WeekDay.None;
            if (line[21] == '1')
                daysRunning |= WeekDay.Monday;
            if (line[22] == '1')
                daysRunning |= WeekDay.Tuesday;
            if (line[23] == '1')
                daysRunning |= WeekDay.Wednesday;
            if (line[24] == '1')
                daysRunning |= WeekDay.Thursday;
            if (line[25] == '1')
                daysRunning |= WeekDay.Friday;
            if (line[26] == '1')
                daysRunning |= WeekDay.Saturday;
            if (line[27] == '1')
                daysRunning |= WeekDay.Sunday;
            BankHolidayRunning = BankHolidays.ParseFromChar(line[28]);
            Status = line[29];
            Categary = line.Substring(30, 2);
            Train_identity = line.Substring(32, 4);
            Headcode = line.Substring(36, 4);
            Service_code = line.Substring(41, 8);
            Portion_id = line[49];
            Power_type = line.Substring(50, 3);
            Timing_load = line.Substring(53, 4);
            Speed = line.Substring(57, 3);
        }

      

        public void SaveToGraph(VDS.RDF.IGraph target, VDS.RDF.IUriNode provCreatingAction)
        {
            
                createIdentityNode(target, provCreatingAction);
            //Set the type
            target.Assert(IdentityNode, UriNodeExt.RdfType(target), target.CreateUriNode(UriFactory.Create(Properties.Settings.Default.ScheduledService)));
            IdentityNode.IdentifyNode(target.CreateLiteralNode(TrainUID));
            IUriNode serviceNodePredicate = target.CreateUriNode(Properties.Settings.Default.serviceNodePredicate);
            //TODO: most of the save to graph!
            foreach (ServiceNodeBase node in ServiceNodes)
            {
                //Note that order is crucail here: parent must be set before you try to save to graph and serviceAsNode will be null and afterwards
                //This feels some what fragile and probably needs a good look taking at it
                node.Parent = this;

                    node.SaveToGraph(target, provCreatingAction);
                target.Assert(IdentityNode, serviceNodePredicate, node.ServiceAsNode);
            }
        }

        private void createIdentityNode(IGraph target, IUriNode provCreatingAction)
        {
            string myUriStr = Properties.Settings.Default.ResourceBaseURI + "Service_trainUID_" + TrainUID;
            IdentityNode = target.CreateUriNode(UriFactory.Create(myUriStr));
            if(provCreatingAction != null)
                IdentityNode.AssertResponibility(provCreatingAction);
            
        }

        public IEnumerable<string> GetAsSparqlLines()
        {
            List<string> sparqlLines = new List<string>();
            string idNode = "<" + LDLUris.RailwayTrainStr + "#" + TrainUID + ">";
            string typeLine = idNode + " <" + LDLUris.RDFTypeStr + "> <" + LDLUris.RailwayTrainStr +">";
            sparqlLines.Add(typeLine);
            string destLine = idNode + " <" + LDLUris.RDFTypeStr + "> <" + LDLUris.RailwayTrainStr + ">";
            sparqlLines.Add(typeLine);
            return sparqlLines;

        }

        public IUriNode IdentityNode;//This may at some point land up in an interface, but I can't currently see a compelling reason for it
        private const string dateFormat = "yyMMdd";
    }
}
