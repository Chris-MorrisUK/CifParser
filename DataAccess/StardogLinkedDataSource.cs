using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VDS.RDF;
using VDS.RDF.Storage;
using VDS.RDF.Query;

namespace DataAccess
{
    public class StarDogLinkedDataSource
    {

        private readonly IGraph root;
        private readonly StardogConnector theConnector;


        public StarDogLinkedDataSource(StardogServerDetails details)
            : this(details.URI, details.KnowledgeBase, new Credential(details.UserName, details.Password))
        { }

        public StarDogLinkedDataSource(string uri, string db, Credential cred)
        {
            root = new Graph();
            theConnector = new StardogConnector(uri, db, cred.User, cred.Pass);
            

        }
        public IGraph GetSparlAsGraph(SparqlQuery query)
        {
            return GetSparlAsGraph(query.ToString());
        }

        public IGraph GetSparlAsGraph(string query)
        {
            if (!query.Contains("CONSTRUCT"))
            {
                throw new ArgumentException("Only construct querries can be shown as graphs");
            }
            var res = theConnector.Query(query);
            return  res as IGraph;
        }

        public string GetSparlAsString(string query)
        {
            StringBuilder toDisplay = new StringBuilder();
            IEnumerable<SparqlResult> allRes = Query(query);
            if (allRes.Count<SparqlResult>() == 0)
                return "No Results";
            foreach (SparqlResult res in allRes)
            {
                foreach (KeyValuePair<string, INode> v in res)
                {
                    string valueString;
                    if (v.Value != null)
                        valueString = v.Value.ToString();
                    else
                        valueString = "(No Value)";
                    toDisplay.AppendLine(v.Key + " = " + valueString);
                }

            }
            return toDisplay.ToString();
        }

        public IEnumerable<SparqlResult> Query(string sparql)
        {
            if (theConnector == null)
                throw new InvalidOperationException("Stardog must be initialised before using queries");
            SparqlResultSet res = theConnector.Query(sparql) as SparqlResultSet;
            return res.Results;
        }
    }
}
