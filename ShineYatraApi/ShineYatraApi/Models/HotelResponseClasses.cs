namespace ShineYatraApi.Models
{
    #region namespace

    using System.Collections.Generic;
    using System.Xml.Serialization;

    #endregion namespace

    /***Hotel description****/

    [XmlRoot(ElementName = "contactinfo")]
    public class Contactinfo
    {
        [XmlElement(ElementName = "address")]
        public string Address { get; set; }

        [XmlElement(ElementName = "pincode")]
        public string Pincode { get; set; }

        [XmlElement(ElementName = "citywiselocation")]
        public string Citywiselocation { get; set; }

        [XmlElement(ElementName = "locationinfo")]
        public string Locationinfo { get; set; }

        [XmlElement(ElementName = "phone")]
        public string Phone { get; set; }

        [XmlElement(ElementName = "fax")]
        public string Fax { get; set; }

        [XmlElement(ElementName = "email")]
        public string Email { get; set; }

        [XmlElement(ElementName = "website")]
        public string Website { get; set; }
    }

    [XmlRoot(ElementName = "bookinginfo")]
    public class Bookinginfo
    {
        [XmlElement(ElementName = "checkintime")]
        public string Checkintime { get; set; }

        [XmlElement(ElementName = "checkouttime")]
        public string Checkouttime { get; set; }
    }

    [XmlRoot(ElementName = "services")]
    public class Services
    {
        [XmlElement(ElementName = "creditcards")]
        public string Creditcards { get; set; }

        [XmlElement(ElementName = "hotelservices")]
        public string Hotelservices { get; set; }

        [XmlElement(ElementName = "roomservices")]
        public string Roomservices { get; set; }
    }

    [XmlRoot(ElementName = "image")]
    public class Image
    {
        [XmlElement(ElementName = "imagedesc")]
        public string Imagedesc { get; set; }

        [XmlElement(ElementName = "imagepath")]
        public string Imagepath { get; set; }
    }

    [XmlRoot(ElementName = "images")]
    public class Images
    {
        [XmlElement(ElementName = "image")]
        public List<Image> Image { get; set; }
    }

    [XmlRoot(ElementName = "hoteldetail")]
    public class Hoteldetail
    {
        [XmlElement(ElementName = "hotelname")]
        public string Hotelname { get; set; }

        [XmlElement(ElementName = "hoteldesc")]
        public string Hoteldesc { get; set; }

        [XmlElement(ElementName = "hotelchain")]
        public string Hotelchain { get; set; }

        [XmlElement(ElementName = "starrating")]
        public string Starrating { get; set; }

        [XmlElement(ElementName = "city")]
        public string City { get; set; }

        [XmlElement(ElementName = "country")]
        public string Country { get; set; }

        [XmlElement(ElementName = "noofrooms")]
        public string Noofrooms { get; set; }

        [XmlElement(ElementName = "contactinfo")]
        public Contactinfo Contactinfo { get; set; }

        [XmlElement(ElementName = "bookinginfo")]
        public Bookinginfo Bookinginfo { get; set; }

        [XmlElement(ElementName = "services")]
        public Services Services { get; set; }

        [XmlElement(ElementName = "facilities")]
        public string Facilities { get; set; }

        [XmlElement(ElementName = "images")]
        public Images Images { get; set; }
    }

    [XmlRoot(ElementName = "hotel")]
    public class Hotel
    {
        [XmlElement(ElementName = "hoteldetail")]
        public Hoteldetail Hoteldetail { get; set; }
    }

    [XmlRoot(ElementName = "searchresult")]
    public class Searchresult
    {
        [XmlElement(ElementName = "hotel")]
        public Hotel Hotel { get; set; }
    }

    [XmlRoot(ElementName = "arzHotelDescResp")]
    public class ArzHotelDescResp
    {
        [XmlElement(ElementName = "searchresult")]
        public Searchresult Searchresult { get; set; }
    }

    /**get hotel policy**/
    [XmlRoot(ElementName = "clientInfo")]
    public class ClientInfo
    {
        [XmlElement(ElementName = "partnerID")]
        public string PartnerID { get; set; }
    }

    [XmlRoot(ElementName = "hotelinfo")]
    public class Hotelinfo
    {
        [XmlElement(ElementName = "hotelid")]
        public string Hotelid { get; set; }

        [XmlElement(ElementName = "webService")]
        public string WebService { get; set; }

        [XmlElement(ElementName = "ratePlanType")]
        public string RatePlanType { get; set; }

        [XmlElement(ElementName = "roomTypeCode")]
        public string RoomTypeCode { get; set; }
    }

    [XmlRoot(ElementName = "cancellationPolicy")]
    public class CancellationPolicy
    {
        [XmlElement(ElementName = "policy")]
        public List<string> Policy { get; set; }
    }

    [XmlRoot(ElementName = "hotelPolicyRules")]
    public class HotelPolicyRules
    {
        [XmlElement(ElementName = "policy")]
        public List<string> Policy { get; set; }
    }

    [XmlRoot(ElementName = "searchresult")]
    public class SearchResult
    {
        [XmlElement(ElementName = "cancellationPolicy")]
        public CancellationPolicy CancellationPolicy { get; set; }

        [XmlElement(ElementName = "hotelPolicyRules")]
        public HotelPolicyRules HotelPolicyRules { get; set; }
    }

    [XmlRoot(ElementName = "arzHotelPolicyResp")]
    public class ArzHotelPolicyResp
    {
        [XmlElement(ElementName = "searchresult")]
        public SearchResult Searchresult { get; set; }
    }

    /*****Booking confirmation*****/

    [XmlRoot(ElementName = "address")]
    public class Address
    {
        [XmlElement(ElementName = "addressLine")]
        public List<string> AddressLine { get; set; }
    }

    [XmlRoot(ElementName = "contactNumber")]
    public class ContactNumber
    {
        [XmlElement(ElementName = "phoneNumber")]
        public string PhoneNumber { get; set; }
    }

    [XmlRoot(ElementName = "tPA__Extensions")]
    public class TPA__Extensions
    {
        [XmlElement(ElementName = "faxNumber")]
        public string FaxNumber { get; set; }
    }

    [XmlRoot(ElementName = "contactNumbers")]
    public class ContactNumbers
    {
        [XmlElement(ElementName = "contactNumber")]
        public ContactNumber ContactNumber { get; set; }

        [XmlElement(ElementName = "tPA__Extensions")]
        public TPA__Extensions TPA__Extensions { get; set; }
    }


    [XmlRoot(ElementName = "bookingresponse")]
    public class Bookingresponse
    {
        [XmlElement(ElementName = "wsKey")]
        public string WsKey { get; set; }

        [XmlElement(ElementName = "extGuestTotal")]
        public string ExtGuestTotal { get; set; }

        [XmlElement(ElementName = "roomTotal")]
        public string RoomTotal { get; set; }

        [XmlElement(ElementName = "servicetaxTotal")]
        public string ServicetaxTotal { get; set; }

        [XmlElement(ElementName = "discount")]
        public string Discount { get; set; }

        [XmlElement(ElementName = "bookingstatus")]
        public string Bookingstatus { get; set; }

        [XmlElement(ElementName = "bookingremarks")]
        public string Bookingremarks { get; set; }

        [XmlElement(ElementName = "bookingref")]
        public string Bookingref { get; set; }

        [XmlElement(ElementName = "bookingTrn")]
        public string BookingTrn { get; set; }
    }

    [XmlRoot(ElementName = "arzHotelBookingResp")]
    public class ArzHotelBookingResp
    {
        [XmlElement(ElementName = "bookingresponse")]
        public Bookingresponse Bookingresponse { get; set; }
    }

    /***Cancellation Response***/

    [XmlRoot(ElementName = "cancellationinfo")]
    public class Cancellationinfo
    {
        [XmlElement(ElementName = "currency")]
        public string Currency { get; set; }

        [XmlElement(ElementName = "refundTotalAmount")]
        public string RefundTotalAmount { get; set; }

        [XmlElement(ElementName = "cancellationAmount")]
        public string CancellationAmount { get; set; }

        [XmlElement(ElementName = "success")]
        public string Success { get; set; }

        [XmlElement(ElementName = "error")]
        public string Error { get; set; }
    }

    [XmlRoot(ElementName = "arzHotelCancellationRes")]
    public class ArzHotelCancellationRes
    {
        [XmlElement(ElementName = "cancellationinfo")]
        public Cancellationinfo Cancellationinfo { get; set; }
    }

    /***Hotel Booking Response***/

    [XmlRoot(ElementName = "arzHotelProvResp")]
    public class ArzHotelProvResp
    {
        [XmlElement(ElementName = "allocresult")]
        public AllocResult AllocResult { get; set; }
    }
    [XmlRoot(ElementName = "allocresult")]
    public class AllocResult
    {
        [XmlElement(ElementName = "allocavail")]
        public string AllocAvail { get; set; }

        [XmlElement(ElementName = "allocid")]
        public string AllocId { get; set; }

        [XmlElement(ElementName = "error")]
        public string Error { get; set; }
    }
}