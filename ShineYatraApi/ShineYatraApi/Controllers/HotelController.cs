namespace ShineYatraApi.Controllers
{
    #region namespace

    using System;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Xml;
    using System.Xml.Serialization;
    using HotelBookingConfirmation;
    using HotelCancellationService;
    using HotelDescription;
    using HotelPolicyApi;
    using HotelProvisionalBooking;
    using HotelSearch;
    using Models;
    using Models.RequestModel;
    using System.Xml.Linq;
    using System.Linq;
    using System.Collections.Generic;

    #endregion namespace

    /// <summary>
    /// Contain hotel api functions
    /// </summary>
    public class HotelController : ApiController
    {
        HttpHelper hepler = new HttpHelper();

        /// <summary>
        /// Holds xml Templates 
        /// </summary>
        HotelTemplate HolelTemplateXml = new HotelTemplate();

        /// <summary>
        /// method for search hotel
        /// </summary>
        /// <returns></returns>
        [ActionName("SearchHotel")]
        public async Task<HttpResponseMessage> SearchHotel()
        {
            if (hepler.CheckHeader(Request))
            {
                string responseString = string.Empty;
                HttpResponseMessage response = null;
                try
                {
                    var searchParamterXml = await Request.Content.ReadAsByteArrayAsync();
                    string searchParameter = System.Text.Encoding.UTF8.GetString(searchParamterXml);
                    XmlSerializer serializer = new XmlSerializer(typeof(HotelRequest));
                    using (TextReader reader = new StringReader(searchParameter))
                    {
                        HotelRequest searchDetail = (HotelRequest)serializer.Deserialize(reader);

                        var endTag = "</guestDetails>";
                        int startIndex = searchParameter.IndexOf("<guestDetails>");
                        int endIndex = searchParameter.IndexOf(endTag, startIndex) + endTag.Length;

                        var guestDetailXml = searchParameter.Substring(startIndex, endIndex - startIndex);

                        var searchRequestObject = HolelTemplateXml.SearchHotel;
                        searchRequestObject = searchRequestObject.Replace("startDateValue", searchDetail.Start).
                            Replace("endDateValue", searchDetail.End).
                            Replace("guestDetailsXmlValue", guestDetailXml).
                            Replace("hotelCityNameValue", searchDetail.HotelCityName).
                            Replace("hotelNameValue", searchDetail.HotelName).
                            Replace("areaValue", searchDetail.Area).
                            Replace("attractionValue", searchDetail.Attraction).
                            Replace("ratingValue", searchDetail.Rating).
                            Replace("sortingPreferenceValue", searchDetail.SortingPreference).
                            Replace("hotelPackageValue", searchDetail.HotelPackage);

                        var hotelSearch = new HotelAvailSearch();
                        hotelSearch.Url = "http://demo.arzoo.com/HotelXML_V1.2/services/HotelAvailSearch";
                        responseString = hotelSearch.getHotelAvailSearch(searchRequestObject, string.Empty);
                        if (string.IsNullOrEmpty(responseString))
                        {
                            response = Request.CreateResponse<string>(HttpStatusCode.NotFound, "<error>No Content Found</error>");
                        }
                        if (!responseString.Contains("<"))
                        {
                            response = Request.CreateResponse<string>(HttpStatusCode.NotFound, responseString);
                        }
                        else
                        {
                            var serializer1 = new XmlSerializer(typeof(Models.HotelDetail.ArzHotelAvailResp));

                            Models.HotelDetail.ArzHotelAvailResp hotelAvailResp;
                            using (TextReader reader1 = new StringReader(responseString))
                            {
                                hotelAvailResp = (Models.HotelDetail.ArzHotelAvailResp)serializer1.Deserialize(reader1);
                            }

                            if (hotelAvailResp == null)
                            {
                                response = Request.CreateResponse<string>(HttpStatusCode.NotFound, "<error>No Content Found</error>");
                            }
                            else if (!string.IsNullOrEmpty(hotelAvailResp.Error))
                            {
                                response = Request.CreateResponse<string>(HttpStatusCode.BadRequest, "<Error>" + hotelAvailResp.Error + "</Error>");
                            }
                            else
                            {
                                response = Request.CreateResponse<string>(HttpStatusCode.OK, responseString);
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

        /// <summary>
        /// method for search hotel
        /// </summary>
        /// <returns></returns>
        [ActionName("SearchHotelWithDetail")]
        public async Task<HttpResponseMessage> SearchHotelWithDetail()
        {
            Models.HotelDetail.Hotel selectedHotel = new Models.HotelDetail.Hotel();

            if (hepler.CheckHeader(Request))
            {
                string responseString = string.Empty;
                HttpResponseMessage response = null;
                try
                {
                    var searchParamterXml = await Request.Content.ReadAsByteArrayAsync();
                    string searchParameter = System.Text.Encoding.UTF8.GetString(searchParamterXml);
                    XmlSerializer serializer = new XmlSerializer(typeof(HotelRequest));
                    using (TextReader reader = new StringReader(searchParameter))
                    {
                        HotelRequest searchDetail = (HotelRequest)serializer.Deserialize(reader);

                        var endTag = "</guestDetails>";
                        int startIndex = searchParameter.IndexOf("<guestDetails>");
                        int endIndex = searchParameter.IndexOf(endTag, startIndex) + endTag.Length;

                        var guestDetailXml = searchParameter.Substring(startIndex, endIndex - startIndex);

                        var searchRequestObject = HolelTemplateXml.SearchHotel;
                        searchRequestObject = searchRequestObject.Replace("startDateValue", searchDetail.Start).
                            Replace("endDateValue", searchDetail.End).
                            Replace("guestDetailsXmlValue", guestDetailXml).
                            Replace("hotelCityNameValue", searchDetail.HotelCityName).
                            Replace("hotelNameValue", searchDetail.HotelName).
                            Replace("areaValue", searchDetail.Area).
                            Replace("attractionValue", searchDetail.Attraction).
                            Replace("ratingValue", searchDetail.Rating).
                            Replace("sortingPreferenceValue", searchDetail.SortingPreference).
                            Replace("hotelPackageValue", searchDetail.HotelPackage);

                        var hotelSearch = new HotelAvailSearch();
                        hotelSearch.Url = "http://demo.arzoo.com/HotelXML_V1.2/services/HotelAvailSearch";
                        responseString = hotelSearch.getHotelAvailSearch(searchRequestObject, string.Empty);
                        if (string.IsNullOrEmpty(responseString))
                        {
                            response = Request.CreateResponse<string>(HttpStatusCode.NotFound, "<error>No Content Found</error>");
                        }
                        if (!responseString.Contains("<"))
                        {
                            response = Request.CreateResponse<string>(HttpStatusCode.NotFound, responseString);
                        }
                        else
                        {
                            var serializer1 = new XmlSerializer(typeof(Models.HotelDetail.ArzHotelAvailResp));

                            Models.HotelDetail.ArzHotelAvailResp hotelAvailResp;
                            using (TextReader reader1 = new StringReader(responseString))
                            {
                                hotelAvailResp = (Models.HotelDetail.ArzHotelAvailResp)serializer1.Deserialize(reader1);
                            }

                            if (hotelAvailResp == null)
                            {
                                response = Request.CreateResponse<string>(HttpStatusCode.NotFound, "<error>No Content Found</error>");
                            }
                            else if (!string.IsNullOrEmpty(hotelAvailResp.Error))
                            {
                                response = Request.CreateResponse<string>(HttpStatusCode.BadRequest, "<Error>" + hotelAvailResp.Error + "</Error>");
                            }
                            else
                            {
                                selectedHotel = hotelAvailResp.Searchresult.Hotel.FirstOrDefault(h => h.Hoteldetail.Hotelid == searchDetail.hotelid);
                                /*Code to get Images of hotel*/

                                searchRequestObject = HolelTemplateXml.SearchHotelDescription;
                                searchRequestObject = searchRequestObject.Replace("hotelidValue", searchDetail.hotelid).
                                    Replace("webServiceValue", searchDetail.webService).
                                    Replace("cityValue", searchDetail.HotelCityName);

                                var hotelDetail = new HotelDetails();
                                hotelDetail.Url = "http://demo.arzoo.com/HotelXML_V1.2/services/HotelDetails";
                                responseString = hotelDetail.getHotelDetails(searchRequestObject, string.Empty);
                                if (string.IsNullOrEmpty(responseString))
                                {
                                    response = Request.CreateResponse<string>(HttpStatusCode.NotFound, "No Content Found");
                                }
                                else
                                {
                                    //responseString = "<string xmlns='http://schemas.microsoft.com/2003/10/Serialization/'>&lt;arzHotelDescResp&gt; &lt;searchresult&gt; &lt;hotel&gt; &lt;hoteldetail&gt; &lt;hotelname&gt;The Grand&lt;/hotelname&gt; &lt;hoteldesc&gt;This luxury hotel is located in the commercial hub of Vasant Kunj area. There are 390 contemporary-styled guest rooms, which overlook the pool and lush green gardens. Business events can be held in any of its 15 spacious convention halls, a business centre and a mini-conference room, which can seat a maximum of five people. Some fine dining options include Brix, an Italian restaurant, Enoki, a Japanese restaurant and Grand Cafe, the 24 hour coffee shop.&lt;/hoteldesc&gt; &lt;hotelchain&gt;&lt;/hotelchain&gt; &lt;starrating&gt;5&lt;/starrating&gt; &lt;city&gt;NEW DELHI&lt;/city&gt; &lt;country&gt;India&lt;/country&gt; &lt;noofrooms&gt;0&lt;/noofrooms&gt; &lt;rph&gt;00000898&lt;/rph&gt; &lt;webService&gt;arzooB&lt;/webService&gt; &lt;contactinfo&gt; &lt;address&gt;Nelson Mandela Marg (close to airport), Vasant Kunj Phase II, Vasant Kunj, NEW DELHI, DELHI, India, Pin-110070&lt;/address&gt; &lt;pincode&gt;NA&lt;/pincode&gt; &lt;citywiselocation&gt;Vasant Kunj&lt;/citywiselocation&gt; &lt;locationinfo&gt;This luxury hotel is located in the commercial hub of Vasant Kunj area. There are 390 contemporary-styled guest rooms, which overlook the pool and lush green gardens. Business events can be held in any of its 15 spacious convention halls, a business centre and a mini-conference room, which can seat a maximum of five people. Some fine dining options include Brix, an Italian restaurant, Enoki, a Japanese restaurant and Grand Cafe, the 24 hour coffee shop.&lt;/locationinfo&gt; &lt;phone&gt;NA&lt;/phone&gt; &lt;fax&gt;NA&lt;/fax&gt; &lt;email&gt;NA&lt;/email&gt; &lt;website&gt;NA&lt;/website&gt; &lt;/contactinfo&gt; &lt;bookinginfo&gt; &lt;checkintime&gt;12:00:00&lt;/checkintime&gt; &lt;checkouttime&gt;12:00:00&lt;/checkouttime&gt; &lt;/bookinginfo&gt; &lt;services&gt; &lt;creditcards&gt;All&lt;/creditcards&gt; &lt;hotelservices&gt;Concierge,Luggage storage,24-hour front desk,Internet access-high-speed,Internet access-surcharge,Limousine service available,Porter/bellhop,Restaurant,Steam room,Air-conditioned public areas,Bar/lounge,Suitable for children,Currency exchange,Elevator/lift,Health club,Laundry facilities,Meeting room(s) small groups,Parking(valet),Sauna,Security guard,Smoke-free property,Coffee shop or cafe,Complimentary breakfast,Spa services onsite - Free,Swimming pool,Airport transportation(surcharge),Travel counter,Babysitting or child care ,Business center,Express check-in/check-out,Fitness equipment,Medical services,Secretarial services,Doorman,Banquet facilities,Breakfast services,Complimentary Newspapers in lobby,Backup generator,Wedding services,Number of floors,Room service(24 hours),Safe deposit box-front desk,Spa services on site - Chargeable&lt;/hotelservices&gt; &lt;roomservices&gt;Temperature Control ,Slippers,Turndown service,Voicemail,Child care(in room,surcharge),Climate control,Complimentary toiletries,Extra towels,linens,bedding,Designer toiletries,Attached Bathroom,Refrigerator-Room,Rollaway beds,Smoking rooms,Television-Room,Telephone-Room,Central Air Conditioning,Desk,Coffee/tea maker,Complimentary newspaper,Hair dryer,Iron/ironing board(on request),Reading lamps,Moisturiser,Bathrobes,Clock radio,Housekeeping,Air conditioning-Room,Bottled water in room(complimentary),Cable/satellite TV,Direct-dial phone,Electronic/magnetic keys,In-room safe,Internet access-high speed,Minibar,Multi-line phone,Separate sitting area,Wakeup-calls,Welcome amenities,Wheelchair accessibility-Room,Conditioner,Shower Caps,Global Direct Dial,Inroom Broadband,H/C running water,Handicap accessible rooms&lt;/roomservices&gt; &lt;/services&gt; &lt;facilities&gt;NA&lt;/facilities&gt; &lt;images&gt; &lt;image&gt; &lt;imagepath&gt;http://cdn.travelpartnerweb.com/DesiyaImages/Image/1/nxd/maw/wye/mbv/HA.jpg&lt;/imagepath&gt; &lt;/image&gt; &lt;image&gt; &lt;imagepath&gt;http://cdn.travelpartnerweb.com/DesiyaImages/Image/1/nxd/maw/wye/mbv/HO.jpg&lt;/imagepath&gt; &lt;/image&gt; &lt;image&gt; &lt;imagepath&gt;http://cdn.travelpartnerweb.com/DesiyaImages/Image/1/nxd/maw/wye/mbv/0000002062RD.jpg&lt;/imagepath&gt; &lt;/image&gt; &lt;image&gt; &lt;imagepath&gt;http://cdn.travelpartnerweb.com/DesiyaImages/Image/1/nxd/maw/wye/mbv/0000002064RD.jpg&lt;/imagepath&gt; &lt;/image&gt; &lt;image&gt; &lt;imagepath&gt;http://cdn.travelpartnerweb.com/DesiyaImages/Image/1/nxd/maw/wye/mbv/0000005823RD.jpg&lt;/imagepath&gt; &lt;/image&gt; &lt;image&gt; &lt;imagepath&gt;http://cdn.travelpartnerweb.com/DesiyaImages/Image/1/nxd/maw/wye/mbv/0000005822RD.jpg&lt;/imagepath&gt; &lt;/image&gt; &lt;image&gt; &lt;imagepath&gt;http://cdn.travelpartnerweb.com/DesiyaImages/Image/1/nxd/maw/wye/mbv/0000005821RD.jpg&lt;/imagepath&gt; &lt;/image&gt; &lt;image&gt; &lt;imagepath&gt;http://cdn.travelpartnerweb.com/DesiyaImages/Image/1/nxd/maw/wye/mbv/0000016673RD.jpg&lt;/imagepath&gt; &lt;/image&gt; &lt;image&gt; &lt;imagepath&gt;http://cdn.travelpartnerweb.com/DesiyaImages/Image/1/nxd/maw/wye/mbv/0000016662RD.jpg&lt;/imagepath&gt; &lt;/image&gt; &lt;image&gt; &lt;imagepath&gt;http://cdn.travelpartnerweb.com/DesiyaImages/Image/1/nxd/maw/wye/mbv/0000016661RD.jpg&lt;/imagepath&gt; &lt;/image&gt; &lt;image&gt; &lt;imagepath&gt;http://cdn.travelpartnerweb.com/DesiyaImages/Image/1/nxd/maw/wye/mbv/0000016554RD.jpg&lt;/imagepath&gt; &lt;/image&gt; &lt;image&gt; &lt;imagepath&gt;http://cdn.travelpartnerweb.com/DesiyaImages/Image/1/nxd/maw/wye/mbv/0000016553RD.jpg&lt;/imagepath&gt; &lt;/image&gt; &lt;image&gt; &lt;imagepath&gt;http://cdn.travelpartnerweb.com/DesiyaImages/Image/1/nxd/maw/wye/mbv/0000016674RD.jpg&lt;/imagepath&gt; &lt;/image&gt; &lt;image&gt; &lt;imagepath&gt;http://cdn.travelpartnerweb.com/DesiyaImages/Image/1/nxd/maw/wye/mbv/0000024122RD.jpg&lt;/imagepath&gt; &lt;/image&gt; &lt;image&gt; &lt;imagepath&gt;http://cdn.travelpartnerweb.com/DesiyaImages/Image/1/nxd/maw/wye/mbv/0000024903RD.jpg&lt;/imagepath&gt; &lt;/image&gt; &lt;image&gt; &lt;imagepath&gt;http://cdn.travelpartnerweb.com/DesiyaImages/Image/1/nxd/maw/wye/mbv/0000024234RD.jpg&lt;/imagepath&gt; &lt;/image&gt; &lt;image&gt; &lt;imagepath&gt;http://cdn.travelpartnerweb.com/DesiyaImages/Image/1/nxd/maw/wye/mbv/0000023334RD.jpg&lt;/imagepath&gt; &lt;/image&gt; &lt;image&gt; &lt;imagepath&gt;http://cdn.travelpartnerweb.com/DesiyaImages/Image/1/nxd/maw/wye/mbv/0000024127RD.jpg&lt;/imagepath&gt; &lt;/image&gt; &lt;image&gt; &lt;imagepath&gt;http://cdn.travelpartnerweb.com/DesiyaImages/Image/1/nxd/maw/wye/mbv/0000024905RD.jpg&lt;/imagepath&gt; &lt;/image&gt; &lt;image&gt; &lt;imagepath&gt;http://cdn.travelpartnerweb.com/DesiyaImages/Image/1/nxd/maw/wye/mbv/0000023333RD.jpg&lt;/imagepath&gt; &lt;/image&gt; &lt;image&gt; &lt;imagepath&gt;http://cdn.travelpartnerweb.com/DesiyaImages/Image/1/nxd/maw/wye/mbv/XT1.jpg&lt;/imagepath&gt; &lt;/image&gt; &lt;image&gt; &lt;imagepath&gt;http://cdn.travelpartnerweb.com/DesiyaImages/Image/1/nxd/maw/wye/mbv/XT2.jpg&lt;/imagepath&gt; &lt;/image&gt; &lt;image&gt; &lt;imagepath&gt;http://cdn.travelpartnerweb.com/DesiyaImages/Image/1/nxd/maw/wye/mbv/XT3.jpg&lt;/imagepath&gt; &lt;/image&gt; &lt;image&gt; &lt;imagepath&gt;http://cdn.travelpartnerweb.com/DesiyaImages/Image/1/nxd/maw/wye/mbv/XT4.jpg&lt;/imagepath&gt; &lt;/image&gt; &lt;image&gt; &lt;imagepath&gt;http://cdn.travelpartnerweb.com/DesiyaImages/Image/1/nxd/maw/wye/mbv/XT5.jpg&lt;/imagepath&gt; &lt;/image&gt; &lt;image&gt; &lt;imagepath&gt;http://cdn.travelpartnerweb.com/DesiyaImages/Image/1/nxd/maw/wye/mbv/XT6.jpg&lt;/imagepath&gt; &lt;/image&gt; &lt;image&gt; &lt;imagepath&gt;http://cdn.travelpartnerweb.com/DesiyaImages/Image/1/nxd/maw/wye/mbv/XT7.jpg&lt;/imagepath&gt; &lt;/image&gt; &lt;image&gt; &lt;imagepath&gt;http://cdn.travelpartnerweb.com/DesiyaImages/Image/1/nxd/maw/wye/mbv/XT8.jpg&lt;/imagepath&gt; &lt;/image&gt; &lt;image&gt; &lt;imagepath&gt;http://cdn.travelpartnerweb.com/DesiyaImages/Image/1/nxd/maw/wye/mbv/XT9.jpg&lt;/imagepath&gt; &lt;/image&gt; &lt;/images&gt; &lt;/hoteldetail&gt; &lt;/hotel&gt; &lt;/searchresult&gt; &lt;clientInfo&gt; &lt;partnerID&gt;100200&lt;/partnerID&gt; &lt;/clientInfo&gt; &lt;searchquery&gt; &lt;hotelinfo&gt; &lt;hotelid&gt;00000898&lt;/hotelid&gt; &lt;webService&gt;arzooB&lt;/webService&gt; &lt;city&gt;New Delhi&lt;/city&gt; &lt;/hotelinfo&gt; &lt;/searchquery&gt; &lt;/arzHotelDescResp&gt;</string>";
                                    //responseString = RemoveLineEndings(responseString);
                                    //responseString = RemoveAllNamespaces(responseString);
                                    Console.Write(responseString);
                                    // response = Request.CreateResponse<string>(HttpStatusCode.OK, responseString);

                                    /*******uncomment this****/
                                    serializer1 = new XmlSerializer(typeof(ArzHotelDescResp));

                                    ArzHotelDescResp hotelSecpResp;
                                    using (TextReader reader1 = new StringReader(responseString))
                                    {
                                        hotelSecpResp = (ArzHotelDescResp)serializer1.Deserialize(reader1);
                                    }

                                    if (hotelSecpResp == null)
                                    {
                                        response = Request.CreateResponse<string>(HttpStatusCode.NoContent, "No Content Found");
                                    }
                                    else
                                    {
                                        selectedHotel.Hoteldetail.Images.Image = hotelSecpResp.Searchresult.Hotel.Hoteldetail.Images.Image;
                                        selectedHotel.Hoteldetail.Services = hotelSecpResp.Searchresult.Hotel.Hoteldetail.Services;

                                        //return Content(HttpStatusCode.OK, selectedHotel, Configuration.Formatters.XmlFormatter);
                                        //response = Request.CreateResponse<string>(HttpStatusCode.NotFound, selectedHotel);

                                        XmlSerializer hoteldetail = new XmlSerializer(typeof(Models.HotelDetail.Hotel));
                                        var responseXml = "";

                                        using (var sww = new StringWriter())
                                        {
                                            using (XmlWriter writer = XmlWriter.Create(sww))
                                            {
                                                hoteldetail.Serialize(writer, selectedHotel);
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

        [ActionName("SearchHotelDescription")]
        public async Task<HttpResponseMessage> SearchHotelDescription()
        {
            if (hepler.CheckHeader(Request))
            {
                string responseString = string.Empty;
                HttpResponseMessage response = null;
                try
                {
                    var searchParamterXml = await Request.Content.ReadAsByteArrayAsync();
                    string searchParameter = System.Text.Encoding.UTF8.GetString(searchParamterXml);
                    XmlSerializer serializer = new XmlSerializer(typeof(HotelDescriptionRequest));
                    using (TextReader reader = new StringReader(searchParameter))
                    {
                        HotelDescriptionRequest searchDetail = (HotelDescriptionRequest)serializer.Deserialize(reader);

                        var searchRequestObject = HolelTemplateXml.SearchHotelDescription;
                        searchRequestObject = searchRequestObject.Replace("hotelidValue", searchDetail.hotelid).
                            Replace("webServiceValue", searchDetail.webService).
                            Replace("cityValue", searchDetail.city);

                        var hotelSearch = new HotelDetails();
                        hotelSearch.Url = "http://demo.arzoo.com/HotelXML_V1.2/services/HotelDetails";
                        responseString = hotelSearch.getHotelDetails(searchRequestObject, string.Empty);
                        if (string.IsNullOrEmpty(responseString))
                        {
                            response = Request.CreateResponse<string>(HttpStatusCode.NotFound, "No Content Found");
                        }
                        else
                        {
                            //responseString = "<string xmlns='http://schemas.microsoft.com/2003/10/Serialization/'>&lt;arzHotelDescResp&gt; &lt;searchresult&gt; &lt;hotel&gt; &lt;hoteldetail&gt; &lt;hotelname&gt;The Grand&lt;/hotelname&gt; &lt;hoteldesc&gt;This luxury hotel is located in the commercial hub of Vasant Kunj area. There are 390 contemporary-styled guest rooms, which overlook the pool and lush green gardens. Business events can be held in any of its 15 spacious convention halls, a business centre and a mini-conference room, which can seat a maximum of five people. Some fine dining options include Brix, an Italian restaurant, Enoki, a Japanese restaurant and Grand Cafe, the 24 hour coffee shop.&lt;/hoteldesc&gt; &lt;hotelchain&gt;&lt;/hotelchain&gt; &lt;starrating&gt;5&lt;/starrating&gt; &lt;city&gt;NEW DELHI&lt;/city&gt; &lt;country&gt;India&lt;/country&gt; &lt;noofrooms&gt;0&lt;/noofrooms&gt; &lt;rph&gt;00000898&lt;/rph&gt; &lt;webService&gt;arzooB&lt;/webService&gt; &lt;contactinfo&gt; &lt;address&gt;Nelson Mandela Marg (close to airport), Vasant Kunj Phase II, Vasant Kunj, NEW DELHI, DELHI, India, Pin-110070&lt;/address&gt; &lt;pincode&gt;NA&lt;/pincode&gt; &lt;citywiselocation&gt;Vasant Kunj&lt;/citywiselocation&gt; &lt;locationinfo&gt;This luxury hotel is located in the commercial hub of Vasant Kunj area. There are 390 contemporary-styled guest rooms, which overlook the pool and lush green gardens. Business events can be held in any of its 15 spacious convention halls, a business centre and a mini-conference room, which can seat a maximum of five people. Some fine dining options include Brix, an Italian restaurant, Enoki, a Japanese restaurant and Grand Cafe, the 24 hour coffee shop.&lt;/locationinfo&gt; &lt;phone&gt;NA&lt;/phone&gt; &lt;fax&gt;NA&lt;/fax&gt; &lt;email&gt;NA&lt;/email&gt; &lt;website&gt;NA&lt;/website&gt; &lt;/contactinfo&gt; &lt;bookinginfo&gt; &lt;checkintime&gt;12:00:00&lt;/checkintime&gt; &lt;checkouttime&gt;12:00:00&lt;/checkouttime&gt; &lt;/bookinginfo&gt; &lt;services&gt; &lt;creditcards&gt;All&lt;/creditcards&gt; &lt;hotelservices&gt;Concierge,Luggage storage,24-hour front desk,Internet access-high-speed,Internet access-surcharge,Limousine service available,Porter/bellhop,Restaurant,Steam room,Air-conditioned public areas,Bar/lounge,Suitable for children,Currency exchange,Elevator/lift,Health club,Laundry facilities,Meeting room(s) small groups,Parking(valet),Sauna,Security guard,Smoke-free property,Coffee shop or cafe,Complimentary breakfast,Spa services onsite - Free,Swimming pool,Airport transportation(surcharge),Travel counter,Babysitting or child care ,Business center,Express check-in/check-out,Fitness equipment,Medical services,Secretarial services,Doorman,Banquet facilities,Breakfast services,Complimentary Newspapers in lobby,Backup generator,Wedding services,Number of floors,Room service(24 hours),Safe deposit box-front desk,Spa services on site - Chargeable&lt;/hotelservices&gt; &lt;roomservices&gt;Temperature Control ,Slippers,Turndown service,Voicemail,Child care(in room,surcharge),Climate control,Complimentary toiletries,Extra towels,linens,bedding,Designer toiletries,Attached Bathroom,Refrigerator-Room,Rollaway beds,Smoking rooms,Television-Room,Telephone-Room,Central Air Conditioning,Desk,Coffee/tea maker,Complimentary newspaper,Hair dryer,Iron/ironing board(on request),Reading lamps,Moisturiser,Bathrobes,Clock radio,Housekeeping,Air conditioning-Room,Bottled water in room(complimentary),Cable/satellite TV,Direct-dial phone,Electronic/magnetic keys,In-room safe,Internet access-high speed,Minibar,Multi-line phone,Separate sitting area,Wakeup-calls,Welcome amenities,Wheelchair accessibility-Room,Conditioner,Shower Caps,Global Direct Dial,Inroom Broadband,H/C running water,Handicap accessible rooms&lt;/roomservices&gt; &lt;/services&gt; &lt;facilities&gt;NA&lt;/facilities&gt; &lt;images&gt; &lt;image&gt; &lt;imagepath&gt;http://cdn.travelpartnerweb.com/DesiyaImages/Image/1/nxd/maw/wye/mbv/HA.jpg&lt;/imagepath&gt; &lt;/image&gt; &lt;image&gt; &lt;imagepath&gt;http://cdn.travelpartnerweb.com/DesiyaImages/Image/1/nxd/maw/wye/mbv/HO.jpg&lt;/imagepath&gt; &lt;/image&gt; &lt;image&gt; &lt;imagepath&gt;http://cdn.travelpartnerweb.com/DesiyaImages/Image/1/nxd/maw/wye/mbv/0000002062RD.jpg&lt;/imagepath&gt; &lt;/image&gt; &lt;image&gt; &lt;imagepath&gt;http://cdn.travelpartnerweb.com/DesiyaImages/Image/1/nxd/maw/wye/mbv/0000002064RD.jpg&lt;/imagepath&gt; &lt;/image&gt; &lt;image&gt; &lt;imagepath&gt;http://cdn.travelpartnerweb.com/DesiyaImages/Image/1/nxd/maw/wye/mbv/0000005823RD.jpg&lt;/imagepath&gt; &lt;/image&gt; &lt;image&gt; &lt;imagepath&gt;http://cdn.travelpartnerweb.com/DesiyaImages/Image/1/nxd/maw/wye/mbv/0000005822RD.jpg&lt;/imagepath&gt; &lt;/image&gt; &lt;image&gt; &lt;imagepath&gt;http://cdn.travelpartnerweb.com/DesiyaImages/Image/1/nxd/maw/wye/mbv/0000005821RD.jpg&lt;/imagepath&gt; &lt;/image&gt; &lt;image&gt; &lt;imagepath&gt;http://cdn.travelpartnerweb.com/DesiyaImages/Image/1/nxd/maw/wye/mbv/0000016673RD.jpg&lt;/imagepath&gt; &lt;/image&gt; &lt;image&gt; &lt;imagepath&gt;http://cdn.travelpartnerweb.com/DesiyaImages/Image/1/nxd/maw/wye/mbv/0000016662RD.jpg&lt;/imagepath&gt; &lt;/image&gt; &lt;image&gt; &lt;imagepath&gt;http://cdn.travelpartnerweb.com/DesiyaImages/Image/1/nxd/maw/wye/mbv/0000016661RD.jpg&lt;/imagepath&gt; &lt;/image&gt; &lt;image&gt; &lt;imagepath&gt;http://cdn.travelpartnerweb.com/DesiyaImages/Image/1/nxd/maw/wye/mbv/0000016554RD.jpg&lt;/imagepath&gt; &lt;/image&gt; &lt;image&gt; &lt;imagepath&gt;http://cdn.travelpartnerweb.com/DesiyaImages/Image/1/nxd/maw/wye/mbv/0000016553RD.jpg&lt;/imagepath&gt; &lt;/image&gt; &lt;image&gt; &lt;imagepath&gt;http://cdn.travelpartnerweb.com/DesiyaImages/Image/1/nxd/maw/wye/mbv/0000016674RD.jpg&lt;/imagepath&gt; &lt;/image&gt; &lt;image&gt; &lt;imagepath&gt;http://cdn.travelpartnerweb.com/DesiyaImages/Image/1/nxd/maw/wye/mbv/0000024122RD.jpg&lt;/imagepath&gt; &lt;/image&gt; &lt;image&gt; &lt;imagepath&gt;http://cdn.travelpartnerweb.com/DesiyaImages/Image/1/nxd/maw/wye/mbv/0000024903RD.jpg&lt;/imagepath&gt; &lt;/image&gt; &lt;image&gt; &lt;imagepath&gt;http://cdn.travelpartnerweb.com/DesiyaImages/Image/1/nxd/maw/wye/mbv/0000024234RD.jpg&lt;/imagepath&gt; &lt;/image&gt; &lt;image&gt; &lt;imagepath&gt;http://cdn.travelpartnerweb.com/DesiyaImages/Image/1/nxd/maw/wye/mbv/0000023334RD.jpg&lt;/imagepath&gt; &lt;/image&gt; &lt;image&gt; &lt;imagepath&gt;http://cdn.travelpartnerweb.com/DesiyaImages/Image/1/nxd/maw/wye/mbv/0000024127RD.jpg&lt;/imagepath&gt; &lt;/image&gt; &lt;image&gt; &lt;imagepath&gt;http://cdn.travelpartnerweb.com/DesiyaImages/Image/1/nxd/maw/wye/mbv/0000024905RD.jpg&lt;/imagepath&gt; &lt;/image&gt; &lt;image&gt; &lt;imagepath&gt;http://cdn.travelpartnerweb.com/DesiyaImages/Image/1/nxd/maw/wye/mbv/0000023333RD.jpg&lt;/imagepath&gt; &lt;/image&gt; &lt;image&gt; &lt;imagepath&gt;http://cdn.travelpartnerweb.com/DesiyaImages/Image/1/nxd/maw/wye/mbv/XT1.jpg&lt;/imagepath&gt; &lt;/image&gt; &lt;image&gt; &lt;imagepath&gt;http://cdn.travelpartnerweb.com/DesiyaImages/Image/1/nxd/maw/wye/mbv/XT2.jpg&lt;/imagepath&gt; &lt;/image&gt; &lt;image&gt; &lt;imagepath&gt;http://cdn.travelpartnerweb.com/DesiyaImages/Image/1/nxd/maw/wye/mbv/XT3.jpg&lt;/imagepath&gt; &lt;/image&gt; &lt;image&gt; &lt;imagepath&gt;http://cdn.travelpartnerweb.com/DesiyaImages/Image/1/nxd/maw/wye/mbv/XT4.jpg&lt;/imagepath&gt; &lt;/image&gt; &lt;image&gt; &lt;imagepath&gt;http://cdn.travelpartnerweb.com/DesiyaImages/Image/1/nxd/maw/wye/mbv/XT5.jpg&lt;/imagepath&gt; &lt;/image&gt; &lt;image&gt; &lt;imagepath&gt;http://cdn.travelpartnerweb.com/DesiyaImages/Image/1/nxd/maw/wye/mbv/XT6.jpg&lt;/imagepath&gt; &lt;/image&gt; &lt;image&gt; &lt;imagepath&gt;http://cdn.travelpartnerweb.com/DesiyaImages/Image/1/nxd/maw/wye/mbv/XT7.jpg&lt;/imagepath&gt; &lt;/image&gt; &lt;image&gt; &lt;imagepath&gt;http://cdn.travelpartnerweb.com/DesiyaImages/Image/1/nxd/maw/wye/mbv/XT8.jpg&lt;/imagepath&gt; &lt;/image&gt; &lt;image&gt; &lt;imagepath&gt;http://cdn.travelpartnerweb.com/DesiyaImages/Image/1/nxd/maw/wye/mbv/XT9.jpg&lt;/imagepath&gt; &lt;/image&gt; &lt;/images&gt; &lt;/hoteldetail&gt; &lt;/hotel&gt; &lt;/searchresult&gt; &lt;clientInfo&gt; &lt;partnerID&gt;100200&lt;/partnerID&gt; &lt;/clientInfo&gt; &lt;searchquery&gt; &lt;hotelinfo&gt; &lt;hotelid&gt;00000898&lt;/hotelid&gt; &lt;webService&gt;arzooB&lt;/webService&gt; &lt;city&gt;New Delhi&lt;/city&gt; &lt;/hotelinfo&gt; &lt;/searchquery&gt; &lt;/arzHotelDescResp&gt;</string>";
                            //responseString = RemoveLineEndings(responseString);
                            //responseString = RemoveAllNamespaces(responseString);
                            Console.Write(responseString);
                            // response = Request.CreateResponse<string>(HttpStatusCode.OK, responseString);

                            /*******uncomment this****/
                            var serializer1 = new XmlSerializer(typeof(ArzHotelDescResp));

                            ArzHotelDescResp hotelAvailResp;
                            using (TextReader reader1 = new StringReader(responseString))
                            {
                                hotelAvailResp = (ArzHotelDescResp)serializer1.Deserialize(reader1);
                            }

                            if (hotelAvailResp == null)
                            {
                                response = Request.CreateResponse<string>(HttpStatusCode.NoContent, "No Content Found");
                            }
                            else
                            {
                                XmlSerializer cancelationRequest = new XmlSerializer(typeof(ArzHotelDescResp));
                                var responseXml = "";

                                using (var sww = new StringWriter())
                                {
                                    using (XmlWriter writer = XmlWriter.Create(sww))
                                    {
                                        cancelationRequest.Serialize(writer, hotelAvailResp);
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

        [ActionName("HotelPolicy")]
        public async Task<HttpResponseMessage> HotelPolicy()
        {
            if (hepler.CheckHeader(Request))
            {
                string responseString = string.Empty;
                HttpResponseMessage response = null;
                try
                {
                    var searchParamterXml = await Request.Content.ReadAsByteArrayAsync();
                    string searchParameter = System.Text.Encoding.UTF8.GetString(searchParamterXml);
                    XmlSerializer serializer = new XmlSerializer(typeof(HotelDescriptionRequest));
                    using (TextReader reader = new StringReader(searchParameter))
                    {
                        HotelDescriptionRequest searchDetail = (HotelDescriptionRequest)serializer.Deserialize(reader);

                        var searchRequestObject = HolelTemplateXml.HotelPolicyXml;
                        searchRequestObject = searchRequestObject.Replace("hotelidValue", searchDetail.hotelid).
                            Replace("webServiceValue", searchDetail.webService).
                            Replace("ratePlanTypeValue", searchDetail.RatePlanType).
                            Replace("roomTypeCodeValue", searchDetail.RoomTypeCode).
                            Replace("checkInDateValue", searchDetail.CheckInDate).
                            Replace("checkOutDateValue", searchDetail.CheckOutDate);

                        var hotelPolicy = new HotelPolicy();
                        hotelPolicy.Url = "http://demo.arzoo.com/HotelXML_V1.2/services/HotelPolicy";
                        responseString = hotelPolicy.getHotelPolicy(searchRequestObject, string.Empty);
                        if (string.IsNullOrEmpty(responseString))
                        {
                            response = Request.CreateResponse<string>(HttpStatusCode.NotFound, "<error>No Content Found</error>");
                        }
                        else
                        {
                            //response = Request.CreateResponse<string>(HttpStatusCode.OK, responseString);

                            //  responseString = "<string xmlns='http://schemas.microsoft.com/2003/10/Serialization/'>&lt;arzHotelPolicyResp&gt;  &lt;clientInfo&gt;    &lt;partnerID&gt;100200&lt;/partnerID&gt;  &lt;/clientInfo&gt;  &lt;searchquery&gt;    &lt;hotelinfo&gt;      &lt;hotelid&gt;00000898&lt;/hotelid&gt;      &lt;webService&gt;arzooB&lt;/webService&gt;      &lt;ratePlanType&gt;0000184150&lt;/ratePlanType&gt;      &lt;roomTypeCode&gt;&lt;/roomTypeCode&gt;    &lt;/hotelinfo&gt;    &lt;residentOfIndia&gt;true&lt;/residentOfIndia&gt;    &lt;currency&gt;INR&lt;/currency&gt;  &lt;/searchquery&gt;  &lt;searchresult&gt;    &lt;cancellationPolicy&gt;      &lt;policy&gt;No Cancellation policy found, Please Contact support&lt;/policy&gt;    &lt;/cancellationPolicy&gt;    &lt;hotelPolicyRules&gt;      &lt;policy&gt;Guest must be over 18 years of age to check-in to this hotel.&lt;/policy&gt;      &lt;policy&gt;As per Government regulations, it is mandatory for all guests above 18 years of age to carry a valid photo identity card &amp;amp; address proof at the time of check-in.&amp;lt;/br&amp;gt;Please note that failure to abide by this can result with the hotel denying a check-in. Hotels normally do not provide any refund for such cancellations.&lt;/policy&gt;      &lt;policy&gt;The standard check-in and check-out times are 12 noon. Early check-in or late check-out is subject to hotel availability and may also be chargeable by the hotel.&amp;lt;/br&amp;gt;Any early check-in or late check-out request must be directed to and reconfirmed with the hotel directly.&lt;/policy&gt;      &lt;policy&gt;Failure to check-in to the hotel, will attract the full cost of stay or penalty as per the hotel cancellation policy.&lt;/policy&gt;      &lt;policy&gt;Hotels charge a compulsory Gala Dinner Supplement during Christmas, New Year&amp;apos;s eve or other special events and festivals like Diwali or Dusshera.&amp;lt;/br&amp;gt;These additional  charge are not included in the booking amount and will be collected directly at the hotel. &lt;/policy&gt;      &lt;policy&gt;There might be seasonal variation in hotel tariff rates during Peak days, for example URS period in Ajmer or Lord Jagannath Rath Yatra in Puri,&amp;lt;/br&amp;gt;the room tariff differences if any will have to be borne and paid by the customer directly at the hotel, if the booking stay period falls during such dates.&lt;/policy&gt;      &lt;policy&gt;All additional charges other than the room charges and inclusions as mentioned in the booking voucher are to be borne and paid separately during check-out.&amp;lt;/br&amp;gt;Please make sure that you are aware of all such charges that may comes as extras. Some of them can be WiFi costs, Mini Bar, Laundry Expenses, Telephone calls, Room Service, Snacks etc. &lt;/policy&gt;      &lt;policy&gt;Some hotels may have policies that do not allow unmarried / unrelated couples or certain foreign nationalities to check-in without the correct documentation.&amp;lt;/br&amp;gt;No refund will be applicable in case the hotel denies check-in under such circumstances. If you have any doubts on this, do call us for any assistance.&lt;/policy&gt;      &lt;policy&gt;Any changes or booking modifications are subject to availability and charges may apply as per the hotel policies.&lt;/policy&gt;    &lt;/hotelPolicyRules&gt;  &lt;/searchresult&gt;&lt;/arzHotelPolicyResp&gt;</string>";
                            ///**resonse dosnt add this***/
                            //responseString = "<string xmlns='http://schemas.microsoft.com/2003/10/Serialization/'>" + responseString + "</string>";
                            // responseString = RemoveLineEndings(responseString);
                            // responseString = this.RemoveAllNamespacesTest(responseString);
                            var serializer1 = new XmlSerializer(typeof(ArzHotelPolicyResp));

                            ArzHotelPolicyResp hotelAvailResp;
                            using (TextReader reader1 = new StringReader(responseString))
                            {
                                hotelAvailResp = (ArzHotelPolicyResp)serializer1.Deserialize(reader1);
                            }

                            if (hotelAvailResp == null)
                            {
                                response = Request.CreateResponse<string>(HttpStatusCode.NotFound, "<error>No Content Found</error>");
                            }
                            else
                            {
                                XmlSerializer cancelationRequest = new XmlSerializer(typeof(ArzHotelPolicyResp));
                                var responseXml = "";

                                using (var sww = new StringWriter())
                                {
                                    using (XmlWriter writer = XmlWriter.Create(sww))
                                    {
                                        cancelationRequest.Serialize(writer, hotelAvailResp);
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
                    // responseString = e.Message;
                    response = Request.CreateResponse<string>(HttpStatusCode.NotFound, responseString);
                }

                return response;
            }

            return null;
        }

        [ActionName("ProvisionalBooking")]
        public async Task<HttpResponseMessage> ProvisionalBooking()
        {
            if (hepler.CheckHeader(Request))
            {
                string responseString = string.Empty;
                HttpResponseMessage response = null;
                try
                {
                    var searchParamterXml = await Request.Content.ReadAsByteArrayAsync();
                    string searchParameter = System.Text.Encoding.UTF8.GetString(searchParamterXml);

                    /*****Hotel info xml vale*****/
                    var endTag = "</hotelinfo>";
                    int startIndex = searchParameter.IndexOf("<hotelinfo>");
                    int endIndex = searchParameter.IndexOf(endTag, startIndex) + endTag.Length;

                    var hotelInformationXml = searchParameter.Substring(startIndex, endIndex - startIndex);

                    /****Guest detail xml value*****/

                    endTag = "</roomStayCandidate>";
                    startIndex = searchParameter.IndexOf("<roomStayCandidate>");
                    endIndex = searchParameter.IndexOf(endTag, startIndex) + endTag.Length;
                    var guestDetailXml = searchParameter.Substring(startIndex, endIndex - startIndex);

                    /****Guest information xml value*****/

                    endTag = "</guestInformation>";
                    startIndex = searchParameter.IndexOf("<guestInformation>");
                    endIndex = searchParameter.IndexOf(endTag, startIndex) + endTag.Length;

                    var guestInformationXml = searchParameter.Substring(startIndex, endIndex - startIndex);

                    /****Rate Band xml value*****/

                    endTag = "</ratebands>";
                    startIndex = searchParameter.IndexOf("<ratebands>");
                    endIndex = searchParameter.IndexOf(endTag, startIndex) + endTag.Length;

                    var rateBandsXml = searchParameter.Substring(startIndex, endIndex - startIndex);

                    var searchRequestObject = HolelTemplateXml.ProvisionalBookingXml;
                    searchRequestObject = searchRequestObject.Replace("HotelInfoXmlValue", hotelInformationXml).
                        Replace("ratebandsXmlValue", rateBandsXml).
                        Replace("guestInformationXmlValue", guestInformationXml).
                        Replace("roomStayCandidateXmlValue", guestDetailXml);

                    searchRequestObject = searchRequestObject.Replace("sessionidValue", "50900072934233286112");
                    var hotelBooking = new HotelProvisional();
                    hotelBooking.Url = "http://demo.arzoo.com/HotelXML_V1.2/services/HotelProvisional";

                    //searchRequestObject = "<arzHotelProvReq><clientInfo><username>DiscountTadkaXML</username><password>*326F36BA87BCE3E86B69733BF874CC82DAE95143</password><partnerID>100200</partnerID><sessionid></sessionid><userID>77743579</userID><userType>ArzooHWS1.1</userType></clientInfo>"+
                    //    "<allocquery><currency>INR</currency><hotelinfo><hotelid>00015297</hotelid><roomtype>Standard Double Room</roomtype><webService>arzooB</webService><fromdate>06/06/2017</fromdate><todate>07/06/2017</todate><roomTypeCode>0000051992</roomTypeCode><ratePlanType>0000187494</ratePlanType></hotelinfo><roomStayCandidate><guestDetails><adults>2</adults><child/></guestDetails></roomStayCandidate><ratebands><validdays>1111111</validdays><wsKey>sRY/4+arP7JPuT6OzXspSJpPbNVM+j22xDrDapJ4gq41FhycCo9cdsQ6w2qSeIKuqOqjucdng3fdUAi7eQNxoAW2YuDXnt6bT7k+js17KUiaT2zVTPo9tsQ6w2qSeIKuFuYUM0JK2OKOUSHMG+t1OM7ypuHGO+F4OxkRUznNPzb4ObtmxiLMossgGy4hB5cjtpS5KQFG8E2XeSJaL/cyFPRm1B4L965uPC0M2sa4L77mazCKZpmMXHCdUHHYpL1/AeaHEazJMrtwnVBx2KS9fwHmhxGsyTK79glFDUZDEBXlCYR/0AcewcQ0aG0Pnb6y4lDA/vuhHwHEOsNqkniCrmAuNivOy7yP5UbL9G65YSJhPlvm7+yfwppPbNVM+j22UXq7BV6LV742Lb9+Hof4HhmRLvBpzSP23JMm9NPt3o2cdDVxQp7VBN8Xb3hPTM5BeLOX2WSZ65PEOsNqkniCrsSeNaHgldt7hG1JphJZIAw=</wsKey><extGuestTotal>0</extGuestTotal><roomTotal>481</roomTotal><servicetaxTotal>0</servicetaxTotal><discount>28.0</discount><commission>0</commission></ratebands><guestInformation><title>Mr.</title><firstName>V</firstName><middleName/><lastName>Balakrishna</lastName><phoneNumber><countryCode>91</countryCode><areaCode>01482</areaCode><number>9000364199</number><extension>1</extension></phoneNumber><email>sammetaharish@gmail.com</email><address><addressLine>CHITTOOR</addressLine><city>Chittoor</city><zipCode>517128</zipCode><state>Andhra Pradesh</state><country>India</country></address><residentOfIndia>true</residentOfIndia></guestInformation></allocquery></arzHotelProvReq>";

                    /// searchRequestObject = "<arzHotelProvReq><clientInfo><username>DiscountTadkaXML</username><password>*326F36BA87BCE3E86B69733BF874CC82DAE95143</password><partnerID>100200</partnerID><sessionid></sessionid><userID>77743579</userID><userType>ArzooHWS1.1</userType></clientInfo><allocquery><currency>INR</currency><hotelinfo><hotelid>00012611</hotelid><roomtype>Standard Single Room Only</roomtype><webService>arzooB</webService><fromdate>20/06/2017</fromdate><todate>22/06/2017</todate><roomTypeCode>0000235638</roomTypeCode><ratePlanType>0000925100</ratePlanType></hotelinfo><roomStayCandidate><guestDetails><adults>1</adults><child/></guestDetails></roomStayCandidate><ratebands><validdays>1111111</validdays><wsKey>crgAMDiElmg6eaST9cxEwA+fTZgh1nupLJ4p4u6ke0Cul6mwahX8sSyeKeLupHtAgjjDdnAxohfEOsNqkniCriJi4UCNTjhNT7k+js17KUiaT2zVTPo9tsQ6w2qSeIKuFuYUM0JK2OKOUSHMG+t1OKx13FO7mho4xDrDapJ4gq4KKdOzoa1kvQBMThrLYrRaTOCIuPXTPp95F9GTH09ruFFbmbdfUiAjxJlKOPaXnAvEOsNqkniCrsHjlG4I7MKBn4doaXhLJbPB45RuCOzCgZ+HaGl4SyWzweOUbgjswoFWp5nZ86iUG8Q6w2qSeIKumDDPerEPVIDlRsv0brlhIooNEWEKH2bBiX6Yfx2T7InULFBeohhejfflIYqwnPKw6VoVcO39l/qI1+3MTUnYIwNzeLwHbZ6/Ni2/fh6H+B5be+3ljOZXHQ==</wsKey><extGuestTotal>0</extGuestTotal><roomTotal>2800</roomTotal><servicetaxTotal>0</servicetaxTotal><discount>1167.0</discount><commission>0</commission></ratebands><guestInformation><title>Mr.</title><firstName>sammeta</firstName><middleName/><lastName>harish</lastName><phoneNumber><countryCode>91</countryCode><areaCode>01482</areaCode><number>9177780181</number><extension>1</extension></phoneNumber><email>bindu89kasera@gmail.com</email><address><addressLine>11-133,9th street,Jeevakona</addressLine><city>Tirupati</city><zipCode>517501</zipCode><state>Andhrapradesh</state><country>India</country></address><residentOfIndia>true</residentOfIndia></guestInformation></allocquery></arzHotelProvReq>";
                    responseString = hotelBooking.getHotelProvisional(searchRequestObject, string.Empty);
                    if (string.IsNullOrEmpty(responseString))
                    {
                        response = Request.CreateResponse<string>(HttpStatusCode.NotFound, "<error>No Content Found</error>");
                    }
                    else
                    {
                        response = Request.CreateResponse<string>(HttpStatusCode.OK, responseString);

                        //responseString = "<string xmlns='http://schemas.microsoft.com/2003/10/Serialization/'>&lt;arzHotelProvResp&gt; &lt;clientInfo&gt; &lt;partnerID&gt;100200&lt;/partnerID&gt; &lt;/clientInfo&gt; &lt;allocquery&gt; &lt;hotelid&gt;00012611&lt;/hotelid&gt; &lt;roomtype&gt;Standard Single Room Only&lt;/roomtype&gt; &lt;webService&gt;arzooB&lt;/webService&gt; &lt;roomTypeCode&gt;0000235638&lt;/roomTypeCode&gt; &lt;ratePlanCode&gt;0000925100&lt;/ratePlanCode&gt; &lt;fromdate&gt;20/06/2017&lt;/fromdate&gt; &lt;todate&gt;22/06/2017&lt;/todate&gt; &lt;residentOfIndia&gt;true&lt;/residentOfIndia&gt; &lt;/allocquery&gt; &lt;allocresult&gt; &lt;wsKey&gt;crgAMDiElmg6eaST9cxEwA+fTZgh1nupLJ4p4u6ke0Cul6mwahX8sSyeKeLupHtAgjjDdnAxohfEOsNqkniCriJi4UCNTjhNT7k+js17KUiaT2zVTPo9tsQ6w2qSeIKuFuYUM0JK2OKOUSHMG+t1OKx13FO7mho4xDrDapJ4gq4KKdOzoa1kvQBMThrLYrRaTOCIuPXTPp95F9GTH09ruFFbmbdfUiAjxJlKOPaXnAvEOsNqkniCrsHjlG4I7MKBn4doaXhLJbPB45RuCOzCgZ+HaGl4SyWzweOUbgjswoFWp5nZ86iUG8Q6w2qSeIKumDDPerEPVIDlRsv0brlhIooNEWEKH2bBiX6Yfx2T7InULFBeohhejfflIYqwnPKw6VoVcO39l/qI1+3MTUnYIwNzeLwHbZ6/Ni2/fh6H+B5be+3ljOZXHQ==&lt;/wsKey&gt; &lt;allocavail&gt;Y&lt;/allocavail&gt; &lt;allocid&gt;4769&lt;/allocid&gt; &lt;/allocresult&gt; &lt;/arzHotelProvResp&gt; </string>";
                        //responseString = "<string xmlns='http://schemas.microsoft.com/2003/10/Serialization/'>&lt;arzhotelprovresp&gt; &lt;clientInfo&gt; &lt;partnerID&gt;100200&lt;/partnerID&gt; &lt;/clientInfo&gt; &lt;allocquery&gt; &lt;hotelid&gt;00012611&lt;/hotelid&gt; &lt;roomtype&gt;Standard Single Room Only&lt;/roomtype&gt; &lt;webService&gt;arzooB&lt;/webService&gt; &lt;roomTypeCode&gt;0000235638&lt;/roomTypeCode&gt; &lt;ratePlanCode&gt;0000925100&lt;/ratePlanCode&gt; &lt;fromdate&gt;20/06/2017&lt;/fromdate&gt; &lt;todate&gt;22/06/2017&lt;/todate&gt; &lt;residentOfIndia&gt;true&lt;/residentOfIndia&gt; &lt;/allocquery&gt; &lt;allocresult&gt; &lt;wsKey&gt;crgAMDiElmg6eaST9cxEwA+fTZgh1nupLJ4p4u6ke0Cul6mwahX8sSyeKeLupHtAgjjDdnAxohfEOsNqkniCriJi4UCNTjhNT7k+js17KUiaT2zVTPo9tsQ6w2qSeIKuFuYUM0JK2OKOUSHMG+t1OKx13FO7mho4xDrDapJ4gq4KKdOzoa1kvQBMThrLYrRaTOCIuPXTPp95F9GTH09ruFFbmbdfUiAjxJlKOPaXnAvEOsNqkniCrsHjlG4I7MKBn4doaXhLJbPB45RuCOzCgZ+HaGl4SyWzweOUbgjswoFWp5nZ86iUG8Q6w2qSeIKumDDPerEPVIDlRsv0brlhIooNEWEKH2bBiX6Yfx2T7InULFBeohhejfflIYqwnPKw6VoVcO39l/qI1+3MTUnYIwNzeLwHbZ6/Ni2/fh6H+B5be+3ljOZXHQ==&lt;/wsKey&gt; &lt;allocavail&gt;Y&lt;/allocavail&gt; &lt;allocid&gt;4769&lt;/allocid&gt; &lt;/allocresult&gt; &lt;/arzhotelprovresp&gt;</string>";
                        //responseString = RemoveLineEndings(responseString);
                        // responseString = this.RemoveAllNamespaces(responseString);
                        var serializer1 = new XmlSerializer(typeof(ArzHotelProvResp));


                        ArzHotelProvResp hotelAvailResp;
                        using (TextReader reader1 = new StringReader(responseString))
                        {
                            hotelAvailResp = (ArzHotelProvResp)serializer1.Deserialize(reader1);
                        }

                        if (hotelAvailResp == null)
                        {
                            response = Request.CreateResponse<string>(HttpStatusCode.NotFound, "<error>No Content Found</error>");
                        }
                        else
                        {
                            XmlSerializer bookingConfirmationResponse = new XmlSerializer(typeof(ArzHotelProvResp));
                            var responseXml = "";

                            using (var sww = new StringWriter())
                            {
                                using (XmlWriter writer = XmlWriter.Create(sww))
                                {
                                    bookingConfirmationResponse.Serialize(writer, hotelAvailResp);
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

        [ActionName("BookingConfirmation")]
        public async Task<HttpResponseMessage> BookingConfirmation()
        {
            if (hepler.CheckHeader(Request))
            {
                string responseString = string.Empty;
                HttpResponseMessage response = null;
                try
                {
                    var searchParamterXml = await Request.Content.ReadAsByteArrayAsync();
                    string searchParameter = System.Text.Encoding.UTF8.GetString(searchParamterXml);
                    XmlSerializer serializer = new XmlSerializer(typeof(HotelDescriptionRequest));
                    using (TextReader reader = new StringReader(searchParameter))
                    {
                        var endTag = "</guestInformation>";
                        int startIndex = searchParameter.IndexOf("<guestInformation>");
                        int endIndex = searchParameter.IndexOf(endTag, startIndex) + endTag.Length;

                        var guestInformationXml = searchParameter.Substring(startIndex, endIndex - startIndex);

                        /******/
                        endTag = "</roomStayCandidate>";
                        startIndex = searchParameter.IndexOf("<roomStayCandidate>");
                        endIndex = searchParameter.IndexOf(endTag, startIndex) + endTag.Length;

                        var guestDetailXml = searchParameter.Substring(startIndex, endIndex - startIndex);

                        HotelDescriptionRequest searchDetail = (HotelDescriptionRequest)serializer.Deserialize(reader);

                        var searchRequestObject = HolelTemplateXml.BookingConfirmationRequestXml;
                        searchRequestObject = searchRequestObject.Replace("hotelidValue", searchDetail.hotelid).
                            Replace("webServiceValue", searchDetail.webService).
                            Replace("ratePlanTypeValue", searchDetail.RatePlanType).
                            Replace("roomTypeCodeValue", searchDetail.RoomTypeCode).
                            Replace("fromdateValue", searchDetail.CheckInDate).
                            Replace("todateValue", searchDetail.CheckOutDate).
                            Replace("cityValue", searchDetail.city).
                            Replace("fromallocationValue", searchDetail.Fromallocation).
                            Replace("allocidValue", searchDetail.Allocid).
                            Replace("roomtypeValue", searchDetail.Roomtype).
                            Replace("wsKeyValue", searchDetail.WsKey).
                            Replace("roombasisValue", searchDetail.Roombasis).
                            Replace("guestInformationXmlValue", guestInformationXml).
                            Replace("roomStayCandidateXmlValue", guestDetailXml);

                        var hotelBooking = new HotelBooking();
                        hotelBooking.Url = "http://demo.arzoo.com/HotelXML_V1.2/services/HotelBooking";
                        responseString = hotelBooking.getHotelBooking(searchRequestObject, string.Empty);
                        if (string.IsNullOrEmpty(responseString))
                        {
                            response = Request.CreateResponse<string>(HttpStatusCode.NotFound, "<error>No Content Found</error>");
                        }
                        else
                        {
                            //response = Request.CreateResponse<string>(HttpStatusCode.OK, responseString);

                            //responseString = RemoveLineEndings(responseString);
                            //responseString = this.RemoveAllNamespaces(responseString);
                            var serializer1 = new XmlSerializer(typeof(ArzHotelBookingResp));

                            ArzHotelBookingResp hotelAvailResp;
                            using (TextReader reader1 = new StringReader(responseString))
                            {
                                hotelAvailResp = (ArzHotelBookingResp)serializer1.Deserialize(reader1);
                            }

                            if (hotelAvailResp == null)
                            {
                                response = Request.CreateResponse<string>(HttpStatusCode.NotFound, "<error>No Content Found</error>");
                            }
                            else
                            {
                                XmlSerializer bookingConfirmationResponse = new XmlSerializer(typeof(ArzHotelBookingResp));
                                var responseXml = "";

                                using (var sww = new StringWriter())
                                {
                                    using (XmlWriter writer = XmlWriter.Create(sww))
                                    {
                                        bookingConfirmationResponse.Serialize(writer, hotelAvailResp);
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

        [ActionName("BookingCancellation")]
        public async Task<HttpResponseMessage> BookingCancellation()
        {
            if (hepler.CheckHeader(Request))
            {
                string responseString = string.Empty;
                HttpResponseMessage response = null;
                try
                {
                    var searchParamterXml = await Request.Content.ReadAsByteArrayAsync();
                    string searchParameter = System.Text.Encoding.UTF8.GetString(searchParamterXml);
                    XmlSerializer serializer = new XmlSerializer(typeof(HotelDescriptionRequest));
                    using (TextReader reader = new StringReader(searchParameter))
                    {
                        HotelDescriptionRequest searchDetail = (HotelDescriptionRequest)serializer.Deserialize(reader);

                        var searchRequestObject = HolelTemplateXml.CancelRequestXml;
                        searchRequestObject = searchRequestObject.Replace("emailValue", searchDetail.Email).
                            Replace("lastNameValue", searchDetail.LastName).
                            Replace("bookingrefValue", searchDetail.Bookingref).
                            Replace("webServiceValue", searchDetail.webService);

                        var hotelCancel = new HotelCancellation();
                        hotelCancel.Url = "http://demo.arzoo.com/HotelXML_V1.2/services/HotelCancellation";
                        responseString = hotelCancel.getHotelCancellation(searchRequestObject, string.Empty);
                        if (string.IsNullOrEmpty(responseString))
                        {
                            response = Request.CreateResponse<string>(HttpStatusCode.NotFound, "<error>No Content Found</error>");
                        }
                        else
                        {
                            //responseString = "<string xmlns='http://schemas.microsoft.com/2003/10/Serialization/'>&lt;arzHotelCancellationRes&gt; &lt;clientInfo&gt; &lt;partnerID&gt;100200&lt;/partnerID&gt; &lt;/clientInfo&gt; &lt;cancellationinfo&gt; &lt;email&gt;bindu89kasera@gmail.com&lt;/email&gt; &lt;lastName&gt;harish&lt;/lastName&gt; &lt;cancellationId&gt;TGU00002448&lt;/cancellationId&gt; &lt;webService&gt;arzooB&lt;/webService&gt; &lt;currency&gt;INR&lt;/currency&gt; &lt;refundTotalAmount&gt;0&lt;/refundTotalAmount&gt; &lt;success&gt;Success&lt;/success&gt; &lt;/cancellationinfo&gt; &lt;/arzHotelCancellationRes&gt;</string>";
                            // response = Request.CreateResponse<string>(HttpStatusCode.OK, responseString);

                            //responseString = RemoveLineEndings(responseString);
                            //responseString = this.RemoveAllNamespaces(responseString);
                            var serializer1 = new XmlSerializer(typeof(ArzHotelCancellationRes));

                            ArzHotelCancellationRes hotelAvailResp;
                            using (TextReader reader1 = new StringReader(responseString))
                            {
                                hotelAvailResp = (ArzHotelCancellationRes)serializer1.Deserialize(reader1);
                            }

                            if (hotelAvailResp == null)
                            {
                                response = Request.CreateResponse<string>(HttpStatusCode.NotFound, "<error>No Content Found</error>");
                            }
                            else
                            {
                                XmlSerializer bookingConfirmationResponse = new XmlSerializer(typeof(ArzHotelCancellationRes));
                                var responseXml = "";

                                using (var sww = new StringWriter())
                                {
                                    using (XmlWriter writer = XmlWriter.Create(sww))
                                    {
                                        bookingConfirmationResponse.Serialize(writer, hotelAvailResp);
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

        //Implemented based on interface, not part of algorithm
        public string RemoveAllNamespaces(string xmlDocument)
        {
            XElement xmlDocumentWithoutNs = RemoveAllNamespaces(XElement.Parse(xmlDocument));

            return xmlDocumentWithoutNs.Value;
        }

        //Implemented based on interface, not part of algorithm
        public string RemoveAllNamespacesTest(string xmlDocument)
        {
            XElement xmlDocumentWithoutNs = RemoveAllNamespaces(XElement.Parse(xmlDocument));

            return xmlDocumentWithoutNs.ToString();
        }

        //Core recursion function
        private XElement RemoveAllNamespaces(XElement xmlDocument)
        {
            if (!xmlDocument.HasElements)
            {
                XElement xElement = new XElement(xmlDocument.Name.LocalName);
                xElement.Value = xmlDocument.Value;

                foreach (XAttribute attribute in xmlDocument.Attributes())
                    xElement.Add(attribute);

                return xElement;
            }
            return new XElement(xmlDocument.Name.LocalName, xmlDocument.Elements().Select(el => RemoveAllNamespaces(el)));
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

        // Get api/values
        [ActionName("GetHotelCityList")]
        public IHttpActionResult GetHotelCityList()
        {
            var cities = this.AssignDomesticCities();
            return Content(HttpStatusCode.OK, cities, Configuration.Formatters.XmlFormatter);
        }

        public IList<KeyValuePair> AssignDomesticCities()
        {
            IList<KeyValuePair> cityList = new List<KeyValuePair>();
            cityList.Add(new KeyValuePair { Id = "Abhiramapuram", Value = "Abhiramapuram" });
            cityList.Add(new KeyValuePair { Id = "New Delhi", Value = "New Delhi" });
            cityList.Add(new KeyValuePair { Id = "Lucknow", Value = "Lucknow" });
            cityList.Add(new KeyValuePair { Id = "Mcleodganj", Value = "Mcleodganj" });
            return cityList;
        }
    }
}