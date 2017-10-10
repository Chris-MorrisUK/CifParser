using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VDS.RDF;

namespace ScheduleVis.BO
{
    public class TiplocItemBase: IImportedItem
    {
        string tiploc;
        public string Tiploc
        {
            get { return tiploc; }
            set { tiploc = value; }
        }
        protected IUriNode locationNode;

        public virtual void PopulateFromLine(string line)
        {
            throw new NotImplementedException("Tiplocs appear in different places in different item times - it's not possible to parse them here");
        }

        public virtual void SaveToGraph(VDS.RDF.IGraph target, VDS.RDF.IUriNode provCreatingAction)
        {
            createLocationNode(target);
            //assert that it is a location
            target.Assert(locationNode, UriNodeExt.RdfType(target), target.CreateUriNode(Properties.Settings.Default.Location));
            //assert that it is a tiploc location
            target.Assert(locationNode, UriNodeExt.RdfType(target), target.CreateUriNode(Properties.Settings.Default.TiplocLocation));
            //link to the tiploc code
            ILiteralNode tiplockCodeNode = target.CreateLiteralNode(Tiploc);
            IUriNode tiplockUriNode = target.CreateUriNode(Properties.Settings.Default.tiplocCode);
            target.Assert(locationNode, tiplockUriNode, tiplockCodeNode);
            //set the id
            locationNode.IdentifyNode(tiplockCodeNode);
            //lastly, do the prov
            if (provCreatingAction != null)
                locationNode.AssertResponibility(provCreatingAction);
        }

        protected void createLocationNode(VDS.RDF.IGraph target)
        {
            //Tiploc related information
            locationNode = target.CreateUriNode(GenerateTIPLOCUri(Tiploc));
        }

        protected static Uri GenerateTIPLOCUri(string tiploc)
        {
            string res = Properties.Settings.Default.ResourceBaseURI + tiploc;
            return UriFactory.Create(res);
        }
    }
}
