using Newtonsoft.Json;
using ShineYatraApi.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace ShineYatraApi.Controllers
{
    public class HttpHelper
    {
        public bool CheckHeader(HttpRequestMessage request)
        {
            var authKey = string.Empty;
            if (request.Headers.Contains("authKey"))
            {
                authKey = request.Headers.GetValues("authKey").First();
            }

            if (string.IsNullOrEmpty(authKey))
            {
                return false;
            }

            if (authKey.Trim() == ConfigurationManager.AppSettings["authKey"])
            {
                return true;
            }

            return false;
        }

        public static async Task<Response> FetchLoginAPI(LoginModel loginDetail)
        {
            var responseDetail = new Response();
            if (loginDetail == null || string.IsNullOrEmpty(loginDetail.username) || string.IsNullOrEmpty(loginDetail.password))
            {
                responseDetail.ResponseValue = "Please send Username or Password";
            }
            else
            {
                var values = new Dictionary<string, string>();
                values.Add("token", "abfYkr54678prlAew56129PklE8426");
                values.Add("Username", loginDetail.username);
                values.Add("Password", loginDetail.password);
                await CallLoginAPi(responseDetail, values);
            }
            return responseDetail;
        }

        private static async Task CallLoginAPi(Response responseDetail, Dictionary<string, string> values)
        {
            using (var httpClient = new HttpClient())
            {
                var content = new FormUrlEncodedContent(values);
                try
                {
                    var httpResponse = await httpClient.PostAsync("http://dreamtouchglobal.com/CheckLogin.aspx", content);
                    if (httpResponse.Content != null)
                    {
                        var responseContent = await httpResponse.Content.ReadAsStringAsync();
                        var userDetail = JsonConvert.DeserializeObject<DTUserDetails>(responseContent);
                        if (userDetail == null)
                        {
                            responseDetail.ResponseValue = "User not found.";
                        }
                        else if (userDetail.status != null && userDetail.status.Contains("FAIL"))
                        {
                            responseDetail.ResponseValue = "Status Fail";
                        }
                        else
                        {
                            responseDetail.Status = true;
                            responseDetail.ResponseValue = new JavaScriptSerializer().Serialize(userDetail);
                        }
                    }
                }
                catch (Exception e)
                {
                    responseDetail.ResponseValue = e.Message;
                }
            }
        }

        public static async Task<Response> GetWalletAmount(LoginModel loginDetail)
        {
            var responseDetail = new Response();
            if (loginDetail == null || string.IsNullOrEmpty(loginDetail.username) || string.IsNullOrEmpty(loginDetail.password))
            {
                responseDetail.ResponseValue = "Please send Username or Password";
            }
            else
            {
                var values = new Dictionary<string, string>();
                values.Add("token", "abfYkr54678prlAew56129PklE8426");
                values.Add("Username", loginDetail.username);
                values.Add("Password", loginDetail.password);
                values.Add("action", "checkswallet");
                await CallLoginAPi(responseDetail, values);
            }
            return responseDetail;
        }

        public static async Task<Response> DeductAmount(LoginModel loginDetail)
        {
            var responseDetail = new Response();
            if (loginDetail == null || string.IsNullOrEmpty(loginDetail.username) || string.IsNullOrEmpty(loginDetail.password))
            {
                responseDetail.ResponseValue = "Please send Username or Password";
            }
            else
            {

                var values = new Dictionary<string, string>();
                values.Add("token", "abfYkr54678prlAew56129PklE8426");
                values.Add("Username", loginDetail.username);
                values.Add("Password", loginDetail.password);
                values.Add("TxnData", "" + loginDetail.txnData + ";" + loginDetail.amount + ";DealPortal");
                //values.Add("TxnData", "20160829182027547;" + Amount + ";DealPortal");
                values.Add("action", "deductswallet");
                await CallLoginAPi(responseDetail, values);
            }
            return responseDetail;
        }

        public static async Task<Response> WalletDeductConfirmation(LoginModel loginDetail)
        {
            var responseDetail = new Response();
            if (loginDetail == null || string.IsNullOrEmpty(loginDetail.username) || string.IsNullOrEmpty(loginDetail.password))
            {
                responseDetail.ResponseValue = "Please send Username or Password";
            }
            else
            {
                var values = new Dictionary<string, string>();
                values.Add("token", "abfYkr54678prlAew56129PklE8426");
                values.Add("Username", loginDetail.username);
                values.Add("Password", loginDetail.password);
                values.Add("voucherno", loginDetail.voucherNo);
                values.Add("Response", "OK");
                //values.Add("TxnData", "20160829182027547;" + Amount + ";DealPortal");
                values.Add("action", "checkvoucher");
                await CallLoginAPi(responseDetail, values);
            }
            return responseDetail;
        }
    }
}