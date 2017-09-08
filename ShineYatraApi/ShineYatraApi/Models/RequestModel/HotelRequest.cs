namespace ShineYatraApi.Models.RequestModel
{
    #region namespace

    using System.Xml.Serialization;

    #endregion namespace

    [XmlRoot(ElementName = "child")]
    public class Child
    {
        [XmlElement(ElementName = "age")]
        public string Age { get; set; }
    }

    [XmlRoot(ElementName = "guestDetails")]
    public class GuestDetails
    {
        [XmlElement(ElementName = "adults")]
        public string Adults { get; set; }

        [XmlElement(ElementName = "child")]
        public Child Child { get; set; }
    }

    [XmlRoot(ElementName = "HotelRequest")]
    public class HotelRequest
    {
        [XmlElement(ElementName = "guestDetails")]
        public GuestDetails GuestDetails { get; set; }

        [XmlElement(ElementName = "start")]
        public string Start { get; set; }

        [XmlElement(ElementName = "end")]
        public string End { get; set; }

        [XmlElement(ElementName = "hotelCityName")]
        public string HotelCityName { get; set; }

        [XmlElement(ElementName = "hotelName")]
        public string HotelName { get; set; }

        [XmlElement(ElementName = "area")]
        public string Area { get; set; }

        [XmlElement(ElementName = "attraction")]
        public string Attraction { get; set; }

        [XmlElement(ElementName = "rating")]
        public string Rating { get; set; }

        [XmlElement(ElementName = "hotelPackage")]
        public string HotelPackage { get; set; }

        [XmlElement(ElementName = "sortingPreference")]
        public string SortingPreference { get; set; }

        [XmlElement(ElementName = "hotelid")]
        public string hotelid { get; set; }

        [XmlElement(ElementName = "webService")]
        public string webService { get; set; }
    }

    public class HotelDescriptionRequest
    {
        [XmlElement(ElementName = "hotelid")]
        public string hotelid { get; set; }

        [XmlElement(ElementName = "webService")]
        public string webService { get; set; }

        [XmlElement(ElementName = "city")]
        public string city { get; set; }

        [XmlElement(ElementName = "roomTypeCode")]
        public string RoomTypeCode { get; set; }

        [XmlElement(ElementName = "checkInDate")]
        public string CheckInDate { get; set; }

        [XmlElement(ElementName = "checkOutDate")]
        public string CheckOutDate { get; set; }

        [XmlElement(ElementName = "ratePlanType")]
        public string RatePlanType { get; set; }

        [XmlElement(ElementName = "fromallocation")]
        public string Fromallocation { get; set; }

        [XmlElement(ElementName = "allocid")]
        public string Allocid { get; set; }

        [XmlElement(ElementName = "roombasis")]
        public string Roombasis { get; set; }

        [XmlElement(ElementName = "roomtype")]
        public string Roomtype { get; set; }

        [XmlElement(ElementName = "wsKey")]
        public string WsKey { get; set; }

        [XmlElement(ElementName = "lastName")]
        public string LastName { get; set; }

        [XmlElement(ElementName = "bookingref")]
        public string Bookingref { get; set; }

        [XmlElement(ElementName = "email")]
        public string Email { get; set; }
    }
}