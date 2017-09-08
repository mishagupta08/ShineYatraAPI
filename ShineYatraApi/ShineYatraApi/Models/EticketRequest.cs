using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShineYatraApi.Models
{
    public class EticketRequest
    {
        public string transid { get; set; }

        public string partnerRefId { get; set; }

        public string CancellationId { get; set; }
    }
}