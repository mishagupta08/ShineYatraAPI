using ShineYatraApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Xml;
using System.Xml.Serialization;
using System.Linq;

namespace ShineYatraApi.Controllers
{
    public class RechargeController : ApiController
    {
        /// <summary>
        /// helper object
        /// </summary>
        HttpHelper hepler = new HttpHelper();

        /// <summary>
        /// Holds xml Templates 
        /// </summary>
        RechargeTemplate RechargeTemplate = new RechargeTemplate();

        // POST api/values
        [ActionName("PrepaidRecharge")]
        public async Task<HttpResponseMessage> PrepaidRecharge()
        {
            string responseString = string.Empty;
            HttpResponseMessage response = null;
            if (hepler.CheckHeader(Request))
            {
                try
                {
                    var searchParamterXml = await Request.Content.ReadAsByteArrayAsync();
                    string searchParameter = Encoding.UTF8.GetString(searchParamterXml);
                    XmlSerializer serializer = new XmlSerializer(typeof(RechargeRequest));
                    using (TextReader reader = new StringReader(searchParameter))
                    {
                        RechargeRequest rechargeRequest = (RechargeRequest)serializer.Deserialize(reader);

                        var rechargeObject = RechargeTemplate.PrepaidRechargeValues;
                        rechargeObject = rechargeObject.Replace("productValue", rechargeRequest.ProductId).
                            Replace("MobileNumberValue", rechargeRequest.MobileNumber).
                            Replace("AmountValue", rechargeRequest.Amount).
                            Replace("RequestIdValue", rechargeRequest.RequestId);

                        //// Code to get Cancel flight status*** Start
                        using (var httpClient = new HttpClient())
                        {
                            var httpContent = new StringContent(rechargeObject, Encoding.UTF8, "text/json");
                            var requestString = "http://103.29.232.110:8089/Ezypaywebservice/PushRequest.aspx";
                            // Do the actual request and await the response
                            var httpResponse = await httpClient.PostAsync(requestString, httpContent);

                            // If the response contains content we want to read it!
                            if (httpResponse.Content != null)
                            {
                                responseString = await httpResponse.Content.ReadAsStringAsync();
                            }

                            response = Request.CreateResponse<string>(HttpStatusCode.OK, responseString);
                        }

                        /*******Extract repsonse oject from xml******/
                    }
                }
                catch (Exception e)
                {
                    response = Request.CreateResponse<string>(HttpStatusCode.BadRequest, e.Message);
                }
            }
            return response;
        }

        // POST api/values
        [ActionName("PostpaidRecharge")]
        public async Task<HttpResponseMessage> PostpaidRecharge()
        {
            if (hepler.CheckHeader(Request))
            {
                string responseString = string.Empty;
                HttpResponseMessage response = null;
                try
                {
                    var searchParamterXml = await Request.Content.ReadAsByteArrayAsync();
                    string searchParameter = System.Text.Encoding.UTF8.GetString(searchParamterXml);
                    XmlSerializer serializer = new XmlSerializer(typeof(RechargeRequest));
                    using (TextReader reader = new StringReader(searchParameter))
                    {
                        RechargeRequest rechargeRequest = (RechargeRequest)serializer.Deserialize(reader);

                        var rechargeObject = RechargeTemplate.PostpaidRechargeValues;
                        rechargeObject = rechargeObject.Replace("productValue", rechargeRequest.ProductId).
                            Replace("MobileNumberValue", rechargeRequest.MobileNumber).
                            Replace("AmountValue", rechargeRequest.Amount).
                            Replace("RequestIdValue", rechargeRequest.RequestId);

                        //// Code to get Cancel flight status*** Start
                        using (var httpClient = new HttpClient())
                        {
                            var httpContent = new StringContent(rechargeObject, Encoding.UTF8, "text/json");
                            var requestString = "http://103.29.232.110:8089/Ezypaywebservice/postpaidpush.aspx?";
                            // Do the actual request and await the response
                            var httpResponse = await httpClient.PostAsync(requestString, httpContent);

                            // If the response contains content we want to read it!
                            if (httpResponse.Content != null)
                            {
                                responseString = await httpResponse.Content.ReadAsStringAsync();
                            }
                        }

                        /*******Extract repsonse oject from xml******/
                    }
                }
                catch (Exception e)
                {
                    return null;
                }
            }
            return null;
        }

        // POST api/values
        [ActionName("Services")]
        public async Task<HttpResponseMessage> Services()
        {
            string responseString = string.Empty;
            HttpResponseMessage response = null;
            if (hepler.CheckHeader(Request))
            {
                try
                {
                    var searchParamterXml = await Request.Content.ReadAsByteArrayAsync();
                    string searchParameter = Encoding.UTF8.GetString(searchParamterXml);
                    XmlSerializer serializer = new XmlSerializer(typeof(ServicesRequest));
                    using (TextReader reader = new StringReader(searchParameter))
                    {
                        ServicesRequest rechargeRequest = (ServicesRequest)serializer.Deserialize(reader);

                        var rechargeObject = RechargeTemplate.ServiceValues;
                        rechargeObject = rechargeObject.Replace("typeValue", rechargeRequest.type);

                        //rechargeObject = "?token=63a3c58a814a9fe4e684246673b72027&type=PREPAID";
                        // Code to get Cancel flight status*** Start
                        using (var httpClient = new HttpClient())
                        {
                          //  var httpContent = new StringContent(rechargeObject, Encoding.UTF8, "application/json");
                            //var requestString = "https://www.instantpay.in/ws/api/serviceproviders";
                            ///Do the actual request and await the response
                            var httpResponse = await httpClient.GetAsync(rechargeObject);

                            ///If the response contains content we want to read it!
                            if (httpResponse.Content != null)
                            {
                                responseString = await httpResponse.Content.ReadAsStringAsync();
                            }

                            response = Request.CreateResponse<string>(HttpStatusCode.OK, responseString);
                        }

                        /*******Extract repsonse oject from xml******/

                        //if (response != null)
                        //{
                        //    var serializer1 = new XmlSerializer(typeof(Xml));

                        //    Xml serviceResponse;
                        //    using (TextReader reader1 = new StringReader(responseString))
                        //    {
                        //        serviceResponse = (Xml)serializer1.Deserialize(reader1);
                        //    }

                        //    if (serviceResponse == null)
                        //    {
                        //        response = Request.CreateResponse<string>(HttpStatusCode.NoContent, "No Content Found");
                        //    }
                        //    else
                        //    {
                        //        XmlSerializer cancelationRequest = new XmlSerializer(typeof(Xml));
                        //        var responseXml = "";

                        //        using (var sww = new StringWriter())
                        //        {
                        //            using (XmlWriter writer = XmlWriter.Create(sww))
                        //            {
                        //                cancelationRequest.Serialize(writer, serviceResponse);
                        //                responseXml = sww.ToString(); // Your XML
                        //            }
                        //        }

                        //        response = Request.CreateResponse<string>(HttpStatusCode.OK, responseXml);
                        //    }
                       // }
                    }
                }
                catch (Exception e)
                {
                    response = Request.CreateResponse<string>(HttpStatusCode.NoContent, e.Message);
                }
            }

            return response;
        }

        // POST api/values
        [ActionName("ValidateTransaction")]
        public async Task<HttpResponseMessage> ValidateTransaction()
        {
            string responseString = string.Empty;
            HttpResponseMessage response = null;
            if (hepler.CheckHeader(Request))
            {
                try
                {
                    var searchParamterXml = await Request.Content.ReadAsByteArrayAsync();
                    string searchParameter = Encoding.UTF8.GetString(searchParamterXml);
                    XmlSerializer serializer = new XmlSerializer(typeof(ServicesRequest));
                    using (TextReader reader = new StringReader(searchParameter))
                    {
                        ServicesRequest rechargeRequest = (ServicesRequest)serializer.Deserialize(reader);

                        var rechargeObject = RechargeTemplate.TransactionValidationJson;
                        rechargeObject = rechargeObject.Replace("spkeyValue", rechargeRequest.spkey).
                            Replace("agentidValue", rechargeRequest.agentid).
                            Replace("accountValue", rechargeRequest.account).
                            Replace("amountValue", rechargeRequest.amount);

                        // Code to get Cancel flight status*** Start
                        using (var httpClient = new HttpClient())
                        {
                            //var httpContent = new StringContent(rechargeObject, Encoding.UTF8, "text/xml");
                            //var requestString = ("https://www.instantpay.in/ws/api/transaction" + rechargeObject);
                            //var requestString = "https://www.instantpay.in/ws/api/transaction?token=63a3c58a814a9fe4e684246673b72027&mode=VALIDATE&spkey=ACP&agentid=190620171120115451215&account=9829221803&amount=10&optional1=AircelRechargeTest&format=json";
                            ///Do the actual request and await the response

                            var httpResponse = await httpClient.GetAsync(rechargeObject);

                            ///If the response contains content we want to read it!
                            if (httpResponse.Content != null)
                            {
                                responseString = await httpResponse.Content.ReadAsStringAsync();
                            }

                            response = Request.CreateResponse<string>(HttpStatusCode.OK, responseString);

                        }

                        /*******Extract repsonse oject from xml******/
                    }
                }
                catch (Exception e)
                {
                    response = Request.CreateResponse<string>(HttpStatusCode.BadRequest, e.Message);
                }
            }

            return response;
        }

        // POST api/values
        [ActionName("Transaction")]
        public async Task<HttpResponseMessage> Transaction()
        {
            string responseString = string.Empty;
            HttpResponseMessage response = null;
            if (hepler.CheckHeader(Request))
            {
                try
                {
                    var searchParamterXml = await Request.Content.ReadAsByteArrayAsync();
                    string searchParameter = Encoding.UTF8.GetString(searchParamterXml);
                    XmlSerializer serializer = new XmlSerializer(typeof(ServicesRequest));
                    using (TextReader reader = new StringReader(searchParameter))
                    {
                        ServicesRequest rechargeRequest = (ServicesRequest)serializer.Deserialize(reader);

                        var rechargeObject = RechargeTemplate.TransactionJson;
                        rechargeObject = rechargeObject.Replace("modeValue", rechargeRequest.mode).
                            Replace("spkeyValue", rechargeRequest.spkey).
                            Replace("agentidValue", rechargeRequest.agentid).
                            Replace("accountValue", rechargeRequest.account).
                            Replace("amountValue", rechargeRequest.amount).
                            Replace("formatValue", rechargeRequest.format);

                        //var query = "https://www.instantpay.in/ws/api/transaction?token=63a3c58a814a9fe4e684246673b72027&spkey=VFP&agentid=190620171120115451215&account=9261230073&amount=30&optional1=VodafoneRechargeTest&format=json";
                      //  var query = "https://www.instantpay.in/ws/api/transaction?token=63a3c58a814a9fe4e684246673b72027&spkey=ACP&agentid=190620171120115451215&account=9829221803&amount=30&optional1=VodafoneRechargeTest&format=json";
                        // Code to get Cancel flight status*** Start
                        using (var httpClient = new HttpClient())
                        {
                            var httpContent = new StringContent(rechargeObject, Encoding.UTF8, "text/xml");
                            var requestString = "https://www.instantpay.in/ws/api/transaction?" + rechargeObject;
                            ///Do the actual request and await the response
                            var httpResponse = await httpClient.GetAsync(rechargeObject);
                            //var httpResponse = await httpClient.GetAsync(query);

                            ///If the response contains content we want to read it!
                            if (httpResponse.Content != null)
                            {
                                responseString = await httpResponse.Content.ReadAsStringAsync();
                                Console.Write(responseString);
                                response = Request.CreateResponse<string>(HttpStatusCode.OK, responseString);
                            }
                        }

                        /*******Extract repsonse oject from xml******/
                    }
                }
                catch (Exception e)
                {
                    response = Request.CreateResponse<string>(HttpStatusCode.NoContent, e.Message);
                }
            }


            return response;
        }

        [ActionName("CheckTransactionStatus")]
        public async Task<HttpResponseMessage> CheckTransactionStatus()
        {
            string responseString = string.Empty;
            HttpResponseMessage response = null;
            if (hepler.CheckHeader(Request))
            {
                try
                {
                    var searchParamterXml = await Request.Content.ReadAsByteArrayAsync();
                    string searchParameter = Encoding.UTF8.GetString(searchParamterXml);
                    XmlSerializer serializer = new XmlSerializer(typeof(ServicesRequest));
                    using (TextReader reader = new StringReader(searchParameter))
                    {
                        ServicesRequest rechargeRequest = (ServicesRequest)serializer.Deserialize(reader);

                        var rechargeObject = RechargeTemplate.StatusJson;
                        rechargeObject = rechargeObject.Replace("agentidValue", rechargeRequest.agentid);

                        //var query = "https://www.instantpay.in/ws/api/getMIS?token=63a3c58a814a9fe4e684246673b72027&agentid=190620171120115451215&format=json";
                        // Code to get Cancel flight status*** Start
                        using (var httpClient = new HttpClient())
                        {
                            ///Do the actual request and await the response
                            var httpResponse = await httpClient.GetAsync(rechargeObject);
                            //var httpResponse = await httpClient.GetAsync(query);

                            ///If the response contains content we want to read it!
                            if (httpResponse.Content != null)
                            {
                                responseString = await httpResponse.Content.ReadAsStringAsync();
                                Console.Write(responseString);
                                response = Request.CreateResponse<string>(HttpStatusCode.OK, responseString);
                            }
                        }

                        /*******Extract repsonse oject from xml******/
                    }
                }
                catch (Exception e)
                {
                    response = Request.CreateResponse<string>(HttpStatusCode.NoContent, e.Message);
                }
            }


            return response;
        }

        // POST api/values
        //[ActionName("CheckWalletBalance")]
        public async Task<HttpResponseMessage> GetWalletBalance()
        {
            string responseString = string.Empty;
            HttpResponseMessage response = null;
            if (hepler.CheckHeader(Request))
            {
                try
                {
                    var query = "https://www.instantpay.in/ws/api/checkwallet?token=63a3c58a814a9fe4e684246673b72027&format=json";
                    // Code to get Cancel flight status*** Start
                    using (var httpClient = new HttpClient())
                    {
                        ///Do the actual request and await the response
                        //  var httpResponse = await httpClient.GetAsync(requestString);
                        var httpResponse = await httpClient.GetAsync(query);

                        ///If the response contains content we want to read it!
                        if (httpResponse.Content != null)
                        {
                            responseString = await httpResponse.Content.ReadAsStringAsync();
                            Console.Write(responseString);
                            response = Request.CreateResponse<string>(HttpStatusCode.OK, responseString);
                        }
                    }

                    /*******Extract repsonse oject from xml******/
                }
                catch (Exception e)
                {
                    response = Request.CreateResponse<string>(HttpStatusCode.NoContent, e.Message);
                }
            }

            return response;
        }
    }
}