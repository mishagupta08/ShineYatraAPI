using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace ShineYatraApi.Models
{
    [XmlRoot(ElementName = "Fare")]
    public class Fare
    {
        [XmlElement(ElementName = "PsgrType")]
        public string PsgrType { get; set; }
        [XmlElement(ElementName = "BaseFare")]
        public string BaseFare { get; set; }
        [XmlElement(ElementName = "Tax")]
        public string Tax { get; set; }
    }

    [XmlRoot(ElementName = "FareAry")]
    public class FareAry
    {
        [XmlElement(ElementName = "Fare")]
        public Fare Fare { get; set; }
    }

    [XmlRoot(ElementName = "FareBreakup")]
    public class FareBreakup
    {
        [XmlElement(ElementName = "FareAry")]
        public FareAry FareAry { get; set; }
    }

    [XmlRoot(ElementName = "FareDetails")]
    public class FareDetailsInt
    {
        [XmlElement(ElementName = "ActualBaseFare")]
        public string ActualBaseFare { get; set; }
        [XmlElement(ElementName = "Tax")]
        public string Tax { get; set; }
        [XmlElement(ElementName = "STax")]
        public string STax { get; set; }
        [XmlElement(ElementName = "TCharge")]
        public string TCharge { get; set; }
        [XmlElement(ElementName = "SCharge")]
        public string SCharge { get; set; }
        [XmlElement(ElementName = "TDiscount")]
        public string TDiscount { get; set; }
        [XmlElement(ElementName = "TMarkup")]
        public string TMarkup { get; set; }
        [XmlElement(ElementName = "TPartnerCommission")]
        public string TPartnerCommission { get; set; }
        [XmlElement(ElementName = "TSdiscount")]
        public string TSdiscount { get; set; }
        [XmlElement(ElementName = "FareBreakup")]
        public FareBreakup FareBreakup { get; set; }
    }

    [XmlRoot(ElementName = "BookingClass")]
    public class BookingClassInt
    {
        [XmlElement(ElementName = "Availability")]
        public string Availability { get; set; }
        [XmlElement(ElementName = "BIC")]
        public string BIC { get; set; }
    }

    [XmlRoot(ElementName = "Psgr")]
    public class Psgr
    {
        [XmlElement(ElementName = "PsgrType")]
        public string PsgrType { get; set; }
        [XmlElement(ElementName = "BaseFare")]
        public string BaseFare { get; set; }
        [XmlElement(ElementName = "Tax")]
        public string Tax { get; set; }
        [XmlElement(ElementName = "BagInfo")]
        public string BagInfo { get; set; }
    }

    [XmlRoot(ElementName = "PsgrAry")]
    public class PsgrAry
    {
        [XmlElement(ElementName = "Psgr")]
        public Psgr Psgr { get; set; }
    }

    [XmlRoot(ElementName = "PsgrBreakup")]
    public class PsgrBreakup
    {
        [XmlElement(ElementName = "PsgrAry")]
        public PsgrAry PsgrAry { get; set; }
    }

    [XmlRoot(ElementName = "BookingClassFare")]
    public class BookingClassFareInt
    {
        [XmlElement(ElementName = "bookingclass")]
        public string BookingclassInt { get; set; }
        [XmlElement(ElementName = "classType")]
        public string ClassType { get; set; }
        [XmlElement(ElementName = "farebasiscode")]
        public string Farebasiscode { get; set; }
        [XmlElement(ElementName = "Rule")]
        public string Rule { get; set; }
        [XmlElement(ElementName = "PsgrBreakup")]
        public PsgrBreakup PsgrBreakup { get; set; }
    }

    [XmlRoot(ElementName = "FlightSegment")]
    public class FlightSegmentInt
    {
        [XmlElement(ElementName = "AirEquipType")]
        public string AirEquipType { get; set; }
        [XmlElement(ElementName = "ArrivalAirportCode")]
        public string ArrivalAirportCode { get; set; }
        [XmlElement(ElementName = "ArrivalAirportName")]
        public string ArrivalAirportName { get; set; }
        [XmlElement(ElementName = "ArrivalDateTime")]
        public string ArrivalDateTime { get; set; }
        [XmlElement(ElementName = "DepartureAirportCode")]
        public string DepartureAirportCode { get; set; }
        [XmlElement(ElementName = "DepartureAirportName")]
        public string DepartureAirportName { get; set; }
        [XmlElement(ElementName = "DepartureDateTime")]
        public string DepartureDateTime { get; set; }
        [XmlElement(ElementName = "FlightNumber")]
        public string FlightNumber { get; set; }
        [XmlElement(ElementName = "MarketingAirlineCode")]
        public string MarketingAirlineCode { get; set; }
        [XmlElement(ElementName = "OperatingAirlineCode")]
        public string OperatingAirlineCode { get; set; }
        [XmlElement(ElementName = "OperatingAirlineName")]
        public string OperatingAirlineName { get; set; }
        [XmlElement(ElementName = "OperatingAirlineFlightNumber")]
        public string OperatingAirlineFlightNumber { get; set; }
        [XmlElement(ElementName = "NumStops")]
        public string NumStops { get; set; }
        [XmlElement(ElementName = "LinkSellAgrmnt")]
        public string LinkSellAgrmnt { get; set; }
        [XmlElement(ElementName = "Conx")]
        public string Conx { get; set; }
        [XmlElement(ElementName = "AirpChg")]
        public string AirpChg { get; set; }
        [XmlElement(ElementName = "InsideAvailOption")]
        public string InsideAvailOption { get; set; }
        [XmlElement(ElementName = "GenTrafRestriction")]
        public string GenTrafRestriction { get; set; }
        [XmlElement(ElementName = "DaysOperates")]
        public string DaysOperates { get; set; }
        [XmlElement(ElementName = "JrnyTm")]
        public string JrnyTm { get; set; }
        [XmlElement(ElementName = "EndDt")]
        public string EndDt { get; set; }
        [XmlElement(ElementName = "StartTerminal")]
        public string StartTerminal { get; set; }
        [XmlElement(ElementName = "EndTerminal")]
        public string EndTerminal { get; set; }
        [XmlElement(ElementName = "FltTm")]
        public string FltTm { get; set; }
        [XmlElement(ElementName = "LSAInd")]
        public string LSAInd { get; set; }
        [XmlElement(ElementName = "Mile")]
        public string Mile { get; set; }
        [XmlElement(ElementName = "BookingClass")]
        public BookingClassInt BookingClass { get; set; }
        [XmlElement(ElementName = "BookingClassFare")]
        public BookingClassFareInt BookingClassFare { get; set; }
    }

    [XmlRoot(ElementName = "FlightSegments")]
    public class FlightSegmentsInt
    {
        [XmlElement(ElementName = "FlightSegment")]
        public List<FlightSegmentInt> FlightSegment { get; set; }
    }

    [XmlRoot(ElementName = "onward")]
    public class Onward
    {
        [XmlElement(ElementName = "FlightSegments")]
        public FlightSegmentsInt FlightSegments { get; set; }
    }

    [XmlRoot(ElementName = "Return")]
    public class Return
    {
        [XmlElement(ElementName = "FlightSegments")]
        public FlightSegmentsInt FlightSegments { get; set; }
    }

    [XmlRoot(ElementName = "OriginDestinationOption")]
    public class OriginDestinationOptionInt
    {
        [XmlElement(ElementName = "FareDetails")]
        public FareDetailsInt FareDetails { get; set; }
        [XmlElement(ElementName = "onward")]
        public Onward Onward { get; set; }
        [XmlElement(ElementName = "Return")]
        public Return Return { get; set; }
        [XmlElement(ElementName = "id")]
        public string Id { get; set; }
    }

    [XmlRoot(ElementName = "OriginDestinationOptions")]
    public class OriginDestinationOptionsInt
    {
        [XmlElement(ElementName = "OriginDestinationOption")]
        public List<OriginDestinationOptionInt> OriginDestinationOption { get; set; }
    }

    [XmlRoot(ElementName = "AvailResponse")]
    public class AvailResponse
    {
        [XmlElement(ElementName = "OriginDestinationOptions")]
        public OriginDestinationOptionsInt OriginDestinationOptions { get; set; }
    }

    [XmlRoot(ElementName = "arzoo_response")]
    public class Arzoo_responseInt
    {
        [XmlElement(ElementName = "AvailResponse")]
        public AvailResponse AvailResponse { get; set; }

        [XmlElement(ElementName = "Error")]
        public string Error { get; set; }
    }

    /*****Pricing Response class*****/

    [XmlRoot(ElementName = "PriceResponse")]
    public class PriceResponseInt
    {
        [XmlElement(ElementName = "error")]
        public string Error { get; set; }
        [XmlElement(ElementName = "OriginDestinationOption")]
        public OriginDestinationOptionInt OriginDestinationOption { get; set; }
    }

    /****Booking Confirmation response classes******/

    [XmlRoot(ElementName = "Eticket")]
    public class EticketInternational
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
    public class EticketdtoInternational
    {
        [XmlElement(ElementName = "Eticket")]
        public List<EticketInternational> Eticket { get; set; }
    }

    [XmlRoot(ElementName = "FlightSegment")]
    public class FlightSegmentInternational
    {
        [XmlElement(ElementName = "confirmationid")]
        public string Confirmationid { get; set; }

        [XmlElement(ElementName = "pnrnumber")]
        public string Pnrnumber { get; set; }

        [XmlElement(ElementName = "eticketdto")]
        public EticketdtoInternational Eticketdto { get; set; }
    }

    [XmlRoot(ElementName = "FlightSegments")]
    public class FlightSegmentsInternational
    {
        [XmlElement(ElementName = "FlightSegment")]
        public List<FlightSegmentInternational> FlightSegment { get; set; }
    }

    [XmlRoot(ElementName = "onward")]
    public class OnwardInt
    {
        [XmlElement(ElementName = "FlightSegments")]
        public FlightSegmentsInternational FlightSegments { get; set; }
    }

    [XmlRoot(ElementName = "Return")]
    public class ReturnInt
    {
        [XmlElement(ElementName = "FlightSegments")]
        public FlightSegmentsInternational FlightSegments { get; set; }
    }

    [XmlRoot(ElementName = "OriginDestinationOptions")]
    public class OriginDestinationOptionsInternational
    {
        [XmlElement(ElementName = "onward")]
        public OnwardInt Onward { get; set; }
        [XmlElement(ElementName = "Return")]
        public ReturnInt Return { get; set; }
    }

    [XmlRoot(ElementName = "CustomerInfo")]
    public class CustomerInfo
    {
        [XmlElement(ElementName = "givenName")]
        public string GivenName { get; set; }
        [XmlElement(ElementName = "surName")]
        public string SurName { get; set; }
        [XmlElement(ElementName = "nameReference")]
        public string NameReference { get; set; }
        [XmlElement(ElementName = "dob")]
        public string Dob { get; set; }
        [XmlElement(ElementName = "age")]
        public string Age { get; set; }
        [XmlElement(ElementName = "psgrtype")]
        public string Psgrtype { get; set; }
    }

    [XmlRoot(ElementName = "personName")]
    public class PersonName
    {
        [XmlElement(ElementName = "CustomerInfo")]
        public List<CustomerInfo> CustomerInfo { get; set; }
    }

    [XmlRoot(ElementName = "EticketDetails")]
    public class EticketDetailsInternational
    {
        [XmlElement(ElementName = "transid")]
        public string Transid { get; set; }
        [XmlElement(ElementName = "OriginDestinationOptions")]
        public OriginDestinationOptionsInternational OriginDestinationOptions { get; set; }
        [XmlElement(ElementName = "status")]
        public string Status { get; set; }
        [XmlElement(ElementName = "error")]
        public string Error { get; set; }
    }

    /****Cancel response****/

    [XmlRoot(ElementName = "onwardcanceldata")]
    public class OnwardcanceldataInt
    {
        [XmlElement(ElementName = "CancellationId")]
        public string CancellationId { get; set; }
        [XmlElement(ElementName = "Remarks")]
        public string Remarks { get; set; }
        [XmlElement(ElementName = "Status")]
        public string Status { get; set; }
        [XmlElement(ElementName = "eticketdto")]
        public EticketdtoInternational Eticketdto { get; set; }
    }

    [XmlRoot(ElementName = "cancelationdtls")]
    public class CancelationdtlsInt
    {
        [XmlElement(ElementName = "onwardcanceldata")]
        public OnwardcanceldataInt Onwardcanceldata { get; set; }
    }

    [XmlRoot(ElementName = "CanIntResponse")]
    public class CanIntResponse
    {
        [XmlElement(ElementName = "error")]
        public string Error { get; set; }
        [XmlElement(ElementName = "Transid")]
        public string Transid { get; set; }
        [XmlElement(ElementName = "cancelationdtls")]
        public CancelationdtlsInt Cancelationdtls { get; set; }
    }

    /******Cancel status response*******/

    [XmlRoot(ElementName = "Cancellation")]
    public class CancellationInt
    {
        [XmlElement(ElementName = "CancellationId")]
        public string CancellationId { get; set; }
        [XmlElement(ElementName = "CancellationStatus")]
        public string CancellationStatus { get; set; }
        [XmlElement(ElementName = "CancellationProcessDateTime")]
        public string CancellationProcessDateTime { get; set; }
        [XmlElement(ElementName = "CancellationCharges")]
        public string CancellationCharges { get; set; }
        [XmlElement(ElementName = "RefundStatus")]
        public string RefundStatus { get; set; }
        [XmlElement(ElementName = "FinalRefundAmount")]
        public string FinalRefundAmount { get; set; }
        [XmlElement(ElementName = "RefundDateTime")]
        public string RefundDateTime { get; set; }
    }

    [XmlRoot(ElementName = "Cancellations")]
    public class CancellationsInt
    {
        [XmlElement(ElementName = "Cancellation")]
        public CancellationInt Cancellation { get; set; }
    }

    [XmlRoot(ElementName = "CanStatusIntResponse")]
    public class CanStatusIntResponse
    {
        [XmlElement(ElementName = "Transid")]
        public string Transid { get; set; }

        [XmlElement(ElementName = "Error")]
        public string Error { get; set; }

        [XmlElement(ElementName = "Cancellations")]
        public CancellationsInt Cancellations { get; set; }
    }

}