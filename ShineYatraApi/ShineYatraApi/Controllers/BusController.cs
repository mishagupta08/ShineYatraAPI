using Newtonsoft.Json;
using ShineYatraApi.Client;
using ShineYatraApi.Models;
using System.Data;
using System.Web.Http;
using System.Xml;

namespace ShineYatraApi.Controllers
{
    public class BusController : ApiController
    {
        private static string baseUrl = "http://api.seatseller.travel";
        private static string consumerKey = "xyz";
        private static string consumerSecret = "xyz";

        SSAPIClient client = new SSAPIClient(baseUrl, consumerKey, consumerSecret);

        // POST api/values
        [ActionName("GetAllCities")]
        public void GetAllCities()
        {
            var cityList = client.getAllSources();
        }

        [ActionName("GetAllDestinations")]
        public void GetAllDestinations(string source)
        {
            var cityList = client.getAllDestinations(source);
        }

        [ActionName("GetAvailableTrips")]
        public void GetAvailableTrips(string source, string destination, string doj)
        {
            var searchResult = client.getAvailableTrips(source, destination, doj);
        }

        [ActionName("GetTripDetails")]
        public void GetTripDetails(string tripId)
        {
            var searchResult = client.getTripDetails(tripId);
        }

        [ActionName("BlockTicket")]
        public void BlockTicket(string source, string destination, string doj)
        {
            DataSet availabilityDataSet = convertJsonStringToDataSet(client.getAvailableTrips(source, destination, doj));
            string availableTrip_Id = getFirstTripId(source, destination, doj);
            object bordingpoint_id = availabilityDataSet.Tables[1].Rows[0][0];
            string makeBlockRequest = "{\"availableTripId\":\"" + availableTrip_Id + "\",\"boardingPointId\":\"" + bordingpoint_id + "\",\"destination\":\"" + destination + "\",\"inventoryItems\":[{\"fare\":\" 400.00\",\"ladiesSeat\":\"false\",\"passenger\":{\"address\":\"some address\",\"age\":\"26\",\"email\":\"imtiazk4u@gmail.com\",\"gender\":\"MALE\",\"idNumber\":\"ID123\",\"idType\":\"PAN_CARD\",\"mobile\":\"8892311531\",\"name\":\"Imtiaz\",\"primary\":\"true\",\"title\":\"Mr\"},\"seatName\":\"12\"},{\"fare\":\"400.00\",\"ladiesSeat\":\"false\",\"passenger\":{\"age\":\"26\",\"email\":\"alikhank4u@gmail.com\",\"gender\":\"MALE\",\"mobile\":\"8892311531\",\"name\":\"Imdad\",\"primary\":\"false\",\"title\":\"Mr\"},\"seatName\":\"13\"}],\"source\":\"" + source + "\"}";
            var result = client.blockTicket(makeBlockRequest);
        }

        public string getFirstTripId(string source, string destination, string doj)
        {
            string getAvailablityString = client.getAvailableTrips(source, destination, doj);
            AvailableTripList deserializedGetAvailablityString = JsonConvert.DeserializeObject<AvailableTripList>(getAvailablityString);
            string firstTripId = deserializedGetAvailablityString.AvailableTrips[0].Id.ToString();

            return firstTripId;
        }

        private DataSet convertJsonStringToDataSet(string jsonString)
        {
            XmlDocument xd = new XmlDocument();
            jsonString = "{ \"rootNode\": {" + jsonString.Trim().TrimStart('{').TrimEnd('}') + "} }";
            xd = (XmlDocument)JsonConvert.DeserializeXmlNode(jsonString);
            DataSet ds = new DataSet();
            ds.ReadXml(new XmlNodeReader(xd));
            return ds;
        }

        [ActionName("GetTicket")]
        public void GetTicket(string tickeNo)
        {
            var searchResult = client.getTicket(tickeNo);
        }

        [ActionName("CancelTicket ")]
        public void CancelTicket(string tickeNo, string seatName)
        {
            string makeCancellationRequest = "{\"tin\":\"" + tickeNo + "\",\"seatsToCancel\":[\"" + seatName + "\"]}";
            var result = client.cancelTicket(makeCancellationRequest);
        }
    }
}