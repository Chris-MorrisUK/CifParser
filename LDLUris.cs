using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWareBussinessObjects.LDLFileBO
{
    public static class LDLUris
    {
        public static Uri ItemIDUri
        {
            get
            {//using the dublin core term rather than reinvent the wheel
                return new Uri("http://purl.org/dc/elements/1.1/identifier");
            }
        }

        public static Uri TrackClassUri
        {
            get
            {
                return new Uri(TrackClassString);
            }
        }

        public static  string RDFTypeStr
        {
            get
            {
                return "http://www.w3.org/1999/02/22-rdf-syntax-ns#type";
            }
        }
        public static Uri RDFType
        {
            get
            {
                return new Uri(RDFTypeStr);
            }
        }

       /* public static BONode RDFTypeNode
        {
            get
            {
                return new BONode(RDFType);
            }
        }*/

        public static Uri LengthProperty
        {
            get
            {
                return new Uri("http://purl.org/rail/core/vocab/length");
            }
        }
        public static Uri nodeProperty
        {
            get
            {
                return new Uri("http://purl.org/rail/core/vocab/node");
            }
        }
        public static Uri Metre
        {
            get
            {
                return new Uri("http://qudt.org/vocab/unit#Meter");
            }
        }
        public static Uri CorssingUri
        {
            get
            {
                return new Uri(Crossing);
            }
        }

        public static string IndependantThing
        {
            get
            {
                return @"http://purl.org/ub/upper/IndependentThing";
            }
        }

        public static string TrackClassString
        {
            get
            {
                return @"http://purl.org/rail/core/vocab/RouteArc";
            }
        }

        public static string Crossing
        {
            get
            {
                return "http://purl.org/rail/core/vocab/Crossing";
            }
        }

        public static Uri arcProperty
        {
            get
            {
                return new Uri( "http://purl.org/rail/core/arc");
            }
        }

        public static string PointsType
        {
            get
            {
                return "http://purl.org/rail/core/Points";
            }

        }

        public static Uri PointsTypeUri
        {
            get
            {
                return new Uri(PointsType);
            }
        }

        public static Uri StartNodeProperty
        {
            get
            {
                return new Uri("http://purl.org/rail/core/startNode");
            }
        }

        public static Uri EndNodeProperty
        {
            get
            {
                return new Uri("http://purl.org/rail/core/endNode");
            }
        }

        /// <summary>
        /// Visual layout only!
        /// </summary>
        public static Uri DiagramXProperty
        {
            get
            {
                return new Uri("http://purl.org/rail/core/diagramX");
            }
        }
        /// <summary>
        /// Visual layout only!
        /// </summary>
        public static Uri DiagramYProperty
        {
            get
            {
                return new Uri("http://purl.org/rail/core/diagramY");
            }
        }

        public static Uri RouteNodeUri
        {
            get
            {
                return new Uri(RouteNodeStr);
            }
        }
        public static string RouteNodeStr
        {
            get
            {
                return "http://purl.org/rail/core/RouteNode";
            }
        }

        public static string Terminus
        {
            get
            {
                return "http://purl.org/rail/core/RouteTerminus";
            }
        }

        public static Uri RouteBoundaryUri
        {
            get
            {
                return new Uri(RouteBoundary);
            }
        }

        public static string RouteBoundary
        {
            get
            {
                return "http://purl.org/rail/core/RouteBoundary";
            }
        }

        public static Uri TerminusURI
        {
            get
            {
                return new Uri(Terminus);
            }
        }

        public static Uri Label
        {
            get
            {
                return new Uri(@"http://www.w3.org/2000/01/rdf-schema#Label");
            }
        }

        public static string InterlockingStr
        {
            get
            {
                return "http://purl.org/rail/core/Interlocking";
            }
        }

        public static Uri InterlockingURI
        {
            get
            {
                return new Uri(InterlockingStr);
            }
        }

        public static string TrackCircuitLocation
        {
            get
            {
                return "http://purl.org/rail/resource/TCLocationALSTONS1";
            }
        }

        public static Uri TrackCircuitLocationUri
        {
            get
            {
                return new Uri(TrackCircuitLocation);
            }
        }

        /// <summary>
        /// To define things that extend along the track e.g. track circuits
        /// </summary>
        public static Uri MaxLocation
        {
            get
            {
                return new Uri("http://purl.org/rail/is/maxLocation");
            }
        }

        public static Uri MinLocation
        {
            get
            {
                return new Uri("http://purl.org/rail/is/minLocation");
            }
        }

        public static Uri LocatedOnProperty
        {
            get
            {
                return new Uri("http://purl.org/rail/core/locatedOn");
            }
        }
        public static string RelativePositionNoun
        {
            get
            {
                return "http://purl.org/ub/upper/RelativePosition";
            }
        }
        public static Uri RelativePositionNounUri
        {
            get
            {
                return new Uri(RelativePositionNoun);
            }
        }
        public static Uri RelativePositionPropertyUri
        {
            get
            {
                return new Uri("http://purl.org/rail/core/relativePosition");
            }
        }

        public static string LinearPositionClassStr
        {
            get
            { 
                return "http://purl.org/rail/core/LinearPosition";
            }
        }
        public static Uri LinearPositionClass
        {
            get
            {
                return new Uri(LinearPositionClassStr);
            }
        }

        public static string BufferStopStr
        {
            get {
                return "http://purl.org/rail/core/BufferStop";
            }
        }

        public static Uri BufferStop
        {
            get
            {
                return new Uri(BufferStopStr);
            }
        }

        public static string RouteStr
        {
            get
            {
                return "http://purl.org/rail/core/Route";
            }
        }

        public static Uri RouteUri
        {
            get
            {
                return new Uri(RouteStr);
            }
        }

        public static Uri MeasurementValueProperty
        {
            get
            {
                return new Uri("http://purl.org/ub/upper/measurementValue");
            }
        }

        public static string MetersLocationClass
        {
            get
            {
                return "http://purl.org/ub/compass#MetersLocation";
            }
        }

        public static Uri MetersLocationClassUri
        {
            get
            {
                return new Uri(MetersLocationClass);
            }
        }


        public static Uri StartTrackPositionProperty
        {
            get
            {
                return new Uri("http://purl.org/rail/core/startTrackPosition");
            }
        }
        public static Uri EndTrackPositionProperty
        {
            get
            {
                return new Uri("http://purl.org/rail/core/endTrackPosition");
            }
        }

        public static Uri RelativePosProperty
        {
            get
            {
                return new Uri("http://purl.org/rail/core/relativePosition");
            }
        }

        public static Uri RouteList
        {
            get
            {
                return new Uri("http://purl.org/rail/core/routeList");
            }
        }

        public static Uri COItemProperty
        {
            get
            {
                return new Uri("http://purl.org/ub/co/item");
            }
        }


        public static Uri InterlockingProperty
        {
            get
            {
                return new Uri("http://purl.org/rail/core/interlocking");
            }
        }

        public static Uri RouteEntranceProperty
        {
            get
            {
                return new Uri("http://purl.org/rail/core/RouteEntrance");
            }
        }
        public static Uri RouteExitProperty
        {
            get
            {
                return new Uri("http://purl.org/rail/core/RouteExit");
            }
        }
        public static string GeodesicLocationStr
        {
            get
            {
                return "http://purl.org/rail/core/GeodesicLocation";
            }
        }

        public static Uri GeodesicLocation
        {
            get
            {
                return new Uri(GeodesicLocationStr);
            }
        }

        public static string FeatureTypeStr
        {
            get
            {
                return "http://www.opengis.net/ont/geosparql#Feature";
            }
        }

        public static Uri FeatureTypeUri
        {
            get
            {
                return new Uri(FeatureTypeStr);
            }
        }

        public static Uri HasGeometeryPropertyUri
        {
            get
            {
                return new Uri("http://www.opengis.net/ont/geosparql#hasGeometry");
            }
        }

        public static Uri LocationProperty
        {
            get
            {
                return new Uri("http://purl.org/ub/upper/location");
            }
        }

        public static Uri OffsetLocationProperty
        {
            get 
            {
                return new Uri("http://purl.org/rail/core/hasOffsetLocation");
            }
        }

        public static string OffsetLocationStr
        {
            get
            { 
                return "http://purl.org/ub/compass#OffsetLocation";
            }
        }

        public static Uri OffsetLocationType
        {
            get
            {
                return new Uri(OffsetLocationStr);
            }
        }
        public static Uri UnitProperty
        {
            get
            {
                return new Uri("http://purl.org/ub/upper/unit");
            }
        }

        public static Uri InXSDTimeProperty
        {
            get
            {
                return new Uri("http://www.w3.org/2006/time#inXSDDateTime");
            }
        }

        public static Uri CompassStateType
        {
            get
            {
                return new Uri(CompassStateTypeStr);
            }
        }
        public static string CompassStateTypeStr
        {
            get
            {
                return "http://purl.org/ub/compass#Compass_State";
            }
        }

        public static Uri CompassStirActiveProperty
        {
            get
            {
                return new Uri("http://purl.org/ub/compass#stirActive");
            }
        }

        public static string CompassABOX
        {
            get
            {
                return "http://purl.org/ub/compassabox/";
            }
        }

        public static string BaliseTypeStr
        {
            get
            {
                return "http://purl.org/rail/core/Balise";
            }
        }

        public static Uri BaliseTypeUri
        {
            get
            {
                return new Uri(BaliseTypeStr);
            }
        }

        public static string BaliseGroupTypeStr
        {
            get
            {
                return "http://purl.org/rail/core/BaliseGroup";
            }
        }

        public static Uri BaliseGroupTypeUri
        {
            get
            {
                return new Uri(BaliseGroupTypeStr);
            }
        }

        public static Uri BaliseFixedProperty
        {
            get {
                return new Uri("http://purl.org/ub/compass#BaliseTypeIsFixed");
            }
        }

        public static Uri BaliseDuplicateNextClassUri
        {
            get 
            {
                return new Uri("http://purl.org/ub/compass#DUPLICATE_OF_THE_NEXT_BALISE");
            }
        }

        public static Uri BaliseDuplicatePrevousClassUri
        {
            get
            {
                return new Uri("http://purl.org/ub/compass#DUPLICATE_OF_THE_PREVOUS_BALISE");
            }
        }
        public static Uri BaliseNoDuplicateClassUri
        {
            get
            {
                return new Uri("http://purl.org/ub/compass#NoDuplication");
            }
        }
        public static Uri BaliseDuplicateSpareClassUri
        {
            get
            {
                return new Uri("http://purl.org/ub/compass#SpareBalise");
            }
        }

        public static Uri BaliseDuplicatePropertyUri
        {
            get
            {
                return new Uri("http://purl.org/rail/core/BaliseDuplicateStatus");
            }
        }

        public static Uri RDFSMemberPropertyUri
        {
            get
            {
                return new Uri("http://www.w3.org/2000/01/rdf-schema#member");
            }
        }

        #region BaliseGroupTypes
        public static Uri BGTypeERTMSLevelTransUri
        {
            get
            {
                return new Uri("http://purl.org/ub/compass#ERTMS_LEVEL_TRANSITION");
            }
        }

        public static Uri BGTypeInFillUri
        {
            get
            {
                return new Uri("http://purl.org/ub/compass#IN_FILL");
            }
        }
        public static Uri BGTypeLevelCrossingUri
        {
            get
            {
                return new Uri("http://purl.org/ub/compass#LEVEL_CROSSING");
            }
        }
        public static Uri BGTypeLTAnnouceUri
        {
            get
            {
                return new Uri("http://purl.org/ub/compass#LT_ANNOUNCEMENT");
            }
        }
        public static Uri BGTypeMainUri
        {
            get
            {
                return new Uri("http://purl.org/ub/compass#MAIN");
            }
        }
        public static Uri BGTypeOdometrynUri
        {
            get
            {
                return new Uri("http://purl.org/ub/compass#ODOMETRY");
            }
        }
        public static Uri BGTypeRBC_BoundaryUri
        {
            get
            {
                return new Uri("http://purl.org/ub/compass#RBC_BOUNDARY");
            }
        }
        public static Uri BGTypeSignalUri
        {
            get
            {
                return new Uri("http://purl.org/ub/compass#SIGNAL");
            }
        }
        #endregion

        public static Uri BGTypePropertyUri
        {
            get
            {
                return new Uri("http://purl.org/ub/compass#BGType");
            }
        }

        public static string RadioBlockControlCentreTypeStr
        {
            get
            {
                return "http://purl.org/ub/compass#RadioBlockControlCentre";
            }
        }

        public static Uri RadioBlockControlCentreTypeUri
        {
            get
            {
                return new Uri(RadioBlockControlCentreTypeStr);
            }
        }

        public static string UpperLocationStr
        {
            get
            {
                return "http://purl.org/ub/upper/Location";
            }
        }
        public static Uri UpperLocationUri
        {
            get
            {
                return new Uri(UpperLocationStr);
            }
        }

        public static string PosReportStr
        {
            get {
                return "http://purl.org/ub/compass#PositionReport";
            }
        }

        public static Uri PosReportUri
        {
            get
            {
                return new Uri(PosReportStr);
            }
        }

        public static string BalisePassReport
        {
            get
            {
                return "http://purl.org/ub/compass#BalisePassReport";
            }
        }

        public static Uri BalisePassReportUri
        {
            get
            {
                return new Uri(BalisePassReport);
            }
        }

        public static Uri BaliseGroupCountryCodePropertyUri
        {
            get
            {
                return new Uri("http://purl.org/ub/co/baliseGroupCountryCode");
            }
        }

        public static Uri BaliseGroupUIDPropertyUri
        {
            get
            {
                return new Uri("http://purl.org/ub/co/baliseGroupID");
            }
        }

        public static Uri WKTPointUri
        {
            get
            {
                return new Uri("http://www.opengis.net/ont/geosparql#asWKT");
            }
        }
        public static string GeometryStr
        {
            get
            {
                return "http://www.opengis.net/ont/geosparql#Geometry";
            }
        }
        public static Uri GeometryUri
        {
            get
            {
                return new Uri(GeometryStr);
            }
        }


        public static Uri LatitudeProperty
        {
            get
            {
                return new Uri("http://www.w3.org/2003/01/geo/wgs84_pos#lat");
            }
        }

        public static Uri LongditudeProperty
        {
            get
            {
                return new Uri("http://www.w3.org/2003/01/geo/wgs84_pos#long");
            }
        }

        public static Uri TSAPPropertyURI
        {
            get
            {
                return new Uri("http://purl.org/ub/compass#TSAP");
            }
        }

        public static Uri NIDEnginePropertyURI
        {
            get
            {
                return new Uri("http://purl.org/ub/compass#NID_Engine");
            }
        }

        public static Uri TTrainProperty
        {
            get
            {
                return new Uri("http://purl.org/ub/compass#T_Train");
            }
        }

        public static string RailwayTrainStr
        {
            get
            {
                return "http://purl.org/rail/core/RailwayTrain";
            }
        }
        public static Uri RailwayTrain
        {
            get
            {
                return new Uri(RailwayTrainStr);
            }
        }

        public static string StirManagedTrainStr
        {
            get
            {
                return "http://purl.org/ub/compass#StirManagedTrain";
            }
        }

        public  static Uri  DistanceFromBaliseProperty
        {
            get
            {
                return new Uri("http://purl.org/rail/core/distanceFromBalise");
            }
        }

        public static Uri StirConnectSentProperty
        {
            get
            {
                return new Uri("http://purl.org/ub/compass#stirConnectSent");
            }
        }

        public static Uri ActiveLocoType
        {
            get
            {
                return new Uri("http://purl.org/ub/compass#ActiveLocomotive");
            }
        }

        public static Uri PassiveLocoType
        {
            get
            {
                return new Uri("http://purl.org/ub/compass#PassiveLocomotive");
            }
        }
    }
}
