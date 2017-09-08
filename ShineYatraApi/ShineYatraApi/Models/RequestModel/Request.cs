using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ShineYatraApi.Model
{
    [XmlRoot(ElementName = "Request")]
    public class Request
    {
        /// <summary>
        /// gets or sets origin
        /// </summary>
        [XmlElement("Origin")]
        public string Origin { get; set; }

        /// <summary>
        /// gets or sets destination
        /// </summary>
        public string Destination { get; set; }

        /// <summary>
        /// gets or sets depart date
        /// </summary>
        public string DepartDate { get; set; }

        /// <summary>
        /// gets or sets Return date
        /// </summary>
        public string ReturnDate { get; set; }

        /// <summary>
        /// gets or sets Adult count
        /// </summary>
        public string AdultPax { get; set; }

        /// <summary>
        /// gets or sets Child date
        /// </summary>
        public string ChildPax { get; set; }

        /// <summary>
        /// gets or sets Infant Count
        /// </summary>
        public string InfantPax { get; set; }

        /// <summary>
        /// gets or sets mode
        /// </summary>
        public string mode { get; set; }

        /// <summary>
        /// gets or sets mode
        /// </summary>
        public string Preferredclass { get; set; }

        /// <summary>
        /// gets or sets id
        /// </summary>
        public string Id { get; set; }

        public string PartnerRefId { get; set; }

        public string FlightNumber { get; set; }

        public string creditcardno { get; set; }

        public string phoneNumber { get; set; }

        public string emailAddress { get; set; }

        [XmlElement(ElementName = "personName")]
        public personName personName { get; set; }
    }

    [XmlRoot(ElementName = "CustomerInfo")]
    public class CustomerInfo
    {
        [XmlElement(ElementName = "givenName")]
        public string givenName { get; set; }

        [XmlElement(ElementName = "surName")]
        public string surName { get; set; }

        [XmlElement(ElementName = "nameReference")]
        public string nameReference { get; set; }

        [XmlElement(ElementName = "psgrtype")]
        public string psgrtype { get; set; }
    }

    [XmlRoot(ElementName = "personName")]
    public class personName
    {
        [XmlElement(ElementName = "CustomerInfo")]
        public List<CustomerInfo> CustomerInfo { get; set; }
    }
}