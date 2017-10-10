using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VDS.RDF;
using VDS.RDF.Parsing;

namespace ScheduleVis.BO
{
    public class ServiceNodeBase:TiplocItemBase, IImportedItem
    {

        public ServiceNodeBase()
        {
            activies = new ActiviesCollection();
        }
        #region private members

        private int callNumber;
        string platform, line, path;
        ActiviesCollection activies;
        DateTime? passingTime;
        DateTime? publicDep, scheduledDep;
        DateTime? publicArrival, scheduledArrival;
        TimeSpan? pathing, engineering, performance;
        ScheduledRoute parent;
        IUriNode thisServiceNode;

        public IUriNode ServiceAsNode
        {
            get { return thisServiceNode; }
        }
        #endregion

        #region public accessors

        public ScheduledRoute Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        public ActiviesCollection Activies
        {
            get { return activies; }
            set { activies = value; }
        }

        public string Line
        {
            get { return line; }
            set { line = value; }
        }

        public string Platform
        {
            get { return platform; }
            set { platform = value; }
        }

        public string Path
        {
            get { return path; }
            set { path = value; }
        }

        public DateTime? PassingTime
        {
            get { return passingTime; }
            set { passingTime = value; }
        }

        public DateTime? PublicArrival
        {
            get { return publicArrival; }
            set { publicArrival = value; }
        }

        public DateTime? ScheduledArrival
        {
            get { return scheduledArrival; }
            set { scheduledArrival = value; }
        }
        public DateTime? ScheduledDep
        {
            get { return scheduledDep; }
            set { scheduledDep = value; }
        }

        public DateTime? PublicDep
        {
            get { return publicDep; }
            set { publicDep = value; }
        }

        public TimeSpan? Performance
        {
            get { return performance; }
            set { performance = value; }
        }

        public TimeSpan? Engineering
        {
            get { return engineering; }
            set { engineering = value; }
        }

        public TimeSpan? Pathing
        {
            get { return pathing; }
            set { pathing = value; }
        }

        public int CallNumber
        {
            get { return callNumber; }
            set { callNumber = value; }
        }

#endregion

        public override void PopulateFromLine(string line)
        {
            Tiploc = line.Substring(2, 8).TrimEnd();
        }


        public override void SaveToGraph(VDS.RDF.IGraph target, VDS.RDF.IUriNode provCreatingAction)
        {
            /*DO NOT call base.SaveToGraph at this juncture
             * It will save it as if it's a tiploc
             * and it's not, it merely has one as a location
             * */
            //Create the identifying node for this service
            thisServiceNode = createSerivceNode(target/*,provCreatingAction*/);
            //set the location
            base.createLocationNode(target);
            IUriNode locationLink = target.CreateUriNode(Properties.Settings.Default.locationPredicate);
            target.Assert(thisServiceNode, locationLink, locationNode);
            //save the calling point number
            IUriNode order = target.CreateUriNode(Properties.Settings.Default.NodeOrder);
            ILiteralNode callingPointNode = CallNumber.ToLiteral(target);
            target.Assert(thisServiceNode, order, callingPointNode);

            //platform
            if (!string.IsNullOrWhiteSpace(Platform))
            {
                IUriNode hasaplatform = target.CreateUriNode(Properties.Settings.Default.Platform);
                target.Assert(thisServiceNode, hasaplatform, platform.ToLiteral(target));
            }
            //public arrival
            if (PublicArrival.HasValue)
            {
                IUriNode haspublicArrivalTime = target.CreateUriNode(Properties.Settings.Default.PublicArrival);
                ILiteralNode pubArrivalTime = target.CreateLiteralNode(PublicArrival.Value.ToString(XmlSpecsHelper.XmlSchemaTimeFormat),UriFactory.Create( XmlSpecsHelper.XmlSchemaDataTypeTime));
                target.Assert(thisServiceNode, haspublicArrivalTime, pubArrivalTime);
            }            
            //scheduled arrival
            if (ScheduledArrival.HasValue)
            {
                IUriNode hasArrivalTime = target.CreateUriNode(Properties.Settings.Default.ttArrival);
                ILiteralNode arrivalTime = target.CreateLiteralNode(ScheduledArrival.Value.ToString(XmlSpecsHelper.XmlSchemaTimeFormat), UriFactory.Create(XmlSpecsHelper.XmlSchemaDataTypeTime));
                target.Assert(thisServiceNode, hasArrivalTime, arrivalTime);
            }
            //passing time
            if (PassingTime.HasValue)
            {
                IUriNode hasPassingTime = target.CreateUriNode(Properties.Settings.Default.PassingTime);
                ILiteralNode passingTime = target.CreateLiteralNode(PassingTime.Value.ToString(XmlSpecsHelper.XmlSchemaTimeFormat), UriFactory.Create(XmlSpecsHelper.XmlSchemaDataTypeTime));
                target.Assert(thisServiceNode, hasPassingTime, passingTime);
            }
            //Departure Time 
            if (ScheduledDep.HasValue)
            {
                IUriNode hasdeptTime = target.CreateUriNode(Properties.Settings.Default.ttDeparture);
                ILiteralNode deptTime = target.CreateLiteralNode(ScheduledDep.Value.ToString(XmlSpecsHelper.XmlSchemaTimeFormat), UriFactory.Create(XmlSpecsHelper.XmlSchemaDataTypeTime));
                target.Assert(thisServiceNode, hasdeptTime, deptTime);
            }
            //Public Depture Time 
            if (PublicDep.HasValue)
            {
                IUriNode hasdeptTime = target.CreateUriNode(Properties.Settings.Default.PublicDeparture);
                ILiteralNode deptTime = target.CreateLiteralNode(PublicDep.Value.ToString(XmlSpecsHelper.XmlSchemaTimeFormat), UriFactory.Create(XmlSpecsHelper.XmlSchemaDataTypeTime));
                target.Assert(thisServiceNode, hasdeptTime, deptTime);
            }
            //line
            if (!string.IsNullOrWhiteSpace(Line))
            {
                IUriNode hasaLine = target.CreateUriNode(Properties.Settings.Default.Line);
                target.Assert(thisServiceNode, hasaLine, Line.ToLiteral(target));
            }
            //Path
            if (!string.IsNullOrWhiteSpace(Path))
            {
                IUriNode hasaPath = target.CreateUriNode(Properties.Settings.Default.Path);
                target.Assert(thisServiceNode, hasaPath, Path.ToLiteral(target));
            }
            //Time table fudge factor
            if (Performance.HasValue)
            {
                IUriNode hasaPerformance = target.CreateUriNode(Properties.Settings.Default.Performance);
                target.Assert(thisServiceNode, hasaPerformance, Performance.Value.ToLiteral(target));
            }
            //Engineering Allowance
            if (Engineering.HasValue)
            {
                IUriNode hasaEngineeringAllowance = target.CreateUriNode(Properties.Settings.Default.Engineering);
                target.Assert(thisServiceNode, hasaEngineeringAllowance, Engineering.Value.ToLiteral(target));
            }
        }

        private IUriNode createSerivceNode(IGraph target /*, IUriNode provCreatingAction*/)
        {
            string UriStr = Properties.Settings.Default.ResourceBaseURI + "#tiploc_" + this.Tiploc + "_serviceTrainUID_" + parent.TrainUID;
            Uri myUri = UriFactory.Create(UriStr);
            IUriNode result = target.CreateUriNode(myUri);
            return result;
        }
    }
}
