using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShineYatraApi.Models
{
    public class origindestinationoption
    {
        public List<FlightsDetail> FlightsDetailList { get; set; }

        public FareDetails FareDetail { get; set; }
    }
}