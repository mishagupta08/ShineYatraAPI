namespace ShineYatraApi.Models
{
    #region namespace

    using System.Collections.Generic;
    using System.Xml.Serialization;

    #endregion namespace

    [XmlRoot(ElementName = "Request")]
    public class Request
    {
        [XmlElement(ElementName = "Origin")]
        public string Origin { get; set; }

        [XmlElement(ElementName = "Destination")]
        public string Destination { get; set; }

        [XmlElement(ElementName = "DepartDate")]
        public string DepartDate { get; set; }

        [XmlElement(ElementName = "ReturnDate")]
        public string ReturnDate { get; set; }

        [XmlElement(ElementName = "AdultPax")]
        public string AdultPax { get; set; }

        [XmlElement(ElementName = "ChildPax")]
        public string ChildPax { get; set; }

        [XmlElement(ElementName = "InfantPax")]
        public string InfantPax { get; set; }

        [XmlElement(ElementName = "Currency")]
        public string Currency { get; set; }

        [XmlElement(ElementName = "Clientid")]
        public string Clientid { get; set; }

        [XmlElement(ElementName = "Clienttype")]
        public string Clienttype { get; set; }

        [XmlElement(ElementName = "Preferredclass")]
        public string Preferredclass { get; set; }

        [XmlElement(ElementName = "mode")]
        public string Mode { get; set; }
    }

    [XmlRoot(ElementName = "ChargeableFares")]
    public class ChargeableFares
    {
        [XmlElement(ElementName = "ActualBaseFare")]
        public string ActualBaseFare { get; set; }

        [XmlElement(ElementName = "Tax")]
        public string Tax { get; set; }

        [XmlElement(ElementName = "STax")]
        public string STax { get; set; }

        [XmlElement(ElementName = "SCharge")]
        public string SCharge { get; set; }

        [XmlElement(ElementName = "TDiscount")]
        public string TDiscount { get; set; }

        [XmlElement(ElementName = "TPartnerCommission")]
        public string TPartnerCommission { get; set; }
    }

    [XmlRoot(ElementName = "NonchargeableFares")]
    public class NonchargeableFares
    {
        [XmlElement(ElementName = "TCharge")]
        public string TCharge { get; set; }

        [XmlElement(ElementName = "TMarkup")]
        public string TMarkup { get; set; }

        [XmlElement(ElementName = "TSdiscount")]
        public string TSdiscount { get; set; }
    }

    [XmlRoot(ElementName = "FareDetails")]
    public class FareDetails
    {
        [XmlElement(ElementName = "ChargeableFares")]
        public ChargeableFares ChargeableFares { get; set; }

        [XmlElement(ElementName = "NonchargeableFares")]
        public NonchargeableFares NonchargeableFares { get; set; }
    }

    [XmlRoot(ElementName = "BookingClass")]
    public class BookingClass
    {
        [XmlElement(ElementName = "Availability")]
        public string Availability { get; set; }

        [XmlElement(ElementName = "ResBookDesigCode")]
        public string ResBookDesigCode { get; set; }
    }

    [XmlRoot(ElementName = "BookingClassFare")]
    public class BookingClassFare
    {
        [XmlElement(ElementName = "adultFare")]
        public string AdultFare { get; set; }

        [XmlElement(ElementName = "bookingclass")]
        public string Bookingclass { get; set; }

        [XmlElement(ElementName = "classType")]
        public string ClassType { get; set; }

        [XmlElement(ElementName = "farebasiscode")]
        public string Farebasiscode { get; set; }

        [XmlElement(ElementName = "Rule")]
        public string Rule { get; set; }

        [XmlElement(ElementName = "adultCommission")]
        public string AdultCommission { get; set; }

        [XmlElement(ElementName = "childCommission")]
        public string ChildCommission { get; set; }

        [XmlElement(ElementName = "commissionOnTCharge")]
        public string CommissionOnTCharge { get; set; }
    }

    [XmlRoot(ElementName = "FlightSegment")]
    public class FlightSegment
    {
        [XmlElement(ElementName = "AirEquipType")]
        public string AirEquipType { get; set; }

        [XmlElement(ElementName = "ArrivalAirportCode")]
        public string ArrivalAirportCode { get; set; }

        [XmlElement(ElementName = "ArrivalDateTime")]
        public string ArrivalDateTime { get; set; }

        [XmlElement(ElementName = "DepartureAirportCode")]
        public string DepartureAirportCode { get; set; }

        [XmlElement(ElementName = "DepartureDateTime")]
        public string DepartureDateTime { get; set; }

        [XmlElement(ElementName = "FlightNumber")]
        public string FlightNumber { get; set; }

        [XmlElement(ElementName = "OperatingAirlineCode")]
        public string OperatingAirlineCode { get; set; }

        [XmlElement(ElementName = "OperatingAirlineFlightNumber")]
        public string OperatingAirlineFlightNumber { get; set; }

        [XmlElement(ElementName = "RPH")]
        public string RPH { get; set; }

        [XmlElement(ElementName = "StopQuantity")]
        public string StopQuantity { get; set; }

        [XmlElement(ElementName = "airLineName")]
        public string AirLineName { get; set; }

        [XmlElement(ElementName = "airportTax")]
        public string AirportTax { get; set; }

        [XmlElement(ElementName = "imageFileName")]
        public string ImageFileName { get; set; }

        [XmlElement(ElementName = "viaFlight")]
        public string ViaFlight { get; set; }

        [XmlElement(ElementName = "BookingClass")]
        public BookingClass BookingClass { get; set; }

        [XmlElement(ElementName = "BookingClassFare")]
        public BookingClassFare BookingClassFare { get; set; }

        [XmlElement(ElementName = "Discount")]
        public string Discount { get; set; }

        [XmlElement(ElementName = "airportTaxChild")]
        public string AirportTaxChild { get; set; }

        [XmlElement(ElementName = "airportTaxInfant")]
        public string AirportTaxInfant { get; set; }

        [XmlElement(ElementName = "adultTaxBreakup")]
        public string AdultTaxBreakup { get; set; }

        [XmlElement(ElementName = "childTaxBreakup")]
        public string ChildTaxBreakup { get; set; }

        [XmlElement(ElementName = "infantTaxBreakup")]
        public string InfantTaxBreakup { get; set; }

        [XmlElement(ElementName = "octax")]
        public string Octax { get; set; }
    }

    [XmlRoot(ElementName = "FlightSegments")]
    public class FlightSegments
    {
        [XmlElement(ElementName = "FlightSegment")]
        public List<FlightSegment> FlightSegment { get; set; }
    }

    [XmlRoot(ElementName = "OriginDestinationOption")]
    public class OriginDestinationOption
    {
        [XmlElement(ElementName = "FareDetails")]
        public FareDetails FareDetails { get; set; }

        [XmlElement(ElementName = "FlightSegments")]
        public FlightSegments FlightSegments { get; set; }

        [XmlElement(ElementName = "id")]
        public string Id { get; set; }

        [XmlElement(ElementName = "key")]
        public string Key { get; set; }
    }

    [XmlRoot(ElementName = "OriginDestinationOptions")]
    public class OriginDestinationOptions
    {
        [XmlElement(ElementName = "OriginDestinationOption")]
        public List<OriginDestinationOption> OriginDestinationOption { get; set; }
    }

    [XmlRoot(ElementName = "Response__Depart")]
    public class Response__Depart
    {
        [XmlElement(ElementName = "OriginDestinationOptions")]
        public OriginDestinationOptions OriginDestinationOptions { get; set; }
    }

    [XmlRoot(ElementName = "Response__Return")]
    public class Response__Return
    {
        [XmlElement(ElementName = "OriginDestinationOptions")]
        public OriginDestinationOptions OriginDestinationOptions { get; set; }
    }

    [XmlRoot(ElementName = "arzoo__response")]
    public class Arzoo__response
    {
        [XmlElement(ElementName = "Request")]
        public Request Request { get; set; }

        [XmlElement(ElementName = "Response__Depart")]
        public Response__Depart Response__Depart { get; set; }

        [XmlElement(ElementName = "Response__Return")]
        public Response__Return Response__Return { get; set; }

        [XmlElement(ElementName = "error__tag")]
        public string Error__tag { get; set; }
    }

    /******Booking Status*******/

    [XmlRoot(ElementName = "Eticket")]
    public class Eticket
    {
        [XmlElement(ElementName = "givenName")]
        public string GivenName { get; set; }

        [XmlElement(ElementName = "surName")]
        public string SurName { get; set; }

        [XmlElement(ElementName = "nameReference")]
        public string NameReference { get; set; }

        [XmlElement(ElementName = "eticketno")]
        public string Eticketno { get; set; }

        [XmlElement(ElementName = "flightuid")]
        public string Flightuid { get; set; }

        [XmlElement(ElementName = "passuid")]
        public string Passuid { get; set; }
    }

    [XmlRoot(ElementName = "eticketdto")]
    public class Eticketdto
    {
        [XmlElement(ElementName = "Eticket")]
        public Eticket Eticket { get; set; }
    }

    [XmlRoot(ElementName = "OriDestPNRRequest")]
    public class OriDestPNRRequest
    {
        [XmlElement(ElementName = "flightno")]
        public string Flightno { get; set; }

        [XmlElement(ElementName = "eticketdto")]
        public Eticketdto Eticketdto { get; set; }

        [XmlElement(ElementName = "confirmationid")]
        public string Confirmationid { get; set; }

        [XmlElement(ElementName = "pnrnumber")]
        public string Pnrnumber { get; set; }
    }

    [XmlRoot(ElementName = "origindestinationoptions")]
    public class Origindestinationoptions
    {
        [XmlElement(ElementName = "OriDestPNRRequest")]
        public OriDestPNRRequest OriDestPNRRequest { get; set; }
    }

    [XmlRoot(ElementName = "requestedPNR")]
    public class RequestedPNR
    {
        [XmlElement(ElementName = "origindestinationoptions")]
        public Origindestinationoptions Origindestinationoptions { get; set; }

        [XmlElement(ElementName = "transid")]
        public string Transid { get; set; }

        [XmlElement(ElementName = "status")]
        public string Status { get; set; }

        [XmlElement(ElementName = "partnerRefId")]
        public string PartnerRefId { get; set; }
    }

    [XmlRoot(ElementName = "EticketDetails")]
    public class EticketDetails
    {
        [XmlElement(ElementName = "requestedPNR")]
        public RequestedPNR RequestedPNR { get; set; }

        [XmlElement(ElementName = "transid")]
        public string Transid { get; set; }

        [XmlElement(ElementName = "status")]
        public string Status { get; set; }

        [XmlElement(ElementName = "partnerRefId")]
        public string PartnerRefId { get; set; }
    }

    /*****Cancelticket*****/

    [XmlRoot(ElementName = "telePhone")]
    public class TelePhone
    {
        [XmlElement(ElementName = "phoneNumber")]
        public string PhoneNumber { get; set; }
    }

    [XmlRoot(ElementName = "email")]
    public class Email
    {
        [XmlElement(ElementName = "emailAddress")]
        public string EmailAddress { get; set; }
    }

    [XmlRoot(ElementName = "onwardcanceldata")]
    public class Onwardcanceldata
    {
        [XmlElement(ElementName = "Canid")]
        public string Canid { get; set; }

        [XmlElement(ElementName = "remarks")]
        public string Remarks { get; set; }

        [XmlElement(ElementName = "status")]
        public string Status { get; set; }

        [XmlElement(ElementName = "eticketdto")]
        public Eticketdto Eticketdto { get; set; }
    }

    [XmlRoot(ElementName = "cancelationdtls")]
    public class Cancelationdtls
    {
        [XmlElement(ElementName = "onwardcanceldata")]
        public Onwardcanceldata Onwardcanceldata { get; set; }
    }

    [XmlRoot(ElementName = "CancelationDetails")]
    public class CancelationDetails
    {
        [XmlElement(ElementName = "requestedPNR")]
        public RequestedPNR RequestedPNR { get; set; }

        [XmlElement(ElementName = "cancelationdtls")]
        public Cancelationdtls Cancelationdtls { get; set; }

        [XmlElement(ElementName = "error")]
        public string Error { get; set; }

        [XmlElement(ElementName = "transid")]
        public string Transid { get; set; }
    }

    /*******Cancellation status******/

    [XmlRoot(ElementName = "Cancellation")]
    public class Cancellation
    {
        [XmlElement(ElementName = "CancellationId")]
        public string CancellationId { get; set; }

        [XmlElement(ElementName = "CancellationStatus")]
        public string CancellationStatus { get; set; }

        [XmlElement(ElementName = "CancellationCharges")]
        public string CancellationCharges { get; set; }
    }

    [XmlRoot(ElementName = "Cancellations")]
    public class Cancellations
    {
        [XmlElement(ElementName = "Cancellation")]
        public Cancellation Cancellation { get; set; }
    }

    [XmlRoot(ElementName = "EticketCanStatusRes")]
    public class EticketCanStatusRes
    {
        [XmlElement(ElementName = "transid")]
        public string Transid { get; set; }

        [XmlElement(ElementName = "partnerRefId")]
        public string PartnerRefId { get; set; }

        [XmlElement(ElementName = "Cancellations")]
        public Cancellations Cancellations { get; set; }

        [XmlElement(ElementName = "CancellationId")]
        public string CancellationId { get; set; }

        [XmlElement(ElementName = "error")]
        public string Error { get; set; }
    }

    /***Pricing Rsponse***/

    [XmlRoot(ElementName = "pricingresponse")]
    public class Pricingresponse
    {
        [XmlElement(ElementName = "onwardFlights")]
        public OnwardFlights OnwardFlights { get; set; }

        [XmlElement(ElementName = "error")]
        public string Error { get; set; }
    }

    [XmlRoot(ElementName = "onwardFlights")]
    public class OnwardFlights
    {
        [XmlElement(ElementName = "OriginDestinationOption")]
        public OriginDestinationOption OriginDestinationOption { get; set; }
    }
}