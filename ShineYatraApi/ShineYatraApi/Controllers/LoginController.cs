using Newtonsoft.Json;
using ShineYatraApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace ShineYatraApi.Controllers
{
    public class LoginController : ApiController
    {
        // Post api/values
        [HttpPost, Route("api/Login/ValidateUser")]
        public async Task<IHttpActionResult> ValidateUser()
        {
            var detail = await Request.Content.ReadAsStringAsync();
            var loginDetail = JsonConvert.DeserializeObject<LoginModel>(detail);

            var result = await HttpHelper.FetchLoginAPI(loginDetail);
            return Content(HttpStatusCode.OK, result, Configuration.Formatters.JsonFormatter);
        }

        // Post api/values
        [HttpPost, Route("api/Login/GetWalletAmount")]
        public async Task<IHttpActionResult> GetWalletAmount()
        {
            var detail = await Request.Content.ReadAsStringAsync();
            var loginDetail = JsonConvert.DeserializeObject<LoginModel>(detail);
            var result = await HttpHelper.GetWalletAmount(loginDetail);
            return Content(HttpStatusCode.OK, result, Configuration.Formatters.JsonFormatter);
        }

        // Post api/values
        [HttpPost, Route("api/Login/DeductAmount")]
        public async Task<IHttpActionResult> DeductAmount()
        {
            var detail = await Request.Content.ReadAsStringAsync();
            var loginDetail = JsonConvert.DeserializeObject<LoginModel>(detail);
            var result = await HttpHelper.DeductAmount(loginDetail);
            return Content(HttpStatusCode.OK, result, Configuration.Formatters.JsonFormatter);
        }

        // Post api/values
        [HttpPost, Route("api/Login/WalletDeductConfirmation")]
        public async Task<IHttpActionResult> WalletDeductConfirmation()
        {
            var detail = await Request.Content.ReadAsStringAsync();
            var loginDetail = JsonConvert.DeserializeObject<LoginModel>(detail);
            var result = await HttpHelper.WalletDeductConfirmation(loginDetail);
            return Content(HttpStatusCode.OK, result, Configuration.Formatters.JsonFormatter);
        }
    }
}