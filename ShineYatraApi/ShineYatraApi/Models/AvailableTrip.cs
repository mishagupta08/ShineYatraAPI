using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShineYatraApi.Models
{
    public class AvailableTrip
    {
        private string arrivalTime;

        public string ArrivalTime
        {
            get { return arrivalTime; }
            set { arrivalTime = value; }
        }
        private string availableSeats;
        public string AvailableSeats
        {
            get { return availableSeats; }
            set { availableSeats = value; }
        }
        private object boardingTimes;
        public object BoardingTimes
        {
            get { return boardingTimes; }
            set { boardingTimes = value; }
        }
        //public string busType { get; set; }
        private string busType;
        public string BusType
        {
            get { return busType; }
            set { busType = value; }
        }
        //public string cancellationPolicy { get; set; }
        private string cancellationPolicy;
        public string CancellationPolicy
        {
            get { return cancellationPolicy; }
            set { cancellationPolicy = value; }
        }
        //public string departureTime { get; set; }
        private string departureTime;
        public string DepartureTime
        {
            get { return departureTime; }
            set { departureTime = value; }
        }
        //public object droppingTimes { get; set; }
        private object fares;
        public object Fares
        {
            get { return fares; }
            set { fares = value; }
        }
        //public object fares { get; set; }
        private object droppingTimes;
        public object DroppingTimes
        {
            get { return droppingTimes; }
            set { droppingTimes = value; }
        }
        //public string id { get; set; }
        private string id;
        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        //public string partialCancellationAllowed { get; set; }
        private string partialCancellationAllowed;
        public string PartialCancellationAllowed
        {
            get { return partialCancellationAllowed; }
            set { partialCancellationAllowed = value; }
        }
        //public string travels { get; set; }
        private string travels;
        public string Travels
        {
            get { return travels; }
            set { travels = value; }
        }
    }

    public class AvailableTripList
    {
        //public List<AvailableTrip> availableTrips { get; set; }
        private List<AvailableTrip> availableTrips;
        public List<AvailableTrip> AvailableTrips
        {
            get { return availableTrips; }
            set { availableTrips = value; }
        }
    }
}
