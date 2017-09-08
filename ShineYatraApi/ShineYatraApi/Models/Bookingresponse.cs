namespace ShineYatraApi.Models.Flight
{
    #region namespace

    using System.Xml.Serialization;

    #endregion namespace

    [XmlRoot(ElementName = "Bookingresponse")]
    public class BookingResponse
    {
        [XmlElement(ElementName = "error")]
        public string Error { get; set; }

        [XmlElement(ElementName = "status")]
        public string Status { get; set; }

        [XmlElement(ElementName = "transid")]
        public string Transid { get; set; }

        [XmlElement(ElementName = "partnerRefId")]
        public string PartnerRefId { get; set; }
    }
}