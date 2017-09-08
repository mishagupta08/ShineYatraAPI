namespace ShineYatraApi
{
    #region namespace

    using System.Configuration;

    #endregion namepsace

    public class Templates
    {
        /// <summary>
        /// Hold value of search flight
        /// </summary>
        public string SearchFlightXml = "<Request>" +
            "<Origin>OriginValue</Origin>" +
            "<Destination>DestinationValue</Destination>" +
            "<DepartDate>DepartDateValue</DepartDate>" +
            "<ReturnDate>ReturnDateValue</ReturnDate>" +
            "<AdultPax>AdultCountValue</AdultPax>" +
            "<ChildPax>ChildCountValue</ChildPax>" +
            "<InfantPax>InfantCountValue</InfantPax>" +
            "<Currency>" + ConfigurationManager.AppSettings["CurrencyValue"] + "</Currency>" +
            "<Clientid>" + ConfigurationManager.AppSettings["Clientid"] + "</Clientid>" +
            "<Clientpassword>" + ConfigurationManager.AppSettings["Clientpassword"] + "</Clientpassword>" +
            "<Clienttype>" + ConfigurationManager.AppSettings["Clienttype"] + "</Clienttype>" +
            "<Preferredclass>PreferredClassValue</Preferredclass>" +
            "<mode>ModeValue</mode>" +
            "<PreferredAirline></PreferredAirline>" +
            "</Request>";

        public string FlightPricingXml = "<pricingrequest><onwardFlights>FlghtDetailXmlTemplate</onwardFlights>" +
                "<returnFlights/><telePhone/><email/><creditcardno/>" +
                "<Clientid>" + ConfigurationManager.AppSettings["Clientid"] + "</Clientid>" +
            "<Clientpassword>" + ConfigurationManager.AppSettings["Clientpassword"] + "</Clientpassword>" +
            "<Clienttype>" + ConfigurationManager.AppSettings["Clienttype"] + "</Clienttype>" +
             "<AdultPax>AdultCountValue</AdultPax>" +
            "<ChildPax>ChildCountValue</ChildPax>" +
            "<InfantPax>InfantCountValue</InfantPax>" +
            "</pricingrequest>";

        public string BookingRequestXml = "<Bookingrequest><onwardFlights>FlghtDetailXmlTemplate</onwardFlights>" +
            "<returnFlights/><creditcardno>creditcardnoValue</creditcardno>" +
            "<Clientid>" + ConfigurationManager.AppSettings["Clientid"] + "</Clientid>" +
            "<Clientpassword>" + ConfigurationManager.AppSettings["Clientpassword"] + "</Clientpassword>" +
            "<Clienttype>" + ConfigurationManager.AppSettings["Clienttype"] + "</Clienttype>" +
            "<AdultPax>AdultCountValue</AdultPax>" +
            "<ChildPax>ChildCountValue</ChildPax>" +
            "<InfantPax>InfantCountValue</InfantPax>" +
            "<partnerRefId>partnerRefIdValue</partnerRefId>" +
            "CustomerInformationXmlValue" +
            "<telePhone><phoneNumber>phoneNumberValue</phoneNumber></telePhone>" +
            "<email><emailAddress>emailAddressValue</emailAddress></email></Bookingrequest>";

        public string BookingStatusXml = "<EticketRequest>" +
"<Clientid>" + ConfigurationManager.AppSettings["Clientid"] + "</Clientid>" +
            "<Clientpassword>" + ConfigurationManager.AppSettings["Clientpassword"] + "</Clientpassword>" +
            "<Clienttype>" + ConfigurationManager.AppSettings["Clienttype"] + "</Clienttype>" +
          "<transid>transidValue</transid>" +
"<partnerRefId>partnerRefIdValue</partnerRefId></EticketRequest>";

        public string CancelRequestXml = "<CancelationDetails>" +
 "<Clientid>" + ConfigurationManager.AppSettings["Clientid"] + "</Clientid>" +
            "<Clientpassword>" + ConfigurationManager.AppSettings["Clientpassword"] + "</Clientpassword>" +
            "<Clienttype>" + ConfigurationManager.AppSettings["Clienttype"] + "</Clienttype>" +
            "<transid>transidValue</transid><status>Successtkd</status>" +
            "<remarks>transaction Cancellation</remarks>" +
            "<eticketdto>EticketXmlValue</eticketdto></CancelationDetails>";

        public string CancelRequestStatusXml = "<EticketCanStatusReq>" +
            "<Clientid>" + ConfigurationManager.AppSettings["Clientid"] + "</Clientid>" +
            "<Clientpassword>" + ConfigurationManager.AppSettings["Clientpassword"] + "</Clientpassword>" +
            "<Clienttype>" + ConfigurationManager.AppSettings["Clienttype"] + "</Clienttype>" +
            "<transid>transidValue</transid><partnerRefId>partnerRefIdValue</partnerRefId>" +
            "<CancellationId>CancellationIdValue</CancellationId></EticketCanStatusReq>";
    }
}