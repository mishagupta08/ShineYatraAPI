namespace ShineYatraApi
{
    #region namespace

    using System.Configuration;

    #endregion namepsace

    /// <summary>
    /// Hold request template for international flight
    /// </summary>
    public class InternationalFlightTemplate
    {
        /// <summary>
        /// Hold value of search flight
        /// </summary>
        public string SearchFlightIntXml = "<AvailRequest>" +
            "<Origin>OriginValue</Origin>" +
            "<Destination>DestinationValue</Destination>" +
            "<DepartDate>DepartDateValue</DepartDate>" +
            "<ReturnDate>ReturnDateValue</ReturnDate>" +
            "<AdultPax>AdultCountValue</AdultPax>" +
            "<ChildPax>ChildCountValue</ChildPax>" +
            "<InfantPax>InfantCountValue</InfantPax>" +
            "<Currency>" + ConfigurationManager.AppSettings["CurrencyValue"] + "</Currency>" +
            "<Clientid>" + ConfigurationManager.AppSettings["Clientid"] + "</Clientid>" +
            "<Clientpassword>" + ConfigurationManager.AppSettings["HotelPassword"] + "</Clientpassword>" +
            "<Clienttype>" + ConfigurationManager.AppSettings["InternationalClienttype"] + "</Clienttype>" +
            "<PreferredClass>PreferredClassValue</PreferredClass>" +
            "<Trip>ModeValue</Trip><Eticket>true</Eticket>" +
            "<PreferredAirline></PreferredAirline>" +
            "</AvailRequest>";

        public string FlightPricingIntXml = "<pricingrequest>FlghtDetailXmlTemplate" +
                "<returnFlights/><telePhone/><email/><creditcardno/>" +
                "<Clientid>" + ConfigurationManager.AppSettings["Clientid"] + "</Clientid>" +
            "<Clientpassword>" + ConfigurationManager.AppSettings["HotelPassword"] + "</Clientpassword>" +
            "<Clienttype>" + ConfigurationManager.AppSettings["InternationalClienttype"] + "</Clienttype>" +
             "<noadults>AdultCountValue</noadults>" +
            "<nochild>ChildCountValue</nochild>" +
            "<noinfant>InfantCountValue</noinfant>" +
            "</pricingrequest>";

        public string BookingRequestXml = "<Bookingrequest>FlghtDetailXmlTemplate" +
            "<creditcardno>creditcardnoValue</creditcardno>" +
            "<Clientid>" + ConfigurationManager.AppSettings["Clientid"] + "</Clientid>" +
            "<Clientpassword>" + ConfigurationManager.AppSettings["Clientpassword"] + "</Clientpassword>" +
            "<Clienttype>" + ConfigurationManager.AppSettings["InternationalClienttype"] + "</Clienttype>" +
            "<noadults>AdultCountValue</noadults>" +
            "<nochild>ChildCountValue</nochild>" +
            "<noinfant>InfantCountValue</noinfant>" +
            "<PartnerreferenceID>partnerRefIdValue</PartnerreferenceID>" +
            "CustomerInformationXmlValue" +
            "<telePhone><phoneNumber>phoneNumberValue</phoneNumber></telePhone>" +
            "<email><emailAddress>emailAddressValue</emailAddress></email></Bookingrequest>";

        public string BookingStatusIntXml = "<EticketRequest>" +
"<Clientid>" + ConfigurationManager.AppSettings["Clientid"] + "</Clientid>" +
          "<Clientpassword>" + ConfigurationManager.AppSettings["Clientpassword"] + "</Clientpassword>" +
          "<Clienttype>" + ConfigurationManager.AppSettings["InternationalClienttype"] + "</Clienttype>" +
        "<transid>transidValue</transid></EticketRequest>";

        public string CancelRequestXml = "<CanIntRequest>" +
"<Clientid>" + ConfigurationManager.AppSettings["Clientid"] + "</Clientid>" +
           "<Clientpassword>" + ConfigurationManager.AppSettings["Clientpassword"] + "</Clientpassword>" +
           "<Clienttype>" + ConfigurationManager.AppSettings["InternationalClienttype"] + "</Clienttype>" +
           "<Transid>transidValue</Transid>" +
           "<Remarks>transaction Cancellation</Remarks>" +
           "<eticketdto>EticketXmlValue</eticketdto></CanIntRequest>";

        public string CancelRequestStatusIntXml = "<CanStatusIntRequest>" +
           "<Clientid>" + ConfigurationManager.AppSettings["Clientid"] + "</Clientid>" +
           "<Clientpassword>" + ConfigurationManager.AppSettings["Clientpassword"] + "</Clientpassword>" +
           "<Clienttype>" + ConfigurationManager.AppSettings["InternationalClienttype"] + "</Clienttype>" +
           "<Transid>transidValue</Transid><PartnerRefId>partnerRefIdValue</PartnerRefId>" +
           "<CancellationId>CancellationIdValue</CancellationId></CanStatusIntRequest>";
    }
}