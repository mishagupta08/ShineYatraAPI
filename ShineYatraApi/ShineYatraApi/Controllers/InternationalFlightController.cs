namespace ShineYatraApi.Controllers
{
    #region namespace

    using Models;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Linq;
    using ShineYatraApi.Models.Flight;

    #endregion namespace

    public class InternationalFlightController : ApiController
    {
        HttpHelper hepler = new HttpHelper();

        /// <summary>
        /// Holds xml Templates 
        /// </summary>
        InternationalFlightTemplate TemplateXml = new InternationalFlightTemplate();

        // POST api/values
        [ActionName("SearchInternationalFlight")]
        public async Task<HttpResponseMessage> SearchInternationalFlight()
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

                        var searchRequestObject = TemplateXml.SearchFlightIntXml;
                        searchRequestObject = searchRequestObject.Replace("OriginValue", searchDetail.Origin).
                            Replace("DestinationValue", searchDetail.Destination).
                            Replace("DepartDateValue", searchDetail.DepartDate).
                            Replace("ReturnDateValue", searchDetail.ReturnDate).
                            Replace("AdultCountValue", searchDetail.AdultPax).
                            Replace("ChildCountValue", searchDetail.ChildPax).
                            Replace("InfantCountValue", searchDetail.InfantPax).
                            Replace("PreferredClassValue", searchDetail.Preferredclass).
                            Replace("ModeValue", searchDetail.Mode);

                        //Make Request for api

                        searchRequestObject = "<AvailRequest><Trip>ONE</Trip><Origin>BOM</Origin><Destination>JFK</Destination><DepartDate>2017-07-21</DepartDate><ReturnDate>2017-07-21</ReturnDate><AdultPax>1</AdultPax><ChildPax>0</ChildPax><InfantPax>0</InfantPax><Currency>INR</Currency><PreferredClass>E</PreferredClass><Eticket>true</Eticket><Clientid>77743717</Clientid><Clientpassword>*210E136166E8DB5B3FFC32FA4223B7B4585830EA</Clientpassword><Clienttype>ArzooINTLWS1.0</Clienttype><PreferredAirline></PreferredAirline></AvailRequest>";

                        using (var httpClient = new HttpClient())
                        {
                            var httpContent = new StringContent(searchRequestObject, Encoding.UTF8, "text/xml");

                            // Do the actual request and await the response
                            var httpResponse = await httpClient.PostAsync("http://demo.arzoo.com:9301/Avalability", httpContent);

                            // If the response contains content we want to read it!
                            if (httpResponse.Content != null)
                            {
                                responseString = await httpResponse.Content.ReadAsStringAsync();
                            }
                        }

                        response = Request.CreateResponse<string>(HttpStatusCode.OK, responseString);

                        //var serializer1 = new XmlSerializer(typeof(Arzoo_responseInt));

                        //Arzoo_responseInt ArzooResponse;
                        //using (TextReader reader1 = new StringReader(responseString))
                        //{
                        //    ArzooResponse = (Arzoo_responseInt)serializer1.Deserialize(reader1);
                        //}

                        //if (ArzooResponse == null)
                        //{
                        //    response = Request.CreateResponse<string>(HttpStatusCode.NoContent, "No Content Found");
                        //}
                        //else if (!string.IsNullOrEmpty(ArzooResponse.Error))
                        //{
                        //    response = Request.CreateResponse<string>(HttpStatusCode.BadRequest, ArzooResponse.Error);
                        //}
                        //else
                        //{
                        //    var flightList = new List<FlightsDetail>();
                        //    foreach (var option in ArzooResponse.AvailResponse.OriginDestinationOptions.OriginDestinationOption)
                        //    {
                        //        foreach (var flightSegment in option.Onward.FlightSegments.FlightSegment)
                        //        {
                        //            flightList.Add(new FlightsDetail
                        //            {
                        //                ArrivalAirportCode = flightSegment.ArrivalAirportCode,
                        //                ArrivalDateTime = flightSegment.ArrivalDateTime,
                        //                Availability = flightSegment.BookingClass.Availability,
                        //                Id = option.Id,
                        //                DepartureAirportCode = flightSegment.DepartureAirportCode,
                        //                DepartureDateTime = flightSegment.DepartureDateTime,
                        //                FlightNumber = flightSegment.FlightNumber,
                        //                BookingClassFareInt = flightSegment.BookingClassFare,
                        //                OperatingAirlineCode = flightSegment.OperatingAirlineCode,
                        //                StopQuantity = flightSegment.NumStops,
                        //                AirLineName = flightSegment.OperatingAirlineName
                        //            });
                        //        }

                        //        if (option.Return != null && option.Return.FlightSegments != null)
                        //        {
                        //            foreach (var flightSegment in option.Return.FlightSegments.FlightSegment)
                        //            {
                        //                flightList.Add(new FlightsDetail
                        //                {
                        //                    ArrivalAirportCode = flightSegment.ArrivalAirportCode,
                        //                    ArrivalDateTime = flightSegment.ArrivalDateTime,
                        //                    Availability = flightSegment.BookingClass.Availability,
                        //                    Id = option.Id,
                        //                    DepartureAirportCode = flightSegment.DepartureAirportCode,
                        //                    DepartureDateTime = flightSegment.DepartureDateTime,
                        //                    FlightNumber = flightSegment.FlightNumber,
                        //                    BookingClassFareInt = flightSegment.BookingClassFare,
                        //                    OperatingAirlineCode = flightSegment.OperatingAirlineCode,
                        //                    StopQuantity = flightSegment.NumStops,
                        //                    AirLineName = flightSegment.OperatingAirlineName,
                        //                    IsReturnFlight = true
                        //                });
                        //            }
                        //        }
                        //    }
                        //    //if (ArzooResponse.AvailResponse.OriginDestinationOptions.OriginDestinationOption != null)
                        //    //{
                        //    //    foreach (var option in ArzooResponse.Response__Return.OriginDestinationOptions.OriginDestinationOption)
                        //    //    {
                        //    //        foreach (var flightSegment in option.FlightSegments.FlightSegment)
                        //    //        {
                        //    //            flightList.Add(new FlightsDetail
                        //    //            {
                        //    //                ArrivalAirportCode = flightSegment.ArrivalAirportCode,
                        //    //                ArrivalDateTime = flightSegment.ArrivalDateTime,
                        //    //                Availability = flightSegment.BookingClass.Availability,
                        //    //                Id = option.Id,
                        //    //                DepartureAirportCode = flightSegment.DepartureAirportCode,
                        //    //                DepartureDateTime = flightSegment.DepartureDateTime,
                        //    //                FlightNumber = flightSegment.FlightNumber,
                        //    //                ImageFileName = flightSegment.ImageFileName,
                        //    //                OperatingAirlineCode = flightSegment.OperatingAirlineCode,
                        //    //                StopQuantity = flightSegment.StopQuantity,
                        //    //                AirLineName = flightSegment.AirLineName,
                        //    //                BookingClassFare = flightSegment.BookingClassFare,
                        //    //                IsReturnFlight = true
                        //    //            });
                        //    //        }
                        //    //    }
                        //    //}
                        //    if (flightList != null && flightList.Count > 0)
                        //    {
                        //        XmlSerializer xsSubmit = new XmlSerializer(typeof(List<FlightsDetail>));
                        //        var xml = "";

                        //        using (var sww = new StringWriter())
                        //        {
                        //            using (XmlWriter writer = XmlWriter.Create(sww))
                        //            {
                        //                xsSubmit.Serialize(writer, flightList);
                        //                xml = sww.ToString(); // Your XML
                        //            }
                        //        }

                        //        response = Request.CreateResponse<string>(HttpStatusCode.OK, xml);
                        //    }
                        //    else
                        //    {
                        //        response = Request.CreateResponse<string>(HttpStatusCode.NotFound, "No Content Found");
                        //    }
                        //  }
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

        [ActionName("FlightPricingInternational")]
        public async Task<HttpResponseMessage> FlightPricingInternational()
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

                        var searchRequestObject = TemplateXml.FlightPricingIntXml;
                        searchRequestObject = searchRequestObject.Replace("OriginValue", searchDetail.Origin).
                            Replace("DestinationValue", searchDetail.Destination).
                            Replace("DepartDateValue", searchDetail.DepartDate).
                            Replace("ReturnDateValue", searchDetail.ReturnDate).
                            Replace("AdultCountValue", searchDetail.AdultPax).
                            Replace("ChildCountValue", searchDetail.ChildPax).
                            Replace("InfantCountValue", searchDetail.InfantPax).
                            Replace("PreferredClassValue", searchDetail.Preferredclass).
                            Replace("ModeValue", searchDetail.mode);

                        //// Code to get all flight*** Start
                        using (var httpClient = new HttpClient())
                        {
                            var httpContent = new StringContent(searchRequestObject, Encoding.UTF8, "text/xml");

                            // Do the actual request and await the response
                            var httpResponse = await httpClient.PostAsync("http://demo.arzoo.com:9301/Avalability", httpContent);

                            // If the response contains content we want to read it!
                            if (httpResponse.Content != null)
                            {
                                responseString = await httpResponse.Content.ReadAsStringAsync();
                            }
                        }

                        //*********End

                        var serializer1 = new XmlSerializer(typeof(Arzoo_responseInt));

                        Arzoo_responseInt ArzooResponse;
                        using (TextReader reader1 = new StringReader(responseString))
                        {
                            ArzooResponse = (Arzoo_responseInt)serializer1.Deserialize(reader1);
                        }

                        if (ArzooResponse == null)
                        {
                            response = Request.CreateResponse<string>(HttpStatusCode.NoContent, "No Content Found");
                        }
                        else if (!string.IsNullOrEmpty(ArzooResponse.Error))
                        {
                            response = Request.CreateResponse<string>(HttpStatusCode.BadRequest, ArzooResponse.Error);
                        }
                        else
                        {
                            var selectedFlights = new OriginDestinationOptionInt();
                            //Check in depart flight
                            if (ArzooResponse.AvailResponse != null)
                            {
                                var options = ArzooResponse.AvailResponse.OriginDestinationOptions.OriginDestinationOption.FirstOrDefault(o => o.Id == searchDetail.Id);
                                if (options != null)
                                {
                                    selectedFlights = options;
                                }
                            }

                            if (selectedFlights == null)
                            {
                                response = Request.CreateResponse<string>(HttpStatusCode.NoContent, "No Content Found");
                            }
                            else
                            {
                                var emptyNamepsaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
                                XmlSerializer xsSubmit = new XmlSerializer(typeof(OriginDestinationOptionInt));
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

                                var flightPricingRequestXml = TemplateXml.FlightPricingIntXml;
                                flightPricingRequestXml = flightPricingRequestXml.Replace("FlghtDetailXmlTemplate", xml).
                                Replace("AdultCountValue", searchDetail.AdultPax).
                                Replace("ChildCountValue", searchDetail.ChildPax).
                                Replace("InfantCountValue", searchDetail.InfantPax);

                                /****Code to Call pricing Api*****/
                                using (var httpClient = new HttpClient())
                                {
                                    var httpContent = new StringContent(searchRequestObject, Encoding.UTF8, "text/xml");

                                    // Do the actual request and await the response
                                    var httpResponse = await httpClient.PostAsync("http://demo.arzoo.com:9301/Pricing", httpContent);

                                    // If the response contains content we want to read it!
                                    if (httpResponse.Content != null)
                                    {
                                        responseString = await httpResponse.Content.ReadAsStringAsync();
                                    }
                                }

                                /****Code to extract response object form xml*****/
                                var serializer2 = new XmlSerializer(typeof(PriceResponseInt));
                                PriceResponseInt pricingResponse;
                                using (TextReader reader1 = new StringReader(responseString))
                                {
                                    pricingResponse = (PriceResponseInt)serializer2.Deserialize(reader1);
                                }

                                if (pricingResponse == null)
                                {
                                    response = Request.CreateResponse<string>(HttpStatusCode.NoContent, "No Content Found");
                                }
                                else if (!string.IsNullOrEmpty(pricingResponse.Error))
                                {
                                    response = Request.CreateResponse<string>(HttpStatusCode.BadRequest, pricingResponse.Error);
                                }
                                else
                                {
                                    var flightList = new List<FlightsDetail>();
                                    foreach (var flightSegment in pricingResponse.OriginDestinationOption.Onward.FlightSegments.FlightSegment)
                                    {
                                        flightList.Add(new FlightsDetail
                                        {
                                            ArrivalAirportCode = flightSegment.ArrivalAirportCode,
                                            ArrivalDateTime = flightSegment.ArrivalDateTime,
                                            Availability = flightSegment.BookingClass.Availability,
                                            Id = pricingResponse.OriginDestinationOption.Id,
                                            DepartureAirportCode = flightSegment.DepartureAirportCode,
                                            DepartureDateTime = flightSegment.DepartureDateTime,
                                            FlightNumber = flightSegment.FlightNumber,
                                            BookingClassFareInt = flightSegment.BookingClassFare,
                                            OperatingAirlineCode = flightSegment.OperatingAirlineCode,
                                            StopQuantity = flightSegment.NumStops,
                                            AirLineName = flightSegment.OperatingAirlineName
                                        });
                                    }

                                    if (pricingResponse.OriginDestinationOption.Return != null && pricingResponse.OriginDestinationOption.Return.FlightSegments != null)
                                    {
                                        foreach (var flightSegment in pricingResponse.OriginDestinationOption.Return.FlightSegments.FlightSegment)
                                        {
                                            flightList.Add(new FlightsDetail
                                            {
                                                ArrivalAirportCode = flightSegment.ArrivalAirportCode,
                                                ArrivalDateTime = flightSegment.ArrivalDateTime,
                                                Availability = flightSegment.BookingClass.Availability,
                                                Id = pricingResponse.OriginDestinationOption.Id,
                                                DepartureAirportCode = flightSegment.DepartureAirportCode,
                                                DepartureDateTime = flightSegment.DepartureDateTime,
                                                FlightNumber = flightSegment.FlightNumber,
                                                BookingClassFareInt = flightSegment.BookingClassFare,
                                                OperatingAirlineCode = flightSegment.OperatingAirlineCode,
                                                StopQuantity = flightSegment.NumStops,
                                                AirLineName = flightSegment.OperatingAirlineName,
                                                IsReturnFlight = true
                                            });
                                        }
                                    }

                                    if (flightList != null && flightList.Count > 0)
                                    {
                                        XmlSerializer flightResponse = new XmlSerializer(typeof(List<FlightsDetail>));
                                        var flightResponseXml = "";

                                        using (var sww = new StringWriter())
                                        {
                                            using (XmlWriter writer = XmlWriter.Create(sww))
                                            {
                                                flightResponse.Serialize(writer, flightList);
                                                flightResponseXml = sww.ToString(); // Your XML
                                            }
                                        }

                                        response = Request.CreateResponse<string>(HttpStatusCode.OK, flightResponseXml);
                                    }
                                    else
                                    {
                                        response = Request.CreateResponse<string>(HttpStatusCode.NotFound, "No Content Found");
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

        [ActionName("BookTicketInternational")]
        public async Task<HttpResponseMessage> BookTicketInternational()
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

                        var searchRequestObject = TemplateXml.BookingStatusIntXml;
                        searchRequestObject = searchRequestObject.Replace("OriginValue", searchDetail.Origin).
                            Replace("DestinationValue", searchDetail.Destination).
                            Replace("DepartDateValue", searchDetail.DepartDate).
                            Replace("ReturnDateValue", searchDetail.ReturnDate).
                            Replace("AdultCountValue", searchDetail.AdultPax).
                            Replace("ChildCountValue", searchDetail.ChildPax).
                            Replace("InfantCountValue", searchDetail.InfantPax).
                            Replace("PreferredClassValue", searchDetail.Preferredclass).
                            Replace("ModeValue", searchDetail.mode);

                        //// Code to get all flight*** Start
                        using (var httpClient = new HttpClient())
                        {
                            var httpContent = new StringContent(searchRequestObject, Encoding.UTF8, "text/xml");

                            // Do the actual request and await the response
                            var httpResponse = await httpClient.PostAsync("http://demo.arzoo.com:9301/Avalability", httpContent);

                            // If the response contains content we want to read it!
                            if (httpResponse.Content != null)
                            {
                                responseString = await httpResponse.Content.ReadAsStringAsync();
                            }
                        }

                        //*********End

                        var serializer1 = new XmlSerializer(typeof(Arzoo_responseInt));

                        Arzoo_responseInt ArzooResponse;
                        using (TextReader reader1 = new StringReader(responseString))
                        {
                            ArzooResponse = (Arzoo_responseInt)serializer1.Deserialize(reader1);
                        }

                        if (ArzooResponse == null)
                        {
                            response = Request.CreateResponse<string>(HttpStatusCode.NoContent, "No Content Found");
                        }
                        else if (!string.IsNullOrEmpty(ArzooResponse.Error))
                        {
                            response = Request.CreateResponse<string>(HttpStatusCode.BadRequest, ArzooResponse.Error);
                        }
                        else
                        {
                            var selectedFlights = new OriginDestinationOptionInt();
                            //Check in depart flight
                            if (ArzooResponse.AvailResponse != null)
                            {
                                var options = ArzooResponse.AvailResponse.OriginDestinationOptions.OriginDestinationOption.FirstOrDefault(o => o.Id == searchDetail.Id);
                                if (options != null)
                                {
                                    selectedFlights = options;
                                }
                            }

                            if (selectedFlights == null)
                            {
                                response = Request.CreateResponse<string>(HttpStatusCode.NoContent, "No Content Found");
                            }
                            else
                            {
                                var emptyNamepsaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
                                XmlSerializer xsSubmit = new XmlSerializer(typeof(OriginDestinationOptionInt));
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

                                /*****Call to book Flight* Start****/

                                //// Code to get all flight*** Start
                                using (var httpClient = new HttpClient())
                                {
                                    var httpContent = new StringContent(bookingRequestXml, Encoding.UTF8, "text/xml");

                                    // Do the actual request and await the response
                                    var httpResponse = await httpClient.PostAsync("http://demo.arzoo.com:9301/Booking", httpContent);

                                    // If the response contains content we want to read it!
                                    if (httpResponse.Content != null)
                                    {
                                        responseString = await httpResponse.Content.ReadAsStringAsync();
                                    }
                                }

                                /*****Call to book Flight* End****/

                                if (string.IsNullOrEmpty(responseString))
                                {
                                    response = Request.CreateResponse<string>(HttpStatusCode.BadRequest, "Invalid Request");
                                }
                                else
                                {
                                    /*******Extract repsonse oject from xml******/
                                    var serializer2 = new XmlSerializer(typeof(Bookingresponse));

                                    BookingResponse BookingresponseDetail;
                                    using (TextReader reader1 = new StringReader(responseString))
                                    {
                                        BookingresponseDetail = (BookingResponse)serializer2.Deserialize(reader1);
                                    }

                                    if (!string.IsNullOrEmpty(BookingresponseDetail.Error))
                                    {
                                        response = Request.CreateResponse<string>(HttpStatusCode.BadRequest, BookingresponseDetail.Error);
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
        [ActionName("BookingStatusInternational")]
        public async Task<HttpResponseMessage> BookingStatusInternational()
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

                        var bookingStatusObject = TemplateXml.BookingStatusIntXml;
                        bookingStatusObject = bookingStatusObject.Replace("transidValue", ticketRequest.transid);

                        //// Code to get Book flight status*** Start
                        using (var httpClient = new HttpClient())
                        {
                            var httpContent = new StringContent(bookingStatusObject, Encoding.UTF8, "text/xml");

                            // Do the actual request and await the response
                            var httpResponse = await httpClient.PostAsync("http://demo.arzoo.com:9301/BookingStatus", httpContent);

                            // If the response contains content we want to read it!
                            if (httpResponse.Content != null)
                            {
                                responseString = await httpResponse.Content.ReadAsStringAsync();
                            }
                        }

                        /*****Call to book Flight status* End****/


                        /*******Extract repsonse oject from xml******/
                        var serializer2 = new XmlSerializer(typeof(EticketDetailsInternational));

                        EticketDetailsInternational BookingresponseDetail;
                        using (TextReader reader1 = new StringReader(responseString))
                        {
                            BookingresponseDetail = (EticketDetailsInternational)serializer2.Deserialize(reader1);
                        }

                        XmlSerializer xsBookingStatus = new XmlSerializer(typeof(EticketDetailsInternational));
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
        [ActionName("CancelRequestInternational")]
        public async Task<HttpResponseMessage> CancelRequestInternational()
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

                        var bookingStatusObject = TemplateXml.BookingStatusIntXml;
                        bookingStatusObject = bookingStatusObject.Replace("transidValue", ticketRequest.transid);

                        //// Code to get Book flight status*** Start
                        using (var httpClient = new HttpClient())
                        {
                            var httpContent = new StringContent(bookingStatusObject, Encoding.UTF8, "text/xml");

                            // Do the actual request and await the response
                            var httpResponse = await httpClient.PostAsync("http://demo.arzoo.com:9301/BookingStatus", httpContent);

                            // If the response contains content we want to read it!
                            if (httpResponse.Content != null)
                            {
                                responseString = await httpResponse.Content.ReadAsStringAsync();
                            }
                        }

                        /********Code for deserialize response*******/

                        var endTag = "</Eticket>";
                        int startIndex = responseString.IndexOf("<Eticket>");
                        int endIndex = responseString.IndexOf(endTag, startIndex) + endTag.Length;

                        var eticketXml = responseString.Substring(startIndex, endIndex - startIndex);

                        var cancelRequestObject = TemplateXml.CancelRequestXml;
                        cancelRequestObject = cancelRequestObject.Replace("transidValue", ticketRequest.transid).
                                Replace("EticketXmlValue", eticketXml);

                        //// Code to get Cancel flight *** Start
                        using (var httpClient = new HttpClient())
                        {
                            var httpContent = new StringContent(bookingStatusObject, Encoding.UTF8, "text/xml");

                            // Do the actual request and await the response
                            var httpResponse = await httpClient.PostAsync("http://demo.arzoo.com:9301/Cancellation", httpContent);

                            // If the response contains content we want to read it!
                            if (httpResponse.Content != null)
                            {
                                responseString = await httpResponse.Content.ReadAsStringAsync();
                            }
                        }

                        /*******Extract repsonse oject from xml******/
                        var serializer2 = new XmlSerializer(typeof(CanIntResponse));

                        CanIntResponse cancelationDetails;
                        using (TextReader reader1 = new StringReader(responseString))
                        {
                            cancelationDetails = (CanIntResponse)serializer2.Deserialize(reader1);
                        }

                        if (!string.IsNullOrEmpty(cancelationDetails.Error))
                        {
                            response = Request.CreateResponse<string>(HttpStatusCode.BadRequest, cancelationDetails.Error);
                        }
                        else
                        {
                            XmlSerializer cancelationRequest = new XmlSerializer(typeof(CanIntResponse));
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

        // POST api/values
        [ActionName("CancelRequestStatusInternational")]
        public async Task<HttpResponseMessage> CancelRequestStatusInternational()
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

                        var bookingStatusObject = TemplateXml.CancelRequestStatusIntXml;
                        bookingStatusObject = bookingStatusObject.Replace("transidValue", ticketRequest.transid).
                            Replace("partnerRefIdValue", ticketRequest.partnerRefId).
                            Replace("CancellationIdValue", ticketRequest.CancellationId);

                        //// Code to get Cancel flight status*** Start
                        using (var httpClient = new HttpClient())
                        {
                            var httpContent = new StringContent(bookingStatusObject, Encoding.UTF8, "text/xml");

                            // Do the actual request and await the response
                            var httpResponse = await httpClient.PostAsync("http://demo.arzoo.com:9301/BookingStatus", httpContent);

                            // If the response contains content we want to read it!
                            if (httpResponse.Content != null)
                            {
                                responseString = await httpResponse.Content.ReadAsStringAsync();
                            }
                        }

                        /*******Extract repsonse oject from xml******/
                        var serializer2 = new XmlSerializer(typeof(CanStatusIntResponse));

                        CanStatusIntResponse cancelationDetails;
                        using (TextReader reader1 = new StringReader(responseString))
                        {
                            cancelationDetails = (CanStatusIntResponse)serializer2.Deserialize(reader1);
                        }

                        if (!string.IsNullOrEmpty(cancelationDetails.Error))
                        {
                            response = Request.CreateResponse<string>(HttpStatusCode.NotFound, cancelationDetails.Error);
                        }
                        else
                        {
                            XmlSerializer cancelationRequest = new XmlSerializer(typeof(CanStatusIntResponse));
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
    }
}