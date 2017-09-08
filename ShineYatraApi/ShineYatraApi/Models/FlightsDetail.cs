using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShineYatraApi.Models
{
    public class FlightsDetail
    {
        /// <summary>
        /// gets or sets id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// gets or sets ArrivalAirportCode
        /// </summary>
        public string ArrivalAirportCode { get; set; }

        /// <summary>
        /// gets or sets ArrivalDateTime
        /// </summary>
        public string ArrivalDateTime { get; set; }

        /// <summary>
        /// gets or sets DepartureAirportCode
        /// </summary>
        public string DepartureAirportCode { get; set; }

        /// <summary>
        /// gets or sets DepartureDateTime
        /// </summary>
        public string DepartureDateTime { get; set; }

        /// <summary>
        /// gets or sets FlightNumber
        /// </summary>
        public string FlightNumber { get; set; }

        /// <summary>
        /// gets or sets OperatingAirlineCode
        /// </summary>
        public string OperatingAirlineCode { get; set; }

        /// <summary>
        /// gets or sets StopQuantity
        /// </summary>
        public string StopQuantity { get; set; }

        /// <summary>
        /// gets or sets ImageFileName
        /// </summary>
        public string ImageFileName { get; set; }

        /// <summary>
        /// gets or sets Availability
        /// </summary>
        public string Availability { get; set; }

        /// <summary>
        /// gets or sets AirLineName
        /// </summary>
        public string AirLineName { get; set; }

        /// <summary>
        /// gets or sets IsReturnFlight flag
        /// </summary>
        public bool IsReturnFlight { get; set; }

        /// <summary>
        /// gets or sets BookingClassFare
        /// </summary>
        public BookingClassFare BookingClassFare { get; set; }

        /// <summary>
        /// holds international flight fare
        /// </summary>
        public BookingClassFareInt BookingClassFareInt { get; set; }
    }
}