using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShineYatraApi.Models
{
    public class LoginModel
    {
        /// <summary>
        /// Gets or sets username
        /// </summary>
        public string username { get; set; }

        /// <summary>
        /// gets or sets password
        /// </summary>
        public string password { get; set; }

        /// <summary>
        /// Gets or sets txnData
        /// </summary>
        public string txnData { get; set; }

        /// <summary>
        /// gets or sets voucherNo
        /// </summary>
        public string voucherNo { get; set; }

        /// <summary>
        /// gets or sets amount
        /// </summary>
        public string amount { get; set; }
    }
}