namespace ShineYatraApi.Controllers
{
    #region namespace

    using System.Linq;
    using System;
    using Search;
    using FlightPricing;
    using FlightBooking;
    using System.Web.Http;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Xml;
    using System.Net.Http;
    using System.Net;
    using System.Xml.Serialization;
    using System.IO;
    using Models;
    using FlightBookingStatus;
    using FlightCancellationRequest;
    using CanellationRequestStatus;
    using Models.Flight;
    using Models.HotelDetail;
    using System.Globalization;

    #endregion namespace

    public class FlightController : ApiController
    {
        HttpHelper hepler = new HttpHelper();
        /// <summary>
        /// Holds xml Templates 
        /// </summary>
        Templates TemplateXml = new Templates();

        // Get api/values
        [ActionName("GetCityList")]
        public IHttpActionResult GetCityList()
        {
            var cities = this.AssignDomesticCities();
            return Content(HttpStatusCode.OK, cities, Configuration.Formatters.XmlFormatter);
        }

        // POST api/values
        [ActionName("SearchFlight")]
        public async Task<HttpResponseMessage> SearchFlight()
        {
            if (hepler.CheckHeader(Request))
            {
                string responseString = string.Empty;
                HttpResponseMessage response = null;
                try
                {
                    var searchParamterXml = await Request.Content.ReadAsByteArrayAsync();
                    string searchParameter = System.Text.Encoding.UTF8.GetString(searchParamterXml);
                    XmlSerializer serializer = new XmlSerializer(typeof(Request));
                    using (TextReader reader = new StringReader(searchParameter))
                    {
                        Request searchDetail = (Request)serializer.Deserialize(reader);

                        var DepartDate = ConvertDateFormat(searchDetail.DepartDate);
                        var ReturnDate = ConvertDateFormat(searchDetail.ReturnDate);
                        var searchRequestObject = TemplateXml.SearchFlightXml;
                        searchRequestObject = searchRequestObject.Replace("OriginValue", searchDetail.Origin).
                            Replace("DestinationValue", searchDetail.Destination).
                            Replace("DepartDateValue", DepartDate).
                            Replace("ReturnDateValue", ReturnDate).
                            Replace("AdultCountValue", searchDetail.AdultPax).
                            Replace("ChildCountValue", searchDetail.ChildPax).
                            Replace("InfantCountValue", searchDetail.InfantPax).
                            Replace("PreferredClassValue", searchDetail.Preferredclass).
                            Replace("ModeValue", searchDetail.Mode);

                        var flight = new DOMFlightAvailability();
                        responseString = flight.getAvailability(searchRequestObject);
                        response = Request.CreateResponse<string>(HttpStatusCode.OK, responseString);

                        var serializer1 = new XmlSerializer(typeof(Arzoo__response));

                        Arzoo__response ArzooResponse;
                        using (TextReader reader1 = new StringReader(responseString))
                        {
                            ArzooResponse = (Arzoo__response)serializer1.Deserialize(reader1);
                        }

                        if (ArzooResponse == null)
                        {
                            response = Request.CreateResponse<string>(HttpStatusCode.NotFound, "<error>No Content Found</error>");
                        }
                        else if (!string.IsNullOrEmpty(ArzooResponse.Error__tag))
                        {
                            response = Request.CreateResponse<string>(HttpStatusCode.BadRequest, "<error>" + ArzooResponse.Error__tag + "</error>");
                        }
                        else
                        {
                            var flightListOption = new List<origindestinationoption>();
                            //var flightList = new List<FlightsDetail>();
                            foreach (var option in ArzooResponse.Response__Depart.OriginDestinationOptions.OriginDestinationOption)
                            {
                                var flightOption = new origindestinationoption();
                                flightOption.FareDetail = option.FareDetails;
                                flightOption.FlightsDetailList = new List<FlightsDetail>();
                                foreach (var flightSegment in option.FlightSegments.FlightSegment)
                                {
                                    flightOption.FlightsDetailList.Add(new FlightsDetail
                                    {
                                        ArrivalAirportCode = flightSegment.ArrivalAirportCode,
                                        ArrivalDateTime = flightSegment.ArrivalDateTime,
                                        Availability = flightSegment.BookingClass.Availability,
                                        Id = option.Id,
                                        DepartureAirportCode = flightSegment.DepartureAirportCode,
                                        DepartureDateTime = flightSegment.DepartureDateTime,
                                        FlightNumber = flightSegment.FlightNumber,
                                        BookingClassFare = flightSegment.BookingClassFare,
                                        ImageFileName = flightSegment.ImageFileName,
                                        OperatingAirlineCode = flightSegment.OperatingAirlineCode,
                                        StopQuantity = flightSegment.StopQuantity,
                                        AirLineName = flightSegment.AirLineName
                                    });
                                }

                                flightListOption.Add(flightOption);
                            }
                            if (ArzooResponse.Response__Return != null)
                            {
                                foreach (var option in ArzooResponse.Response__Return.OriginDestinationOptions.OriginDestinationOption)
                                {
                                    var flightOption = new origindestinationoption();
                                    flightOption.FareDetail = option.FareDetails;
                                    flightOption.FlightsDetailList = new List<FlightsDetail>();

                                    foreach (var flightSegment in option.FlightSegments.FlightSegment)
                                    {
                                        flightOption.FlightsDetailList.Add(new FlightsDetail
                                        {
                                            ArrivalAirportCode = flightSegment.ArrivalAirportCode,
                                            ArrivalDateTime = flightSegment.ArrivalDateTime,
                                            Availability = flightSegment.BookingClass.Availability,
                                            Id = option.Id,
                                            DepartureAirportCode = flightSegment.DepartureAirportCode,
                                            DepartureDateTime = flightSegment.DepartureDateTime,
                                            FlightNumber = flightSegment.FlightNumber,
                                            ImageFileName = flightSegment.ImageFileName,
                                            OperatingAirlineCode = flightSegment.OperatingAirlineCode,
                                            StopQuantity = flightSegment.StopQuantity,
                                            AirLineName = flightSegment.AirLineName,
                                            BookingClassFare = flightSegment.BookingClassFare,
                                            IsReturnFlight = true
                                        });
                                    }

                                    flightListOption.Add(flightOption);
                                }
                            }
                            if (flightListOption != null && flightListOption.Count > 0)
                            {
                                XmlSerializer xsSubmit = new XmlSerializer(typeof(List<origindestinationoption>));
                                var xml = "";

                                using (var sww = new StringWriter())
                                {
                                    using (XmlWriter writer = XmlWriter.Create(sww))
                                    {
                                        xsSubmit.Serialize(writer, flightListOption);
                                        xml = sww.ToString(); // Your XML
                                    }
                                }

                                response = Request.CreateResponse<string>(HttpStatusCode.OK, xml);
                            }
                            else
                            {
                                response = Request.CreateResponse<string>(HttpStatusCode.NotFound, "<error>No Content Found</error>");
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    responseString = e.Message;
                    response = Request.CreateResponse<string>(HttpStatusCode.NotFound, responseString);
                }

                return response;
            }

            return null;
        }

        [ActionName("FlightPricing")]
        public async Task<HttpResponseMessage> FlightPricing()
        {
            if (hepler.CheckHeader(Request))
            {
                string responseString = string.Empty;
                HttpResponseMessage response = null;
                try
                {
                    var searchParamterXml = await Request.Content.ReadAsByteArrayAsync();
                    string searchParameter = System.Text.Encoding.UTF8.GetString(searchParamterXml);
                    XmlSerializer serializer = new XmlSerializer(typeof(Model.Request));
                    using (TextReader reader = new StringReader(searchParameter))
                    {
                        Model.Request searchDetail = (Model.Request)serializer.Deserialize(reader);

                        var DepartDate = ConvertDateFormat(searchDetail.DepartDate);
                        var ReturnDate = ConvertDateFormat(searchDetail.ReturnDate);

                        var searchRequestObject = TemplateXml.SearchFlightXml;
                        searchRequestObject = searchRequestObject.Replace("OriginValue", searchDetail.Origin).
                            Replace("DestinationValue", searchDetail.Destination).
                            Replace("DepartDateValue", DepartDate).
                            Replace("ReturnDateValue", ReturnDate).
                            Replace("AdultCountValue", searchDetail.AdultPax).
                            Replace("ChildCountValue", searchDetail.ChildPax).
                            Replace("InfantCountValue", searchDetail.InfantPax).
                            Replace("PreferredClassValue", searchDetail.Preferredclass).
                            Replace("ModeValue", searchDetail.mode);

                        var flight = new DOMFlightAvailability();
                        responseString = flight.getAvailability(searchRequestObject);

                        var serializer1 = new XmlSerializer(typeof(Arzoo__response));

                        Arzoo__response ArzooResponse;
                        using (TextReader reader1 = new StringReader(responseString))
                        {
                            ArzooResponse = (Arzoo__response)serializer1.Deserialize(reader1);
                        }

                        if (ArzooResponse == null)
                        {
                            response = Request.CreateResponse<string>(HttpStatusCode.NotFound, "<error>No Content Found</error>");
                        }
                        else if (!string.IsNullOrEmpty(ArzooResponse.Error__tag))
                        {
                            response = Request.CreateResponse<string>(HttpStatusCode.BadRequest, "<error>" + ArzooResponse.Error__tag + "</error>");
                        }
                        else
                        {
                            var selectedFlights = new OriginDestinationOption();
                            //Check in depart flight
                            if (ArzooResponse.Response__Depart != null)
                            {
                                var options = ArzooResponse.Response__Depart.OriginDestinationOptions.OriginDestinationOption.FirstOrDefault(o => o.Id == searchDetail.Id);
                                if (options != null)
                                {
                                    var flightDetail = options.FlightSegments.FlightSegment.FirstOrDefault(f => f.FlightNumber == searchDetail.FlightNumber);
                                    if (flightDetail != null)
                                    {
                                        selectedFlights = options;
                                    }
                                }
                            }

                            //Check in return flight
                            if ((selectedFlights == null || selectedFlights.FlightSegments == null) && ArzooResponse.Response__Return != null)
                            {
                                var options = ArzooResponse.Response__Return.OriginDestinationOptions.OriginDestinationOption.FirstOrDefault(o => o.Id == searchDetail.Id);
                                if (options != null)
                                {
                                    var flightDetail = options.FlightSegments.FlightSegment.FirstOrDefault(f => f.FlightNumber == searchDetail.FlightNumber);
                                    if (flightDetail != null)
                                    {
                                        selectedFlights = options;
                                    }
                                }
                            }

                            if (selectedFlights == null)
                            {
                                response = Request.CreateResponse<string>(HttpStatusCode.NotFound, "<error>No Content Found</error>");
                            }
                            else
                            {
                                var emptyNamepsaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
                                XmlSerializer xsSubmit = new XmlSerializer(typeof(OriginDestinationOption));
                                var xml = "";

                                var settings = new XmlWriterSettings();
                                settings.Indent = true;
                                settings.OmitXmlDeclaration = true;

                                using (var sww = new StringWriter())
                                {
                                    using (XmlWriter writer = XmlWriter.Create(sww, settings))
                                    {
                                        xsSubmit.Serialize(writer, selectedFlights, emptyNamepsaces);
                                        xml = sww.ToString(); // Your XML
                                    }
                                }

                                var flightPricingRequestXml = TemplateXml.FlightPricingXml;
                                flightPricingRequestXml = flightPricingRequestXml.Replace("FlghtDetailXmlTemplate", xml).
                                Replace("AdultCountValue", searchDetail.AdultPax).
                                Replace("ChildCountValue", searchDetail.ChildPax).
                                Replace("InfantCountValue", searchDetail.InfantPax);

                                var flightPricing = new DOMFlightPricing();
                                responseString = flightPricing.getPricingDetails(flightPricingRequestXml);

                                /****Code to extract response object form xml*****/
                                var serializer2 = new XmlSerializer(typeof(Pricingresponse));
                                Pricingresponse pricingResponse;
                                using (TextReader reader1 = new StringReader(responseString))
                                {
                                    pricingResponse = (Pricingresponse)serializer2.Deserialize(reader1);
                                }

                                if (pricingResponse == null)
                                {
                                    response = Request.CreateResponse<string>(HttpStatusCode.NotFound, "<error>No Content Found</error>");
                                }
                                else if (!string.IsNullOrEmpty(pricingResponse.Error))
                                {
                                    response = Request.CreateResponse<string>(HttpStatusCode.BadRequest, "<error>" + pricingResponse.Error + "</error>");
                                }
                                else
                                {
                                    var flightDetail = new origindestinationoption();
                                    flightDetail.FareDetail = pricingResponse.OnwardFlights.OriginDestinationOption.FareDetails;
                                    flightDetail.FlightsDetailList = new List<FlightsDetail>();
                                    foreach (var flightSegment in pricingResponse.OnwardFlights.OriginDestinationOption.FlightSegments.FlightSegment)
                                    {
                                        flightDetail.FlightsDetailList.Add(new FlightsDetail
                                        {
                                            ArrivalAirportCode = flightSegment.ArrivalAirportCode,
                                            ArrivalDateTime = flightSegment.ArrivalDateTime,
                                            Availability = flightSegment.BookingClass.Availability,
                                            Id = pricingResponse.OnwardFlights.OriginDestinationOption.Id,
                                            DepartureAirportCode = flightSegment.DepartureAirportCode,
                                            DepartureDateTime = flightSegment.DepartureDateTime,
                                            FlightNumber = flightSegment.FlightNumber,
                                            ImageFileName = flightSegment.ImageFileName,
                                            OperatingAirlineCode = flightSegment.OperatingAirlineCode,
                                            StopQuantity = flightSegment.StopQuantity,
                                            AirLineName = flightSegment.AirLineName,
                                            BookingClassFare = flightSegment.BookingClassFare
                                        });
                                    }

                                    if (flightDetail != null && flightDetail.FlightsDetailList.Count > 0)
                                    {
                                        XmlSerializer flightResponse = new XmlSerializer(typeof(origindestinationoption));
                                        var flightResponseXml = "";

                                        using (var sww = new StringWriter())
                                        {
                                            using (XmlWriter writer = XmlWriter.Create(sww))
                                            {
                                                flightResponse.Serialize(writer, flightDetail);
                                                flightResponseXml = sww.ToString(); // Your XML
                                            }
                                        }

                                        response = Request.CreateResponse<string>(HttpStatusCode.OK, flightResponseXml);
                                    }
                                    else
                                    {
                                        response = Request.CreateResponse<string>(HttpStatusCode.NotFound, "<error>No Content Found</error>");
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    responseString = e.Message;
                    response = Request.CreateResponse<string>(HttpStatusCode.NotFound, responseString);
                }

                return response;
            }
            return null;
        }

        [ActionName("BookTicket")]
        public async Task<HttpResponseMessage> BookTicket()
        {
            if (hepler.CheckHeader(Request))
            {
                string responseString = string.Empty;
                HttpResponseMessage response = null;
                try
                {
                    var searchParamterXml = await Request.Content.ReadAsByteArrayAsync();
                    string searchParameter = System.Text.Encoding.UTF8.GetString(searchParamterXml);
                    XmlSerializer serializer = new XmlSerializer(typeof(Model.Request));
                    using (TextReader reader = new StringReader(searchParameter))
                    {
                        Model.Request searchDetail = (Model.Request)serializer.Deserialize(reader);

                        var DepartDate = ConvertDateFormat(searchDetail.DepartDate);
                        var ReturnDate = ConvertDateFormat(searchDetail.ReturnDate);

                        var searchRequestObject = TemplateXml.SearchFlightXml;
                        searchRequestObject = searchRequestObject.Replace("OriginValue", searchDetail.Origin).
                            Replace("DestinationValue", searchDetail.Destination).
                            Replace("DepartDateValue", DepartDate).
                            Replace("ReturnDateValue", ReturnDate).
                            Replace("AdultCountValue", searchDetail.AdultPax).
                            Replace("ChildCountValue", searchDetail.ChildPax).
                            Replace("InfantCountValue", searchDetail.InfantPax).
                            Replace("PreferredClassValue", searchDetail.Preferredclass).
                            Replace("ModeValue", searchDetail.mode);

                        var flight = new DOMFlightAvailability();
                        responseString = flight.getAvailability(searchRequestObject);

                        var serializer1 = new XmlSerializer(typeof(Arzoo__response));

                        Arzoo__response ArzooResponse;
                        using (TextReader reader1 = new StringReader(responseString))
                        {
                            ArzooResponse = (Arzoo__response)serializer1.Deserialize(reader1);
                        }

                        if (ArzooResponse == null)
                        {
                            response = Request.CreateResponse<string>(HttpStatusCode.NotFound, "<error>No Content Found</error>");
                        }
                        else if (!string.IsNullOrEmpty(ArzooResponse.Error__tag))
                        {
                            response = Request.CreateResponse<string>(HttpStatusCode.BadRequest, "<error>" + ArzooResponse.Error__tag + "</error>");
                        }
                        else
                        {
                            var selectedFlights = new OriginDestinationOption();
                            //Check in depart flight
                            if (ArzooResponse.Response__Depart != null)
                            {
                                var options = ArzooResponse.Response__Depart.OriginDestinationOptions.OriginDestinationOption.FirstOrDefault(o => o.Id == searchDetail.Id);
                                if (options != null)
                                {
                                    var flightDetail = options.FlightSegments.FlightSegment.FirstOrDefault(f => f.FlightNumber == searchDetail.FlightNumber);
                                    if (flightDetail != null)
                                    {
                                        selectedFlights = options;
                                    }
                                }
                            }

                            //Check in return flight
                            if ((selectedFlights == null || selectedFlights.FlightSegments == null) && ArzooResponse.Response__Return != null)
                            {
                                var options = ArzooResponse.Response__Return.OriginDestinationOptions.OriginDestinationOption.FirstOrDefault(o => o.Id == searchDetail.Id);
                                if (options != null)
                                {
                                    var flightDetail = options.FlightSegments.FlightSegment.FirstOrDefault(f => f.FlightNumber == searchDetail.FlightNumber);
                                    if (flightDetail != null)
                                    {
                                        selectedFlights = options;
                                    }
                                }
                            }

                            if (selectedFlights == null)
                            {
                                response = Request.CreateResponse<string>(HttpStatusCode.NotFound, "<error>No Content Found</error>");
                            }
                            else
                            {
                                var emptyNamepsaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
                                XmlSerializer xsSubmit = new XmlSerializer(typeof(OriginDestinationOption));
                                var xml = "";

                                var settings = new XmlWriterSettings();
                                settings.Indent = true;
                                settings.OmitXmlDeclaration = true;

                                using (var sww = new StringWriter())
                                {
                                    using (XmlWriter writer = XmlWriter.Create(sww, settings))
                                    {
                                        xsSubmit.Serialize(writer, selectedFlights, emptyNamepsaces);
                                        xml = sww.ToString(); // Your XML
                                    }
                                }

                                Random generator = new Random();
                                int partnerRefIdValue = generator.Next(100000, 1000000);

                                if (!string.IsNullOrEmpty(searchDetail.PartnerRefId))
                                {
                                    partnerRefIdValue = Convert.ToInt32(searchDetail.PartnerRefId);
                                }

                                var bookingRequestXml = TemplateXml.BookingRequestXml;
                                bookingRequestXml = bookingRequestXml.Replace("FlghtDetailXmlTemplate", xml).
                                Replace("creditcardnoValue", searchDetail.creditcardno).
                                Replace("phoneNumberValue", searchDetail.phoneNumber).
                                Replace("emailAddressValue", searchDetail.emailAddress).
                                Replace("AdultCountValue", searchDetail.AdultPax).
                                Replace("ChildCountValue", searchDetail.ChildPax).
                                Replace("InfantCountValue", searchDetail.InfantPax).
                                Replace("partnerRefIdValue", partnerRefIdValue.ToString());

                                var endTag = "</personName>";
                                int startIndex = searchParameter.IndexOf("<personName>");
                                int endIndex = searchParameter.IndexOf("</personName>", startIndex) + endTag.Length;

                                var personNameXml = searchParameter.Substring(startIndex, endIndex - startIndex);
                                bookingRequestXml = bookingRequestXml.Replace("CustomerInformationXmlValue", personNameXml);

                                var flightPricing = new DOMFlightBooking();
                                responseString = flightPricing.getBookingDetails(bookingRequestXml);

                                if (string.IsNullOrEmpty(responseString))
                                {
                                    response = Request.CreateResponse<string>(HttpStatusCode.BadRequest, "Invalid Request");
                                }
                                else
                                {
                                    /*******Extract repsonse oject from xml******/
                                    var serializer2 = new XmlSerializer(typeof(BookingResponse));

                                    //responseString = RemoveLineEndings(responseString);

                                    BookingResponse BookingresponseDetail;
                                    using (TextReader reader1 = new StringReader(responseString))
                                    {
                                        BookingresponseDetail = (BookingResponse)serializer2.Deserialize(reader1);
                                    }

                                    if (!string.IsNullOrEmpty(BookingresponseDetail.Error))
                                    {
                                        response = Request.CreateResponse<string>(HttpStatusCode.BadRequest, "<error>" + BookingresponseDetail.Error + "</error>");
                                    }
                                    else
                                    {
                                        XmlSerializer xsBooking = new XmlSerializer(typeof(BookingResponse));
                                        var responseXml = "";

                                        using (var sww = new StringWriter())
                                        {
                                            using (XmlWriter writer = XmlWriter.Create(sww))
                                            {
                                                xsBooking.Serialize(writer, BookingresponseDetail);
                                                responseXml = sww.ToString(); // Your XML
                                            }
                                        }

                                        response = Request.CreateResponse<string>(HttpStatusCode.OK, responseXml);
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    responseString = e.Message;
                    response = Request.CreateResponse<string>(HttpStatusCode.NotFound, responseString);
                }

                return response;
            }

            return null;
        }

        // POST api/values
        [ActionName("BookingStatus")]
        public async Task<HttpResponseMessage> BookingStatus()
        {
            if (hepler.CheckHeader(Request))
            {
                string responseString = string.Empty;
                HttpResponseMessage response = null;
                try
                {
                    var searchParamterXml = await Request.Content.ReadAsByteArrayAsync();
                    string searchParameter = System.Text.Encoding.UTF8.GetString(searchParamterXml);
                    XmlSerializer serializer = new XmlSerializer(typeof(EticketRequest));
                    using (TextReader reader = new StringReader(searchParameter))
                    {
                        EticketRequest ticketRequest = (EticketRequest)serializer.Deserialize(reader);

                        var bookingStatusObject = TemplateXml.BookingStatusXml;
                        bookingStatusObject = bookingStatusObject.Replace("transidValue", ticketRequest.transid).
                            Replace("partnerRefIdValue", ticketRequest.partnerRefId);

                        var bookingStatus = new DOMFlightBookingStatus();
                        responseString = bookingStatus.getBookingStatus(bookingStatusObject);


                        /*******Extract repsonse oject from xml******/
                        var serializer2 = new XmlSerializer(typeof(EticketDetails));

                        EticketDetails BookingresponseDetail;
                        using (TextReader reader1 = new StringReader(responseString))
                        {
                            BookingresponseDetail = (EticketDetails)serializer2.Deserialize(reader1);
                        }

                        XmlSerializer xsBookingStatus = new XmlSerializer(typeof(EticketDetails));
                        var responseXml = "";

                        using (var sww = new StringWriter())
                        {
                            using (XmlWriter writer = XmlWriter.Create(sww))
                            {
                                xsBookingStatus.Serialize(writer, BookingresponseDetail);
                                responseXml = sww.ToString(); // Your XML
                            }
                        }

                        response = Request.CreateResponse<string>(HttpStatusCode.OK, responseXml);
                    }
                }
                catch (Exception e)
                {
                    responseString = e.Message;
                    response = Request.CreateResponse<string>(HttpStatusCode.NotFound, responseString);
                }

                return response;
            }

            return null;
        }

        // POST api/values
        [ActionName("CancelRequest")]
        public async Task<HttpResponseMessage> CancelRequest()
        {
            if (hepler.CheckHeader(Request))
            {
                string responseString = string.Empty;
                HttpResponseMessage response = null;
                try
                {
                    var searchParamterXml = await Request.Content.ReadAsByteArrayAsync();
                    string searchParameter = System.Text.Encoding.UTF8.GetString(searchParamterXml);
                    XmlSerializer serializer = new XmlSerializer(typeof(EticketRequest));
                    using (TextReader reader = new StringReader(searchParameter))
                    {
                        EticketRequest ticketRequest = (EticketRequest)serializer.Deserialize(reader);

                        var bookingSttusObject = TemplateXml.BookingStatusXml;
                        bookingSttusObject = bookingSttusObject.Replace("transidValue", ticketRequest.transid).
                            Replace("partnerRefIdValue", ticketRequest.partnerRefId);

                        var bookindStatus = new DOMFlightBookingStatus();
                        responseString = bookindStatus.getBookingStatus(bookingSttusObject);

                        /********Code for deserialize response*******/

                        if (responseString.IndexOf("<Eticket>") < 0)
                        {
                            response = Request.CreateResponse<string>(HttpStatusCode.OK, "<Error>Booking Is Under Process</Error>");
                        }
                        else
                        {

                            var endTag = "</Eticket>";
                            int startIndex = responseString.IndexOf("<Eticket>");
                            int endIndex = responseString.IndexOf(endTag, startIndex) + endTag.Length;

                            var eticketXml = responseString.Substring(startIndex, endIndex - startIndex);
                            //eticketXml = eticketXml.Replace("EticketDetails", "Eticket");

                            var cancelRequestObject = TemplateXml.CancelRequestXml;
                            cancelRequestObject = cancelRequestObject.Replace("transidValue", ticketRequest.transid).
                                    Replace("EticketXmlValue", eticketXml);

                            var cancelRequest = new DOMFlightCancellation();
                            responseString = cancelRequest.getCancelation(cancelRequestObject);

                            /*******Extract repsonse oject from xml******/
                            var serializer2 = new XmlSerializer(typeof(CancelationDetails));

                            CancelationDetails cancelationDetails;
                            using (TextReader reader1 = new StringReader(responseString))
                            {
                                cancelationDetails = (CancelationDetails)serializer2.Deserialize(reader1);
                            }

                            if (!string.IsNullOrEmpty(cancelationDetails.Error))
                            {
                                response = Request.CreateResponse<string>(HttpStatusCode.BadRequest, "<error>" + cancelationDetails.Error + "</error>");
                            }
                            else
                            {
                                XmlSerializer cancelationRequest = new XmlSerializer(typeof(CancelationDetails));
                                var responseXml = "";

                                using (var sww = new StringWriter())
                                {
                                    using (XmlWriter writer = XmlWriter.Create(sww))
                                    {
                                        cancelationRequest.Serialize(writer, cancelationDetails);
                                        responseXml = sww.ToString(); // Your XML
                                    }
                                }

                                response = Request.CreateResponse<string>(HttpStatusCode.OK, responseXml);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    responseString = e.Message;
                    response = Request.CreateResponse<string>(HttpStatusCode.NotFound, responseString);
                }


                return response;
            }
            return null;
        }

        // POST api/values
        [ActionName("CancelRequestStatus")]
        public async Task<HttpResponseMessage> CancelRequestStatus()
        {
            if (hepler.CheckHeader(Request))
            {
                string responseString = string.Empty;
                HttpResponseMessage response = null;
                try
                {
                    var searchParamterXml = await Request.Content.ReadAsByteArrayAsync();
                    string searchParameter = System.Text.Encoding.UTF8.GetString(searchParamterXml);
                    XmlSerializer serializer = new XmlSerializer(typeof(EticketRequest));
                    using (TextReader reader = new StringReader(searchParameter))
                    {
                        EticketRequest ticketRequest = (EticketRequest)serializer.Deserialize(reader);

                        var bookingStatusObject = TemplateXml.CancelRequestStatusXml;
                        bookingStatusObject = bookingStatusObject.Replace("transidValue", ticketRequest.transid).
                            Replace("partnerRefIdValue", ticketRequest.partnerRefId).
                            Replace("CancellationIdValue", ticketRequest.CancellationId);

                        var cancelStatus = new DOMFlightCancellationStatus();
                        responseString = cancelStatus.getCancelationStatus(bookingStatusObject);

                        /*******Extract repsonse oject from xml******/
                        var serializer2 = new XmlSerializer(typeof(EticketCanStatusRes));

                        EticketCanStatusRes cancelationDetails;
                        using (TextReader reader1 = new StringReader(responseString))
                        {
                            cancelationDetails = (EticketCanStatusRes)serializer2.Deserialize(reader1);
                        }

                        if (!string.IsNullOrEmpty(cancelationDetails.Error))
                        {
                            response = Request.CreateResponse<string>(HttpStatusCode.NotFound, "<error>" + cancelationDetails.Error + "</error>");
                        }
                        else
                        {
                            XmlSerializer cancelationRequest = new XmlSerializer(typeof(EticketCanStatusRes));
                            var responseXml = "";

                            using (var sww = new StringWriter())
                            {
                                using (XmlWriter writer = XmlWriter.Create(sww))
                                {
                                    cancelationRequest.Serialize(writer, cancelationDetails);
                                    responseXml = sww.ToString(); // Your XML
                                }
                            }

                            response = Request.CreateResponse<string>(HttpStatusCode.OK, responseXml);
                        }
                    }
                }
                catch (Exception e)
                {
                    responseString = e.Message;
                    response = Request.CreateResponse<string>(HttpStatusCode.NotFound, responseString);
                }

                return response;
            }

            return null;
        }

        /// <summary>
        /// method to assign city list
        /// </summary>
        public IList<KeyValuePair> AssignDomesticCities()
        {
            IList<KeyValuePair> cityList = new List<KeyValuePair>();
            cityList.Add(new KeyValuePair { Id = "IXA", Value = "AGARTALA" });
            cityList.Add(new KeyValuePair { Id = "AGX", Value = "AGATTI" });
            cityList.Add(new KeyValuePair { Id = "AGR", Value = "AGRA" });
            cityList.Add(new KeyValuePair { Id = "AMD", Value = "AHMEDABAD" });
            cityList.Add(new KeyValuePair { Id = "AJL", Value = "AIJWAL" });
            cityList.Add(new KeyValuePair { Id = "AKD", Value = "AKOLA" });
            cityList.Add(new KeyValuePair { Id = "IXD", Value = "ALLAHABAD" });
            cityList.Add(new KeyValuePair { Id = "IXV", Value = "ALONG" });
            cityList.Add(new KeyValuePair { Id = "ATQ", Value = "AMRITSAR" });
            cityList.Add(new KeyValuePair { Id = "IXU", Value = "AURANGABAD" });
            cityList.Add(new KeyValuePair { Id = "IXB", Value = "BAGDOGRA" });
            cityList.Add(new KeyValuePair { Id = "RGH", Value = "BALURGHAT" });
            cityList.Add(new KeyValuePair { Id = "BLR", Value = "BANGALORE" });
            cityList.Add(new KeyValuePair { Id = "BEK", Value = "BARELI" });
            cityList.Add(new KeyValuePair { Id = "IXG", Value = "BELGAUM" });
            cityList.Add(new KeyValuePair { Id = "BEP", Value = "BELLARY" });
            cityList.Add(new KeyValuePair { Id = "BUP", Value = "BHATINDA" });
            cityList.Add(new KeyValuePair { Id = "BHU", Value = "BHAVNAGAR" });
            cityList.Add(new KeyValuePair { Id = "BHO", Value = "BHOPAL" });
            cityList.Add(new KeyValuePair { Id = "BBI", Value = "BHUBANESHWAR" });
            cityList.Add(new KeyValuePair { Id = "BHJ", Value = "BHUJ" });
            cityList.Add(new KeyValuePair { Id = "BKB", Value = "BIKANER" });
            cityList.Add(new KeyValuePair { Id = "PAB", Value = "BILASPUR" });
            cityList.Add(new KeyValuePair { Id = "CCJ", Value = "CALICUT" });
            cityList.Add(new KeyValuePair { Id = "CBD", Value = "CAR NICOBAR" });
            cityList.Add(new KeyValuePair { Id = "IXC", Value = "CHANDIGARH" });
            cityList.Add(new KeyValuePair { Id = "MAA", Value = "CHENNAI" });
            cityList.Add(new KeyValuePair { Id = "COK", Value = "COCHIN" });
            cityList.Add(new KeyValuePair { Id = "CJB", Value = "COIMBATORE" });
            cityList.Add(new KeyValuePair { Id = "COH", Value = "COOCH BEHAR" });
            cityList.Add(new KeyValuePair { Id = "CDP", Value = "CUDDAPAH" });
            cityList.Add(new KeyValuePair { Id = "NMB", Value = "DAMAN" });
            cityList.Add(new KeyValuePair { Id = "DAE", Value = "DAPARIZO" });
            cityList.Add(new KeyValuePair { Id = "DAI", Value = "DARJEELING" });
            cityList.Add(new KeyValuePair { Id = "DED", Value = "DEHRA DUN" });
            cityList.Add(new KeyValuePair { Id = "DEL", Value = "DEL" });
            cityList.Add(new KeyValuePair { Id = "DEP", Value = "DEPARIZO" });
            cityList.Add(new KeyValuePair { Id = "DBD", Value = "DHANBAD" });

            cityList.Add(new KeyValuePair { Id = "DHM", Value = "DHARAMSALA" });
            cityList.Add(new KeyValuePair { Id = "DIB", Value = "DIBRUGARH" });

            cityList.Add(new KeyValuePair { Id = "DMU", Value = "DIMAPUR" });
            cityList.Add(new KeyValuePair { Id = "DIU", Value = "DIU" });
            cityList.Add(new KeyValuePair { Id = "GAY", Value = "GAYA" });
            cityList.Add(new KeyValuePair { Id = "GOI", Value = "GOA" });
            cityList.Add(new KeyValuePair { Id = "GOP", Value = "GORAKHPUR" });
            cityList.Add(new KeyValuePair { Id = "GUX", Value = "GUNA" });
            cityList.Add(new KeyValuePair { Id = "GAU", Value = "GUWAHATI" });
            cityList.Add(new KeyValuePair { Id = "GWL", Value = "GWALIOR" });
            cityList.Add(new KeyValuePair { Id = "HSS", Value = "HISSAR" });
            cityList.Add(new KeyValuePair { Id = "HBX", Value = "HUBLI" });
            cityList.Add(new KeyValuePair { Id = "HYD", Value = "HYDERABAD" });
            cityList.Add(new KeyValuePair { Id = "IMF", Value = "IMPHAL" });
            cityList.Add(new KeyValuePair { Id = "IDR", Value = "INDORE" });
            cityList.Add(new KeyValuePair { Id = "JLR", Value = "JABALPUR" });
            cityList.Add(new KeyValuePair { Id = "JGB", Value = "JAGDALPUR" });
            cityList.Add(new KeyValuePair { Id = "JAI", Value = "JAIPUR" });
            cityList.Add(new KeyValuePair { Id = "JSA", Value = "JAISALMER" });
            cityList.Add(new KeyValuePair { Id = "IXJ", Value = "JAMMU" });
            cityList.Add(new KeyValuePair { Id = "JGA", Value = "JAMNAGAR" });
            cityList.Add(new KeyValuePair { Id = "IXW", Value = "JAMSHEDPUR" });
            cityList.Add(new KeyValuePair { Id = "PYB", Value = "JEYPORE" });
            cityList.Add(new KeyValuePair { Id = "JDH", Value = "JODHPUR" });
            cityList.Add(new KeyValuePair { Id = "JRH", Value = "JORHAT" });
            cityList.Add(new KeyValuePair { Id = "IXH", Value = "KAILASHAHAR" });
            cityList.Add(new KeyValuePair { Id = "IXQ", Value = "KAMALPUR" });

            cityList.Add(new KeyValuePair { Id = "IXY", Value = "KANDLA" });
            cityList.Add(new KeyValuePair { Id = "KNU", Value = "KANPUR" });
            cityList.Add(new KeyValuePair { Id = "IXK", Value = "KESHOD" });
            cityList.Add(new KeyValuePair { Id = "HJR", Value = "KHAJURAHO" });
            cityList.Add(new KeyValuePair { Id = "IXN", Value = "KHOWAI" });
            cityList.Add(new KeyValuePair { Id = "KLH", Value = "KOLHAPUR" });
            cityList.Add(new KeyValuePair { Id = "CCU", Value = "KOLKATA" });
            cityList.Add(new KeyValuePair { Id = "KTU", Value = "KOTA" });
            cityList.Add(new KeyValuePair { Id = "KUU", Value = "KULU" });
            cityList.Add(new KeyValuePair { Id = "LTU", Value = "LATUR" });
            cityList.Add(new KeyValuePair { Id = "IXL", Value = "LEH" });
            cityList.Add(new KeyValuePair { Id = "IXI", Value = "LILABARI" });
            cityList.Add(new KeyValuePair { Id = "LKO", Value = "LUCKNOW" });
            cityList.Add(new KeyValuePair { Id = "LUH", Value = "LUDHIANA" });
            cityList.Add(new KeyValuePair { Id = "IXM", Value = "MADURAI" });
            cityList.Add(new KeyValuePair { Id = "LDA", Value = "MALDA" });
            cityList.Add(new KeyValuePair { Id = "IXE", Value = "MANGALORE" });
            cityList.Add(new KeyValuePair { Id = "MOH", Value = "MOHANBARI" });
            cityList.Add(new KeyValuePair { Id = "BOM", Value = "MUMBAI" });
            cityList.Add(new KeyValuePair { Id = "MZA", Value = "MUZAFFARNAGAR" });
            cityList.Add(new KeyValuePair { Id = "MZU", Value = "MUZAFFARPUR" });
            cityList.Add(new KeyValuePair { Id = "MYQ", Value = "MYSORE" });
            cityList.Add(new KeyValuePair { Id = "NAG", Value = "NAGPUR" });
            cityList.Add(new KeyValuePair { Id = "NDC", Value = "NANDED" });
            cityList.Add(new KeyValuePair { Id = "ISK", Value = "NASIK" });
            cityList.Add(new KeyValuePair { Id = "NVY", Value = "NEYVELI" });
            cityList.Add(new KeyValuePair { Id = "OMN", Value = "OSMANABAD" });
            cityList.Add(new KeyValuePair { Id = "PGH", Value = "PANTNAGAR" });
            cityList.Add(new KeyValuePair { Id = "IXT", Value = "PASIGHAT" });
            cityList.Add(new KeyValuePair { Id = "IXP", Value = "PATHANKOT" });
            cityList.Add(new KeyValuePair { Id = "PAT", Value = "PATNA" });
            cityList.Add(new KeyValuePair { Id = "PNY", Value = "PONDICHERRY" });
            cityList.Add(new KeyValuePair { Id = "PBD", Value = "PORBANDAR" });
            cityList.Add(new KeyValuePair { Id = "IXZ", Value = "PORTBLAIR" });
            cityList.Add(new KeyValuePair { Id = "PNQ", Value = "PUNE" });
            cityList.Add(new KeyValuePair { Id = "PUT", Value = "PUTTAPARTHI" });
            cityList.Add(new KeyValuePair { Id = "RPR", Value = "RAIPUR" });
            cityList.Add(new KeyValuePair { Id = "RJA", Value = "RAJAHMUNDRY" });
            cityList.Add(new KeyValuePair { Id = "RAJ", Value = "RAJKOT" });
            cityList.Add(new KeyValuePair { Id = "RJI", Value = "RAJOURI" });
            cityList.Add(new KeyValuePair { Id = "RMD", Value = "RAMAGUNDAM" });
            cityList.Add(new KeyValuePair { Id = "IXR", Value = "RANCHI" });
            cityList.Add(new KeyValuePair { Id = "RTC", Value = "RATNAGIRI" });
            cityList.Add(new KeyValuePair { Id = "REW", Value = "REWA" });
            cityList.Add(new KeyValuePair { Id = "RRK", Value = "ROURKELA" });
            cityList.Add(new KeyValuePair { Id = "RUP", Value = "RUPSI" });
            cityList.Add(new KeyValuePair { Id = "SXV", Value = "SALEM" });
            cityList.Add(new KeyValuePair { Id = "TNI", Value = "SATNA" });
            cityList.Add(new KeyValuePair { Id = "SHL", Value = "SHILLONG" });
            cityList.Add(new KeyValuePair { Id = "SSE", Value = "SHOLAPUR" });
            cityList.Add(new KeyValuePair { Id = "IXS", Value = "SILCHAR" });
            cityList.Add(new KeyValuePair { Id = "SLV", Value = "SIMLA" });
            cityList.Add(new KeyValuePair { Id = "SXR", Value = "SRINAGAR" });
            cityList.Add(new KeyValuePair { Id = "STV", Value = "SURAT" });
            cityList.Add(new KeyValuePair { Id = "TEZ", Value = "TEZPUR" });
            cityList.Add(new KeyValuePair { Id = "TEI", Value = "TEZU" });
            cityList.Add(new KeyValuePair { Id = "TJV", Value = "THANJAVUR" });
            cityList.Add(new KeyValuePair { Id = "TRV", Value = "TRIVANDRUM" });
            cityList.Add(new KeyValuePair { Id = "TRZ", Value = "TIRUCHIRAPALLI" });
            cityList.Add(new KeyValuePair { Id = "ICH", Value = "TRICHI" });
            cityList.Add(new KeyValuePair { Id = "TIR", Value = "TIRUPATI" });
            cityList.Add(new KeyValuePair { Id = "TCR", Value = "TUTICORIN" });
            cityList.Add(new KeyValuePair { Id = "UDR", Value = "UDAIPUR" });

            cityList.Add(new KeyValuePair { Id = "BDQ", Value = "VADODRA" });
            cityList.Add(new KeyValuePair { Id = "VNS", Value = "VARANASI" });
            cityList.Add(new KeyValuePair { Id = "VGA", Value = "VIJAYAWADA" });
            cityList.Add(new KeyValuePair { Id = "VTZ", Value = "VIZAG" });
            cityList.Add(new KeyValuePair { Id = "WGC", Value = "WARANGAL" });

            return cityList;
        }

        public string RemoveLineEndings(string value)
        {
            if (String.IsNullOrEmpty(value))
            {
                return value;
            }
            string lineSeparator = ((char)0x2028).ToString();
            string paragraphSeparator = ((char)0x2029).ToString();

            return value.Replace("\r\n", string.Empty).Replace("\n", string.Empty).Replace("\r", string.Empty).Replace(lineSeparator, string.Empty).Replace(paragraphSeparator, string.Empty);
        }

        public string ConvertDateFormat(string dateTime)
        {
            if (string.IsNullOrEmpty(dateTime))
            {
                return null;
            }

            DateTime dt = DateTime.ParseExact(dateTime, "dd-MMM-yyyy", CultureInfo.InvariantCulture);
            var newDate = dt.ToString("yyyy-MM-dd");
            return newDate;
        }
    }
}