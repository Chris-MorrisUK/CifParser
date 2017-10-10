using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VDS.RDF;
using VDS.RDF.Parsing;

namespace ScheduleVis.BO
{
    public class Station :TiplocItemBase, IImportedItem//,/* IGraphConvertable,*/ ITiplocLocation
    {
        string stationName;
        int cateType;        
        
        string sub3AlphaCode;
        string main3AlphaCode;
        int easting;
        int northings;
        int changeTime;
        bool locationEstimated;


        #region accessors
        
        public bool LocationEstimated
        {
            get { return locationEstimated; }
            set { locationEstimated = value; }
        }
        public int ChangeTime
        {
            get { return changeTime; }
            set { changeTime = value; }
        }

        public string StationName
        {
            get { return stationName; }
            set { stationName = value; }
        }


        public int Easting
        {
            get { return easting; }
            set { easting = value; }
        }
        public int Northings
        {
            get { return northings; }
            set { northings = value; }
        }

        public int CateType
        {
            get { return cateType; }
            set { cateType = value; }
        }
        public string Sub3AlphaCode
        {
            get { return sub3AlphaCode; }
            set { sub3AlphaCode = value; }
        }
        public string Main3AlphaCode
        {
            get { return main3AlphaCode; }
            set { main3AlphaCode = value; }
        }
        #endregion

        public override void PopulateFromLine(string line)
        {
            string stationName = line.Substring(5, 30).Trim();
            int cateType = int.Parse(line.Substring(35, 1));
            string tiploc = line.Substring(36, 7).Trim();
            string sub3AlphaCode = line.Substring(43, 3);
            string main3AlphaCode = line.Substring(49, 3);
            int easting = int.Parse(line.Substring(52, 5));
            bool locationEstimated = line[57] == 'E';
            int northings = int.Parse(line.Substring(58, 5));
            int changeTime = int.Parse(line.Substring(64, 2));

            this.StationName = stationName;
            this.CateType = cateType;
            this.Tiploc = tiploc;
            this.Sub3AlphaCode = sub3AlphaCode;
            this.Main3AlphaCode = main3AlphaCode;
            this.Easting = easting;
            this.LocationEstimated = locationEstimated;
            this.Northings = northings;
            this.ChangeTime = changeTime;
        }


        public override void SaveToGraph(IGraph target, IUriNode provCreatingAction)
        {
            base.SaveToGraph(target, provCreatingAction);
            //General station stuff
            //Label the node
            locationNode.LabelNode(stationName, Properties.Settings.Default.Language);
            //set the eastings and Northings
            IUriNode eastingsDescNode = target.CreateUriNode(Properties.Settings.Default.Eastings);
            IUriNode NorthingsDescNode = target.CreateUriNode(Properties.Settings.Default.Northings);
            ILiteralNode eastingsValueNode = target.CreateLiteralNode(easting.ToString(), UriFactory.Create(XmlSpecsHelper.XmlSchemaDataTypeDouble));
            ILiteralNode northingssValueNode = target.CreateLiteralNode(northings.ToString(), UriFactory.Create(XmlSpecsHelper.XmlSchemaDataTypeDouble));
            target.Assert(locationNode, eastingsDescNode, eastingsValueNode);
            target.Assert(locationNode, NorthingsDescNode, northingssValueNode);            
        }

        
    }
}
