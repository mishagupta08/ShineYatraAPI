using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;

namespace ShineYatraApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute("GetCityList", "Api/Flight/GetCityList", new { action = "GetCityList" }, new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) });

            config.Routes.MapHttpRoute("SearchFlight", "Api/Flight/SearchFlight", new { action = "SearchFlight" }, new { httpMethod = new HttpMethodConstraint(HttpMethod.Post) });

            config.Routes.MapHttpRoute("FlightPricing", "Api/Flight/FlightPricing", new { action = "FlightPricing" }, new { httpMethod = new HttpMethodConstraint(HttpMethod.Post) });

            config.Routes.MapHttpRoute("BookTicket", "Api/Flight/BookTicket", new { action = "BookTicket" }, new { httpMethod = new HttpMethodConstraint(HttpMethod.Post) });

            config.Routes.MapHttpRoute("BookingStatus", "Api/Flight/BookingStatus", new { action = "BookingStatus" }, new { httpMethod = new HttpMethodConstraint(HttpMethod.Post) });

            config.Routes.MapHttpRoute("CancelRequest", "Api/Flight/CancelRequest", new { action = "CancelRequest" }, new { httpMethod = new HttpMethodConstraint(HttpMethod.Post) });

            config.Routes.MapHttpRoute("CancelRequestStatus", "Api/Flight/CancelRequestStatus", new { action = "CancelRequestStatus" }, new { httpMethod = new HttpMethodConstraint(HttpMethod.Post) });

            /*****Hotel Api Path*****/
            config.Routes.MapHttpRoute("SearchHotel", "Api/Hotel/SearchHotel", new { action = "SearchHotel" }, new { httpMethod = new HttpMethodConstraint(HttpMethod.Post) });

            config.Routes.MapHttpRoute("SearchHotelDescription", "Api/Hotel/SearchHotelDescription", new { action = "SearchHotelDescription" }, new { httpMethod = new HttpMethodConstraint(HttpMethod.Post) });

            config.Routes.MapHttpRoute("HotelPolicy", "Api/Hotel/HotelPolicy", new { action = "HotelPolicy" }, new { httpMethod = new HttpMethodConstraint(HttpMethod.Post) });

            config.Routes.MapHttpRoute("ProvisionalBooking", "Api/Hotel/ProvisionalBooking", new { action = "ProvisionalBooking" }, new { httpMethod = new HttpMethodConstraint(HttpMethod.Post) });

            config.Routes.MapHttpRoute("BookingConfirmation", "Api/Hotel/BookingConfirmation", new { action = "BookingConfirmation" }, new { httpMethod = new HttpMethodConstraint(HttpMethod.Post) });

            config.Routes.MapHttpRoute("BookingCancellation", "Api/Hotel/BookingCancellation", new { action = "BookingCancellation" }, new { httpMethod = new HttpMethodConstraint(HttpMethod.Post) });

            config.Routes.MapHttpRoute("GetHotelCityList", "Api/Hotel/GetHotelCityList", new { action = "GetHotelCityList" }, new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) });

            config.Routes.MapHttpRoute("SearchHotelWithDetail", "Api/Hotel/SearchHotelWithDetail", new { action = "GetHotelCityList" }, new { httpMethod = new HttpMethodConstraint(HttpMethod.Post) }); 

            /******International Flight Api******/

            config.Routes.MapHttpRoute("SearchInternationalFlight", "Api/Hotel/SearchInternationalFlight", new { action = "SearchInternationalFlight" }, new { httpMethod = new HttpMethodConstraint(HttpMethod.Post) });

            /*****Recharge Api Path*****/
            config.Routes.MapHttpRoute("Services", "Api/Recharge/Services", new { action = "Services" }, new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) });

            config.Routes.MapHttpRoute("ValidateTransaction", "Api/Recharge/ValidateTransaction", new { action = "ValidateTransaction" }, new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) });

            config.Routes.MapHttpRoute("Transaction", "Api/Recharge/Transaction", new { action = "Transaction" }, new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) });

            config.Routes.MapHttpRoute("CheckTransactionStatus", "Api/Recharge/CheckTransactionStatus", new { action = "CheckTransactionStatus" }, new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) });

            config.Routes.MapHttpRoute("GetWalletBalance", "Api/Recharge/GetWalletBalance", new { action = "GetWalletBalance" }, new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) });

            /*****Red Bus Api Path*****/
            config.Routes.MapHttpRoute("GetAllCities", "Api/Bus/GetAllCities", new { action = "GetAllCities" }, new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) });

            config.Routes.MapHttpRoute("GetAllDestinations", "Api/Bus/GetAllDestinations", new { action = "GetAllDestinations" }, new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) });

            config.Routes.MapHttpRoute("GetAvailableTrips", "Api/Bus/GetAvailableTrips", new { action = "GetAvailableTrips" }, new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) });

            config.Routes.MapHttpRoute("GetTripDetails", "Api/Bus/GetTripDetails", new { action = "GetTripDetails" }, new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) });

            config.Routes.MapHttpRoute("BlockTicket", "Api/Bus/BlockTicket", new { action = "BlockTicket" }, new { httpMethod = new HttpMethodConstraint(HttpMethod.Post) });

            config.Routes.MapHttpRoute("GetTicket", "Api/Bus/GetTicket", new { action = "GetTicket" }, new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) });

            config.Routes.MapHttpRoute("CancelTicket", "Api/Bus/CancelTicket", new { action = "CancelTicket" }, new { httpMethod = new HttpMethodConstraint(HttpMethod.Post) });
        }
    }
}
