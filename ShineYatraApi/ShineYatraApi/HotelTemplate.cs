namespace ShineYatraApi
{
    #region namespace

    using System.Configuration;

    #endregion namespace

    /// <summary>
    /// Hold all hotel template
    /// </summary>
    public class HotelTemplate
    {
        //public string SearchHotel = "<?xml version='1.0' encoding='UTF-8'?>" +
        //    "<soapenv:Envelope xmlns:ns0='http://search.hotel.com'" +
        //    "xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/'" +
        //    "xmlns:xsd='http://www.w3.org/2001/XMLSchema' xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'>" +
        //    "<soapenv:Header></soapenv:Header><soapenv:Body><ns0:getHotelAvailSearch xmlns:ns0='http://search.hotel.com'>" +
        //    "<in0><arzHotelAvailReq><clientInfo>" +
        //    "<partnerID>" + ConfigurationManager.AppSettings["PartnerID"] + "</partnerID>" +
        //    "<username>" + ConfigurationManager.AppSettings["Username"] + "</username>" +
        //    "<userID>" + ConfigurationManager.AppSettings["UserID"] + "</userID>" +
        //    "<password>" + ConfigurationManager.AppSettings["HotelPassword"] + "</password>" +
        //    "<userType> " + ConfigurationManager.AppSettings["HotelClientType"] + "</userType>" +
        //    "</clientInfo><requestSegment>" +
        //    "<currency>" + ConfigurationManager.AppSettings["CurrencyValue"] + "</currency><searchType>search</searchType>" +
        //    "<residentOfIndia>" + ConfigurationManager.AppSettings["ResidentOfIndia"] + "</residentOfIndia>" +
        //    "<stayDateRange><start>startDateValue</start><end>endDateValue</end></stayDateRange>" +
        //    "<roomStayCandidate>guestDetailsXmlValue</roomStayCandidate>" +
        //    "<hotelSearchCriteria><hotelCityName>hotelCityNameValue</hotelCityName>" +
        //    "<hotelName>hotelNameValue</hotelName><area>areaValue</area><attraction>attractionValue</attraction>" +
        //    "<rating>ratingValue</rating><sortingPreference>sortingPreferenceValue</sortingPreference>" +
        //    "<hotelPackage>hotelPackageValue</hotelPackage></hotelSearchCriteria></requestSegment></arzHotelAvailReq>" +
        //    "</in0><in1></in1></ns0:getHotelAvailSearch></soapenv:Body></soapenv:Envelope>";

        public string SearchHotel = "<arzHotelAvailReq><clientInfo>" +
        "<partnerID>" + ConfigurationManager.AppSettings["PartnerID"] + "</partnerID>" +
        "<username>" + ConfigurationManager.AppSettings["Username"] + "</username>" +
        "<userID>" + ConfigurationManager.AppSettings["UserID"] + "</userID>" +
        "<password>" + ConfigurationManager.AppSettings["HotelPassword"] + "</password>" +
        "<userType>" + ConfigurationManager.AppSettings["HotelClientType"] + "</userType>" +
        "</clientInfo><requestSegment>" +
        "<currency>" + ConfigurationManager.AppSettings["CurrencyValue"] + "</currency><searchType>search</searchType>" +
        "<residentOfIndia>" + ConfigurationManager.AppSettings["ResidentOfIndia"] + "</residentOfIndia>" +
        "<stayDateRange><start>startDateValue</start><end>endDateValue</end></stayDateRange>" +
        "<roomStayCandidate>guestDetailsXmlValue</roomStayCandidate>" +
        "<hotelSearchCriteria><hotelCityName>hotelCityNameValue</hotelCityName>" +
        "<hotelName>hotelNameValue</hotelName><area>areaValue</area><attraction>attractionValue</attraction>" +
        "<rating>ratingValue</rating><sortingPreference>sortingPreferenceValue</sortingPreference>" +
        "<hotelPackage>hotelPackageValue</hotelPackage></hotelSearchCriteria></requestSegment></arzHotelAvailReq>";

        public string SearchHotelDescription = "<arzHotelDescReq><clientInfo>" +
            "<partnerID>" + ConfigurationManager.AppSettings["PartnerID"] + "</partnerID>" +
            "<username>" + ConfigurationManager.AppSettings["Username"] + "</username>" +
            "<userID>" + ConfigurationManager.AppSettings["UserID"] + "</userID>" +
            "<password>" + ConfigurationManager.AppSettings["HotelPassword"] + "</password>" +
            "<userType>" + ConfigurationManager.AppSettings["HotelClientType"] + "</userType>" +
            "</clientInfo><searchquery><hotelinfo><hotelid>hotelidValue</hotelid>" +
            "<webService>webServiceValue</webService><city>cityValue</city></hotelinfo></searchquery>" +
            "</arzHotelDescReq>";

        public string HotelPolicyXml = "<arzHotelPolicyReq><clientInfo>" +
             "<partnerID>" + ConfigurationManager.AppSettings["PartnerID"] + "</partnerID>" +
            "<username>" + ConfigurationManager.AppSettings["Username"] + "</username>" +
            "<userID>" + ConfigurationManager.AppSettings["UserID"] + "</userID>" +
            "<password>" + ConfigurationManager.AppSettings["HotelPassword"] + "</password>" +
            "<userType>" + ConfigurationManager.AppSettings["HotelClientType"] + "</userType>" +
            "</clientInfo><searchquery>" +
            "<currency>" + ConfigurationManager.AppSettings["CurrencyValue"] + "</currency>" +
            "<residentOfIndia>" + ConfigurationManager.AppSettings["ResidentOfIndia"] + "</residentOfIndia>" +
            "<hotelinfo><hotelid>hotelidValue</hotelid><webService>webServiceValue</webService>" +
            "<ratePlanType>ratePlanTypeValue</ratePlanType><roomTypeCode>roomTypeCodeValue</roomTypeCode>" +
            "<checkInDate>checkInDateValue</checkInDate><checkOutDate>checkOutDateValue</checkOutDate>" +
            "</hotelinfo></searchquery></arzHotelPolicyReq>";

        public string ProvisionalBookingXml = "<arzHotelProvReq><clientInfo>" +
            "<sessionid>sessionidValue</sessionid>" +
            "<partnerID>" + ConfigurationManager.AppSettings["PartnerID"] + "</partnerID>" +
            "<username>" + ConfigurationManager.AppSettings["Username"] + "</username>" +
            "<userID>" + ConfigurationManager.AppSettings["UserID"] + "</userID>" +
            "<password>" + ConfigurationManager.AppSettings["HotelPassword"] + "</password>" +
            "<userType>" + ConfigurationManager.AppSettings["HotelClientType"] + "</userType>" +
            "</clientInfo>" +
            "<allocquery><currency>" + ConfigurationManager.AppSettings["CurrencyValue"] + "</currency>" +
            "HotelInfoXmlValue roomStayCandidateXmlValue" +
            "ratebandsXmlValue" +
            "guestInformationXmlValue</allocquery></arzHotelProvReq>";

        //public string ProvisionalBookingXmlTest = "<arzHotelProvReq><clientInfo><username>DiscountTadkaXML</username><password>*418E0680533546D4AE73578D8E5774B57ED85E38</password><partnerID>100200</partnerID><sessionid>41723658540038361200</sessionid><userID>49746</userID><userType>ArzooHWS1.1</userType></clientInfo>"+
        //    "<allocquery><currency>" + ConfigurationManager.AppSettings["CurrencyValue"] + "</currency>" +
        //    "HotelInfoXmlValue roomStayCandidateXmlValue" +
        //    "ratebandsXmlValue" +
        //    "guestInformationXmlValue</allocquery></arzHotelProvReq>";

        public string BookingConfirmationRequestXml = "<arzHotelBookingReq><bookingrequest><clientInfo>" +
    "<partnerID>" + ConfigurationManager.AppSettings["PartnerID"] + "</partnerID>" +
    "<username>" + ConfigurationManager.AppSettings["Username"] + "</username>" +
    "<userID>" + ConfigurationManager.AppSettings["UserID"] + "</userID>" +
    "<password>" + ConfigurationManager.AppSettings["HotelPassword"] + "</password>" +
    "<userType>" + ConfigurationManager.AppSettings["HotelClientType"] + "</userType>" +
    "</clientInfo><hotelinfo><hotelid>hotelidValue</hotelid><webService>webServiceValue</webService>" +
    "<ratePlanType>ratePlanTypeValue</ratePlanType><roomTypeCode>roomTypeCodeValue</roomTypeCode>" +
    "<city>cityValue</city></hotelinfo><bookinginfo><fromallocation>fromallocationValue</fromallocation>" +
    "<allocid>allocidValue</allocid><fromdate>fromdateValue</fromdate><todate>todateValue</todate>" +
    "<roomtype>roomtypeValue</roomtype><wsKey>wsKeyValue</wsKey><roombasis>roombasisValue</roombasis>" +
    "roomStayCandidateXmlValue</bookinginfo>" +
    "guestInformationXmlValue" +
    "</bookingrequest></arzHotelBookingReq>";

        public string CancelRequestXml = "<arzHotelCancellationReq><clientInfo>" +
            "<partnerID>" + ConfigurationManager.AppSettings["PartnerID"] + "</partnerID>" +
            "<username>" + ConfigurationManager.AppSettings["Username"] + "</username>" +
            "<userID>" + ConfigurationManager.AppSettings["UserID"] + "</userID>" +
            "<password>" + ConfigurationManager.AppSettings["HotelPassword"] + "</password>" +
            "<userType>" + ConfigurationManager.AppSettings["HotelClientType"] + "</userType>" +
            "</clientInfo><cancellationinfo><email>emailValue</email>" +
            "<currency>" + ConfigurationManager.AppSettings["CurrencyValue"] + "</currency>" +
            "<lastName>lastNameValue</lastName><bookingref>bookingrefValue</bookingref>" +
            "<webService>webServiceValue</webService></cancellationinfo></arzHotelCancellationReq>";
    }
}

