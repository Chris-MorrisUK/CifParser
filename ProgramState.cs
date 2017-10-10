using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess;
using VDS.RDF.Query;
using VDS.RDF;
using System.Globalization;

namespace ScheduleVis
{
    public static class ProgramState
    {
        public static StarDogLinkedDataSource TheDataSource;

        public static void SetDotNetRDFOptions()
        {
            Options.FullTripleIndexing = false;
            Options.InternUris = false;
        }

        public static void AddCommonNamespaces(SparqlParameterizedString queryString)
        {
            queryString.Namespaces.AddNamespace("tt", UriFactory.Create("http://purl.org/rail/tt/"));
            queryString.Namespaces.AddNamespace("rdfs", UriFactory.Create("http://www.w3.org/2000/01/rdf-schema#"));
        }

        //massive performance hit if you re-create this each time you need it, hence it is now only created once
        public static CultureInfo Provider
        {
            get
            {
                if (provider == null)
                    provider = CultureInfo.CreateSpecificCulture(Properties.Settings.Default.Language);
                return provider;
            }
        }

        private static CultureInfo provider;
        public static ScheduleVis.BO.Activity PossibleActivities;
    }
}
