using Newtonsoft.Json;
using System;
using System.Collections.Generic;
namespace Telop
{
    public class JMAxml_EqVol_Main
    {
        public class Author
        {
            public string Name { get; set; }
        }
        public class Content
        {
            [JsonProperty("@type")]
            public string Type { get; set; }
            [JsonProperty("#text")]
            public string Text { get; set; }
        }
        public class Entry
        {
            public string Title { get; set; }
            public string Id { get; set; }
            public DateTime Updated { get; set; }
            public Author Author { get; set; }
            public Link Link { get; set; }
            public Content Content { get; set; }
        }
        public class Feed
        {
            [JsonProperty("@xmlns")]
            public string Xmlns { get; set; }
            [JsonProperty("@lang")]
            public string Lang { get; set; }
            public string Title { get; set; }
            public string Subtitle { get; set; }
            public DateTime Updated { get; set; }
            public string Id { get; set; }
            public List<Link> Link { get; set; }
            public Rights Rights { get; set; }
            public List<Entry> Entry { get; set; }
        }
        public class Link
        {
            [JsonProperty("@rel")]
            public string Rel { get; set; }
            [JsonProperty("@href")]
            public string Href { get; set; }
            [JsonProperty("@type")]
            public string Type { get; set; }
        }
        public class Rights
        {
            [JsonProperty("@type")]
            public string Type { get; set; }
            [JsonProperty("#cdata-section")]
            public string CdataSection { get; set; }
        }
        public class JMAXML
        {
            [JsonProperty("?xml")]
            public Xml Xml { get; set; }
            public Feed Feed { get; set; }
        }
        public class Xml
        {
            [JsonProperty("@version")]
            public string Version { get; set; }
            [JsonProperty("@encoding")]
            public string Encoding { get; set; }
        }
    }
    public class JMAxml_EqVol_Detail_Observation
    {
        public class Area
        {
            public string Name { get; set; }
            public string Code { get; set; }
            public Coordinate Coordinate { get; set; }
            public string CraterName { get; set; }
            public CraterCoordinate CraterCoordinate { get; set; }
        }
        public class Areas
        {
            [JsonProperty("@codeType")]
            public string CodeType { get; set; }
            public Area Area { get; set; }
        }
        public class Body
        {
            [JsonProperty("@xmlns")]
            public string Xmlns { get; set; }
            [JsonProperty("@xmlns:jmx_eb")]
            public string XmlnsJmxEb { get; set; }
            public VolcanoInfo VolcanoInfo { get; set; }
            public VolcanoObservation VolcanoObservation { get; set; }
        }
        public class ColorPlume
        {
            [JsonProperty("jmx_eb:PlumeHeightAboveCrater")]
            public JmxEbPlumeHeightAboveCrater JmxEbPlumeHeightAboveCrater { get; set; }
            [JsonProperty("jmx_eb:PlumeHeightAboveSeaLevel")]
            public JmxEbPlumeHeightAboveSeaLevel JmxEbPlumeHeightAboveSeaLevel { get; set; }
            [JsonProperty("jmx_eb:PlumeDirection")]
            public JmxEbPlumeDirection JmxEbPlumeDirection { get; set; }
        }
        public class Coordinate
        {
            [JsonProperty("@description")]
            public string Description { get; set; }
            [JsonProperty("#text")]
            public string Text { get; set; }
        }
        public class CraterCoordinate
        {
            [JsonProperty("@description")]
            public string Description { get; set; }

            [JsonProperty("#text")]
            public string Text { get; set; }
        }
        public class EventDateTime
        {
            [JsonProperty("@significant")]
            public string Significant { get; set; }
            [JsonProperty("#text")]
            public DateTime Text { get; set; }
        }
        public class EventDateTimeUTC
        {
            [JsonProperty("@significant")]
            public string Significant { get; set; }
            [JsonProperty("#text")]
            public DateTime Text { get; set; }
        }
        public class EventTime
        {
            public EventDateTime EventDateTime { get; set; }
            public EventDateTimeUTC EventDateTimeUTC { get; set; }
        }
        public class Head
        {
            [JsonProperty("@xmlns")]
            public string Xmlns { get; set; }
            public string Title { get; set; }
            public DateTime ReportDateTime { get; set; }
            public DateTime TargetDateTime { get; set; }
            public string EventID { get; set; }
            public string InfoType { get; set; }
            public string Serial { get; set; }
            public string InfoKind { get; set; }
            public string InfoKindVersion { get; set; }
            public Headline Headline { get; set; }
        }
        public class Headline
        {
            public string Text { get; set; }
            public Information Information { get; set; }
        }
        public class Information
        {
            [JsonProperty("@type")]
            public string Type { get; set; }
            public Item Item { get; set; }
        }
        public class Item
        {
            public Kind Kind { get; set; }
            public Areas Areas { get; set; }
            public EventTime EventTime { get; set; }
        }
        public class JmxControl
        {
            [JsonProperty("jmx:Title")]
            public string JmxTitle { get; set; }
            [JsonProperty("jmx:DateTime")]
            public DateTime JmxDateTime { get; set; }
            [JsonProperty("jmx:Status")]
            public string JmxStatus { get; set; }
            [JsonProperty("jmx:EditorialOffice")]
            public string JmxEditorialOffice { get; set; }
            [JsonProperty("jmx:PublishingOffice")]
            public string JmxPublishingOffice { get; set; }
        }
        public class JmxEbPlumeDirection
        {
            [JsonProperty("@type")]
            public string Type { get; set; }
            [JsonProperty("@unit")]
            public string Unit { get; set; }
            [JsonProperty("@description")]
            public string Description { get; set; }
            [JsonProperty("#text")]
            public string Text { get; set; }
        }
        public class JmxEbPlumeHeightAboveCrater
        {
            [JsonProperty("@type")]
            public string Type { get; set; }
            [JsonProperty("@unit")]
            public string Unit { get; set; }
            [JsonProperty("@description")]
            public string Description { get; set; }
            [JsonProperty("#text")]
            public string Text { get; set; }
        }
        public class JmxEbPlumeHeightAboveSeaLevel
        {
            [JsonProperty("@type")]
            public string Type { get; set; }
            [JsonProperty("@unit")]
            public string Unit { get; set; }
            [JsonProperty("@description")]
            public string Description { get; set; }
            [JsonProperty("#text")]
            public string Text { get; set; }
        }
        public class JmxReport
        {
            [JsonProperty("@xmlns")]
            public string Xmlns { get; set; }
            [JsonProperty("@xmlns:jmx")]
            public string XmlnsJmx { get; set; }
            [JsonProperty("jmx:Control")]
            public JmxControl JmxControl { get; set; }
            public Head Head { get; set; }
            public Body Body { get; set; }
        }
        public class Kind
        {
            public string Name { get; set; }
            public string Code { get; set; }
        }
        public class JMAXML
        {
            [JsonProperty("?xml")]
            public Xml Xml { get; set; }
            [JsonProperty("jmx:Report")]
            public JmxReport JmxReport { get; set; }
        }
        public class VolcanoInfo
        {
            [JsonProperty("@type")]
            public string Type { get; set; }
            public Item Item { get; set; }
        }
        public class VolcanoObservation
        {
            public ColorPlume ColorPlume { get; set; }
            public string OtherObservation { get; set; }
        }
        public class Xml
        {
            [JsonProperty("@version")]
            public string Version { get; set; }
            [JsonProperty("@encoding")]
            public string Encoding { get; set; }
        }
    }
    public class JMAxml_EqVol_Detail_EruptionBulletin
    {
        public class Area
        {
            public string Name { get; set; }
            public string Code { get; set; }
        }
        public class Areas
        {
            [JsonProperty("@codeType")]
            public string CodeType { get; set; }
            public List<Area> Area { get; set; }
        }

        public class Body
        {
            [JsonProperty("@xmlns")]
            public string Xmlns { get; set; }
            [JsonProperty("@xmlns:jmx_eb")]
            public string XmlnsJmxEb { get; set; }
            public List<VolcanoInfo> VolcanoInfo { get; set; }
            public VolcanoInfoContent VolcanoInfoContent { get; set; }
        }
        public class EventDateTime
        {
            [JsonProperty("@significant")]
            public string Significant { get; set; }
            [JsonProperty("@dubious")]
            public string Dubious { get; set; }
            [JsonProperty("#text")]
            public DateTime Text { get; set; }
        }
        public class EventDateTimeUTC
        {
            [JsonProperty("@significant")]
            public string Significant { get; set; }
            [JsonProperty("@dubious")]
            public string Dubious { get; set; }
            [JsonProperty("#text")]
            public DateTime Text { get; set; }
        }
        public class EventTime
        {
            public EventDateTime EventDateTime { get; set; }
            public EventDateTimeUTC EventDateTimeUTC { get; set; }
        }
        public class Head
        {
            [JsonProperty("@xmlns")]
            public string Xmlns { get; set; }
            public string Title { get; set; }
            public DateTime ReportDateTime { get; set; }
            public DateTime TargetDateTime { get; set; }
            public string TargetDTDubious { get; set; }
            public string EventID { get; set; }
            public string InfoType { get; set; }
            public object Serial { get; set; }
            public string InfoKind { get; set; }
            public string InfoKindVersion { get; set; }
            public Headline Headline { get; set; }
        }
        public class Headline
        {
            public string Text { get; set; }
            public Information Information { get; set; }
        }
        public class Information
        {
            [JsonProperty("@type")]
            public string Type { get; set; }
            public Item Item { get; set; }
        }
        public class Item
        {
            public Kind Kind { get; set; }
            public Areas Areas { get; set; }
            public EventTime EventTime { get; set; }
        }
        public class JmxControl
        {
            [JsonProperty("jmx:Title")]
            public string JmxTitle { get; set; }
            [JsonProperty("jmx:DateTime")]
            public DateTime JmxDateTime { get; set; }
            [JsonProperty("jmx:Status")]
            public string JmxStatus { get; set; }
            [JsonProperty("jmx:EditorialOffice")]
            public string JmxEditorialOffice { get; set; }
            [JsonProperty("jmx:PublishingOffice")]
            public string JmxPublishingOffice { get; set; }
        }
        public class JmxReport
        {
            [JsonProperty("@xmlns")]
            public string Xmlns { get; set; }
            [JsonProperty("@xmlns:jmx")]
            public string XmlnsJmx { get; set; }
            [JsonProperty("jmx:Control")]
            public JmxControl JmxControl { get; set; }
            public Head Head { get; set; }
            public Body Body { get; set; }
        }
        public class Kind
        {
            public string Name { get; set; }
            public string Code { get; set; }
        }
        public class JMAXML
        {
            [JsonProperty("?xml")]
            public Xml Xml { get; set; }
            [JsonProperty("jmx:Report")]
            public JmxReport JmxReport { get; set; }
        }
        public class VolcanoInfo
        {
            [JsonProperty("@type")]
            public string Type { get; set; }
            public Item Item { get; set; }
        }
        public class VolcanoInfoContent
        {
            public string VolcanoHeadline { get; set; }
            public string VolcanoActivity { get; set; }
        }
        public class Xml
        {
            [JsonProperty("@version")]
            public string Version { get; set; }
            [JsonProperty("@encoding")]
            public string Encoding { get; set; }
        }
    }
    public class JMAxml_EqVol_Detail_Commentary
    {
        public class Area
        {
            public string Name { get; set; }
            public string Code { get; set; }
            public Coordinate Coordinate { get; set; }
        }

        public class Areas
        {
            [JsonProperty("@codeType")]
            public string CodeType { get; set; }
            public Area Area { get; set; }
        }

        public class Body
        {
            [JsonProperty("@xmlns")]
            public string Xmlns { get; set; }

            [JsonProperty("@xmlns:jmx_eb")]
            public string XmlnsJmxEb { get; set; }
            public VolcanoInfo VolcanoInfo { get; set; }
            public VolcanoInfoContent VolcanoInfoContent { get; set; }
        }

        public class Coordinate
        {
            [JsonProperty("@description")]
            public string Description { get; set; }

            [JsonProperty("#text")]
            public string Text { get; set; }
        }

        public class Head
        {
            [JsonProperty("@xmlns")]
            public string Xmlns { get; set; }
            public string Title { get; set; }
            public DateTime ReportDateTime { get; set; }
            public DateTime TargetDateTime { get; set; }
            public string EventID { get; set; }
            public string InfoType { get; set; }
            public string Serial { get; set; }
            public string InfoKind { get; set; }
            public string InfoKindVersion { get; set; }
            public Headline Headline { get; set; }
        }

        public class Headline
        {
            public string Text { get; set; }
            public Information Information { get; set; }
        }

        public class Information
        {
            [JsonProperty("@type")]
            public string Type { get; set; }
            public Item Item { get; set; }
        }

        public class Item
        {
            public Kind Kind { get; set; }
            public LastKind LastKind { get; set; }
            public Areas Areas { get; set; }
        }

        public class JmxControl
        {
            [JsonProperty("jmx:Title")]
            public string JmxTitle { get; set; }

            [JsonProperty("jmx:DateTime")]
            public DateTime JmxDateTime { get; set; }

            [JsonProperty("jmx:Status")]
            public string JmxStatus { get; set; }

            [JsonProperty("jmx:EditorialOffice")]
            public string JmxEditorialOffice { get; set; }

            [JsonProperty("jmx:PublishingOffice")]
            public string JmxPublishingOffice { get; set; }
        }

        public class JmxReport
        {
            [JsonProperty("@xmlns")]
            public string Xmlns { get; set; }

            [JsonProperty("@xmlns:jmx")]
            public string XmlnsJmx { get; set; }

            [JsonProperty("jmx:Control")]
            public JmxControl JmxControl { get; set; }
            public Head Head { get; set; }
            public Body Body { get; set; }
        }

        public class Kind
        {
            public string Name { get; set; }
            public string Code { get; set; }
            public string Condition { get; set; }
        }

        public class LastKind
        {
            public string Name { get; set; }
            public string Code { get; set; }
            public object Condition { get; set; }
        }

        public class JMAXML
        {
            [JsonProperty("?xml")]
            public Xml Xml { get; set; }

            [JsonProperty("jmx:Report")]
            public JmxReport JmxReport { get; set; }
        }

        public class VolcanoInfo
        {
            [JsonProperty("@type")]
            public string Type { get; set; }
            public Item Item { get; set; }
        }

        public class VolcanoInfoContent
        {
            public string VolcanoHeadline { get; set; }
            public string VolcanoActivity { get; set; }
            public string VolcanoPrevention { get; set; }
            public string NextAdvisory { get; set; }
        }

        public class Xml
        {
            [JsonProperty("@version")]
            public string Version { get; set; }

            [JsonProperty("@encoding")]
            public string Encoding { get; set; }
        }
    }
    public class JMAxml_Extra_Main
    {
        public class Author
        {
            public string Name { get; set; }
        }

        public class Content
        {
            [JsonProperty("@type")]
            public string Type { get; set; }

            [JsonProperty("#text")]
            public string Text { get; set; }
        }

        public class Entry
        {
            public string Title { get; set; }
            public string Id { get; set; }
            public DateTime Updated { get; set; }
            public Author Author { get; set; }
            public Link Link { get; set; }
            public Content Content { get; set; }
        }

        public class Feed
        {
            [JsonProperty("@xmlns")]
            public string Xmlns { get; set; }

            [JsonProperty("@lang")]
            public string Lang { get; set; }
            public string Title { get; set; }
            public string Subtitle { get; set; }
            public DateTime Updated { get; set; }
            public string Id { get; set; }
            public List<Link> Link { get; set; }
            public Rights Rights { get; set; }
            public List<Entry> Entry { get; set; }
        }

        public class Link
        {
            [JsonProperty("@rel")]
            public string Rel { get; set; }

            [JsonProperty("@href")]
            public string Href { get; set; }

            [JsonProperty("@type")]
            public string Type { get; set; }
        }

        public class Rights
        {
            [JsonProperty("@type")]
            public string Type { get; set; }

            [JsonProperty("#cdata-section")]
            public string CdataSection { get; set; }
        }

        public class JMAXML
        {
            [JsonProperty("?xml")]
            public Xml Xml { get; set; }
            public Feed Feed { get; set; }
        }

        public class Xml
        {
            [JsonProperty("@version")]
            public string Version { get; set; }

            [JsonProperty("@encoding")]
            public string Encoding { get; set; }
        }
    }
    public class JMAxml_Extra_River
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class AdditionalInfo
        {
            public FloodForecastAddition FloodForecastAddition { get; set; }
        }

        public class Area
        {
            public string Name { get; set; }
            public string Code { get; set; }
            public string Prefecture { get; set; }
            public string PrefectureCode { get; set; }
            public string City { get; set; }
            public string CityCode { get; set; }
            public string SubCityList { get; set; }

            [JsonProperty("@codeType")]
            public string CodeType { get; set; }
            public string Location { get; set; }
        }

        public class Areas
        {
            [JsonProperty("@codeType")]
            public string CodeType { get; set; }
            public Area Area { get; set; }
        }

        public class Body
        {
            [JsonProperty("@xmlns")]
            public string Xmlns { get; set; }

            [JsonProperty("@xmlns:jmx_eb")]
            public string XmlnsJmxEb { get; set; }
            public Warning Warning { get; set; }
            public List<MeteorologicalInfos> MeteorologicalInfos { get; set; }
            public OfficeInfo OfficeInfo { get; set; }
            public AdditionalInfo AdditionalInfo { get; set; }
        }

        public class Code
        {
            [JsonProperty("@type")]
            public string Type { get; set; }

            [JsonProperty("#text")]
            public string Text { get; set; }
        }

        public class Criteria
        {
            [JsonProperty("jmx_eb:WaterLevel")]
            public List<JmxEbWaterLevel> JmxEbWaterLevel { get; set; }
        }

        public class FloodForecastAddition
        {
            public List<HydrometricStationPart> HydrometricStationPart { get; set; }
        }

        public class Head
        {
            [JsonProperty("@xmlns")]
            public string Xmlns { get; set; }
            public string Title { get; set; }
            public DateTime ReportDateTime { get; set; }
            public DateTime TargetDateTime { get; set; }
            public string EventID { get; set; }
            public string InfoType { get; set; }
            public string Serial { get; set; }
            public string InfoKind { get; set; }
            public string InfoKindVersion { get; set; }
            public Headline Headline { get; set; }
        }

        public class Headline
        {
            public string Text { get; set; }
            public List<Information> Information { get; set; }
        }

        public class HydrometricStationPart
        {
            public Area Area { get; set; }
            public string ChargeSection { get; set; }
            public Criteria Criteria { get; set; }
        }

        public class Information
        {
            [JsonProperty("@type")]
            public string Type { get; set; }
            public Item Item { get; set; }
        }

        public class Item
        {
            public Kind Kind { get; set; }
            public Areas Areas { get; set; }
            public Stations Stations { get; set; }
        }

        public class JmxControl
        {
            [JsonProperty("jmx:Title")]
            public string JmxTitle { get; set; }

            [JsonProperty("jmx:DateTime")]
            public DateTime JmxDateTime { get; set; }

            [JsonProperty("jmx:Status")]
            public string JmxStatus { get; set; }

            [JsonProperty("jmx:EditorialOffice")]
            public string JmxEditorialOffice { get; set; }

            [JsonProperty("jmx:PublishingOffice")]
            public string JmxPublishingOffice { get; set; }
        }

        public class JmxEbWaterLevel
        {
            [JsonProperty("@type")]
            public string Type { get; set; }

            [JsonProperty("@unit")]
            public string Unit { get; set; }

            [JsonProperty("@condition")]
            public string Condition { get; set; }

            [JsonProperty("#text")]
            public string Text { get; set; }
        }

        public class JmxReport
        {
            [JsonProperty("@xmlns")]
            public string Xmlns { get; set; }

            [JsonProperty("@xmlns:jmx")]
            public string XmlnsJmx { get; set; }

            [JsonProperty("jmx:Control")]
            public JmxControl JmxControl { get; set; }
            public Head Head { get; set; }
            public Body Body { get; set; }
        }

        public class Kind
        {
            public string Name { get; set; }
            public string Code { get; set; }
            public string Condition { get; set; }
            public Property Property { get; set; }
            public string Status { get; set; }
        }

        public class MeteorologicalInfos
        {
            [JsonProperty("@type")]
            public string Type { get; set; }
            public MeteorologicalInfo MeteorologicalInfo { get; set; }
            public TimeSeriesInfo TimeSeriesInfo { get; set; }
        }

        public class MeteorologicalInfo
        {
            public DateTime DateTime { get; set; }
            public Item Item { get; set; }
        }

        public class Office
        {
            [JsonProperty("@type")]
            public string Type { get; set; }
            public string Name { get; set; }
            public string Code { get; set; }
            public string ContactInfo { get; set; }
            public string URI { get; set; }
        }

        public class OfficeInfo
        {
            public List<Office> Office { get; set; }
        }

        public class Property
        {
            public string Type { get; set; }
            public string Text { get; set; }
        }

        public class JMAXML
        {
            [JsonProperty("?xml")]
            public Xml Xml { get; set; }

            [JsonProperty("jmx:Report")]
            public JmxReport JmxReport { get; set; }
        }

        public class Station
        {
            public string Name { get; set; }
            public Code Code { get; set; }
            public string Location { get; set; }
        }

        public class Stations
        {
            public List<Station> Station { get; set; }
        }

        public class TimeDefine
        {
            [JsonProperty("@timeId")]
            public string TimeId { get; set; }
            public DateTime DateTime { get; set; }
            public string Duration { get; set; }
            public string Name { get; set; }
        }

        public class TimeDefines
        {
            public List<TimeDefine> TimeDefine { get; set; }
        }

        public class TimeSeriesInfo
        {
            public TimeDefines TimeDefines { get; set; }
            public object Item { get; set; }
        }

        public class Warning
        {
            [JsonProperty("@type")]
            public string Type { get; set; }
            public List<Item> Item { get; set; }
        }

        public class Xml
        {
            [JsonProperty("@version")]
            public string Version { get; set; }

            [JsonProperty("@encoding")]
            public string Encoding { get; set; }
        }


    }
    public class JMAxml_Extra_Landslide1
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Area
        {
            public string Name { get; set; }
            public string Code { get; set; }
        }

        public class Areas
        {
            [JsonProperty("@codeType")]
            public string CodeType { get; set; }
            public Area Area { get; set; }
        }

        public class Body
        {
            [JsonProperty("@xmlns")]
            public string Xmlns { get; set; }
            public TargetArea TargetArea { get; set; }
            public Warning Warning { get; set; }
            public OfficeInfo OfficeInfo { get; set; }
        }

        public class Head
        {
            [JsonProperty("@xmlns")]
            public string Xmlns { get; set; }
            public string Title { get; set; }
            public DateTime ReportDateTime { get; set; }
            public DateTime TargetDateTime { get; set; }
            public string EventID { get; set; }
            public string InfoType { get; set; }
            public string Serial { get; set; }
            public string InfoKind { get; set; }
            public string InfoKindVersion { get; set; }
            public Headline Headline { get; set; }
        }

        public class Headline
        {
            public string Text { get; set; }
            public Information Information { get; set; }
        }

        public class Information
        {
            [JsonProperty("@type")]
            public string Type { get; set; }
            public Item Item { get; set; }
        }

        public class Item
        {
            public Kind Kind { get; set; }
            public Areas Areas { get; set; }
            public Area Area { get; set; }
        }

        public class JmxControl
        {
            [JsonProperty("jmx:Title")]
            public string JmxTitle { get; set; }

            [JsonProperty("jmx:DateTime")]
            public DateTime JmxDateTime { get; set; }

            [JsonProperty("jmx:Status")]
            public string JmxStatus { get; set; }

            [JsonProperty("jmx:EditorialOffice")]
            public string JmxEditorialOffice { get; set; }

            [JsonProperty("jmx:PublishingOffice")]
            public string JmxPublishingOffice { get; set; }
        }

        public class JmxReport
        {
            [JsonProperty("@xmlns")]
            public string Xmlns { get; set; }

            [JsonProperty("@xmlns:jmx")]
            public string XmlnsJmx { get; set; }

            [JsonProperty("jmx:Control")]
            public JmxControl JmxControl { get; set; }
            public Head Head { get; set; }
            public Body Body { get; set; }
        }

        public class Kind
        {
            public string Name { get; set; }
            public string Code { get; set; }
            public string Condition { get; set; }
            public string Status { get; set; }
        }

        public class Office
        {
            [JsonProperty("@type")]
            public string Type { get; set; }
            public string Name { get; set; }
            public string ContactInfo { get; set; }
        }

        public class OfficeInfo
        {
            public List<Office> Office { get; set; }
        }

        public class JMAXML
        {
            [JsonProperty("?xml")]
            public Xml Xml { get; set; }

            [JsonProperty("jmx:Report")]
            public JmxReport JmxReport { get; set; }
        }

        public class TargetArea
        {
            public string Name { get; set; }
            public string Code { get; set; }
        }

        public class Warning
        {
            [JsonProperty("@type")]
            public string Type { get; set; }
            public List<Item> Item { get; set; }
        }

        public class Xml
        {
            [JsonProperty("@version")]
            public string Version { get; set; }

            [JsonProperty("@encoding")]
            public string Encoding { get; set; }
        }


    }
    public class JMAxml_Extra_Landslide2
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Area
        {
            public string Name { get; set; }
            public string Code { get; set; }
        }

        public class Areas
        {
            [JsonProperty("@codeType")]
            public string CodeType { get; set; }
            public object Area { get; set; }
        }

        public class Body
        {
            [JsonProperty("@xmlns")]
            public string Xmlns { get; set; }
            public TargetArea TargetArea { get; set; }
            public Warning Warning { get; set; }
            public OfficeInfo OfficeInfo { get; set; }
        }

        public class Head
        {
            [JsonProperty("@xmlns")]
            public string Xmlns { get; set; }
            public string Title { get; set; }
            public DateTime ReportDateTime { get; set; }
            public DateTime TargetDateTime { get; set; }
            public string EventID { get; set; }
            public string InfoType { get; set; }
            public string Serial { get; set; }
            public string InfoKind { get; set; }
            public string InfoKindVersion { get; set; }
            public Headline Headline { get; set; }
        }

        public class Headline
        {
            public string Text { get; set; }
            public Information Information { get; set; }
        }

        public class Information
        {
            [JsonProperty("@type")]
            public string Type { get; set; }
            public List<Item> Item { get; set; }
        }

        public class Item
        {
            public Kind Kind { get; set; }
            public Areas Areas { get; set; }
            public Area Area { get; set; }
        }

        public class JmxControl
        {
            [JsonProperty("jmx:Title")]
            public string JmxTitle { get; set; }

            [JsonProperty("jmx:DateTime")]
            public DateTime JmxDateTime { get; set; }

            [JsonProperty("jmx:Status")]
            public string JmxStatus { get; set; }

            [JsonProperty("jmx:EditorialOffice")]
            public string JmxEditorialOffice { get; set; }

            [JsonProperty("jmx:PublishingOffice")]
            public string JmxPublishingOffice { get; set; }
        }

        public class JmxReport
        {
            [JsonProperty("@xmlns")]
            public string Xmlns { get; set; }

            [JsonProperty("@xmlns:jmx")]
            public string XmlnsJmx { get; set; }

            [JsonProperty("jmx:Control")]
            public JmxControl JmxControl { get; set; }
            public Head Head { get; set; }
            public Body Body { get; set; }
        }

        public class Kind
        {
            public string Name { get; set; }
            public string Code { get; set; }
            public string Condition { get; set; }
            public string Status { get; set; }
        }

        public class Office
        {
            [JsonProperty("@type")]
            public string Type { get; set; }
            public string Name { get; set; }
            public string ContactInfo { get; set; }
        }

        public class OfficeInfo
        {
            public List<Office> Office { get; set; }
        }

        public class JMAXML
        {
            [JsonProperty("?xml")]
            public Xml Xml { get; set; }

            [JsonProperty("jmx:Report")]
            public JmxReport JmxReport { get; set; }
        }

        public class TargetArea
        {
            public string Name { get; set; }
            public string Code { get; set; }
        }

        public class Warning
        {
            [JsonProperty("@type")]
            public string Type { get; set; }
            public List<Item> Item { get; set; }
        }

        public class Xml
        {
            [JsonProperty("@version")]
            public string Version { get; set; }

            [JsonProperty("@encoding")]
            public string Encoding { get; set; }
        }


    }
}
