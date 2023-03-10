

using WCS.ClassDTO.DTOs.Bank;
using WCS.InterfaceService.ExternalInterfaces;
using System.Text;
using static WCS.Common.Exceptions.MessageException;
using Newtonsoft.Json;
using System.Xml;
using Newtonsoft.Json.Linq;

namespace WCS.Service.ExternalServices
{
    public class BankService : IBank
    {
        private readonly static string PinCode = "x4622TNUmoyV1mtQv2hp";

        public async Task<ResultPeyment> Payment(BodyPayment BodyPayment)
        {
            var resultPeyment = new ResultPeyment();
            var url = "https://pec.shaparak.ir/NewIPGServices/Sale/SaleService.asmx";
            var httpClient = new HttpClient();
            var xml = "<soap:Envelope xmlns:soap=\"http://www.w3.org/2003/05/soap-envelope\" xmlns:sal=\"https://pec.Shaparak.ir/NewIPGServices/Sale/SaleService\">";
            xml += "<soap:Header/>";
            xml += "<soap:Body>";
            xml += "<sal:SalePaymentRequest>";
            xml += "<sal:requestData>";
            xml += "<sal:LoginAccount>" + PinCode + "</sal:LoginAccount>";
            xml += "<sal:Amount>" + BodyPayment.Amount.ToString() + "</sal:Amount>";
            xml += "<sal:OrderId>" + BodyPayment.OrderId.ToString() + "</sal:OrderId>";
            xml += "<sal:CallBackUrl>" + BodyPayment.CallBackUrl.ToString() + "</sal:CallBackUrl>";
            xml += "</sal:requestData>";
            xml += "</sal:SalePaymentRequest>";
            xml += "</soap:Body>";
            xml += "</soap:Envelope>";
            var httpContent = new StringContent(xml, Encoding.UTF8, "text/xml");
            try
            {
                var result = await httpClient.PostAsync(url, httpContent);
                var content = await result.Content.ReadAsStringAsync();

                var doc = new XmlDocument();
                doc.LoadXml(content);
                var json = JsonConvert.SerializeXmlNode(doc);
                var jObject = JObject.Parse(json);
                var Envelope = JObject.Parse(jObject["soap:Envelope"].ToString());
                var Body = JObject.Parse(Envelope["soap:Body"].ToString());
                var SalePayment = JObject.Parse(Body["SalePaymentRequestResponse"].ToString());
                var Result = JObject.Parse(SalePayment["SalePaymentRequestResult"].ToString());
                resultPeyment.Status = Convert.ToInt16(Result.Value<string>("Status").ToString());
                resultPeyment.Message = Result.Value<string>("Message").ToString();
                resultPeyment.Token = Convert.ToInt64(Result.Value<string>("Token").ToString());
            }
            catch (Exception ex)
            {
                resultPeyment.Status = Convert.ToInt16(Status.Status400);
                resultPeyment.Message = ex.ToString();
                resultPeyment.Token = 0;
            }

            return resultPeyment;
        }

        public async Task<ResultConfirm> Confirm(BodyConfirm BodyConfirm)
        {
            var resultConfirm = new ResultConfirm();
            var url = "https://pec.shaparak.ir/NewIPGServices/Confirm/ConfirmService.asmx";
            var httpClient = new HttpClient();
            var xml = "<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:con=\"https://pec.Shaparak.ir/NewIPGServices/Confirm/ConfirmService\">";
            xml += "<soapenv:Header/>";
            xml += "<soapenv:Body>";
            xml += "<con:ConfirmPayment>";
            xml += "<con:requestData>";
            xml += "<con:LoginAccount>" + PinCode + "</con:LoginAccount>";
            xml += "<con:Token>" + BodyConfirm.Token.ToString() + "</con:Token>";
            xml += "</con:requestData>";
            xml += "</con:ConfirmPayment>";
            xml += "</soapenv:Body>";
            xml += "</soapenv:Envelope>";
            var httpContent = new StringContent(xml, Encoding.UTF8, "text/xml");
            try
            {
                var result = await httpClient.PostAsync(url, httpContent);
                var content = await result.Content.ReadAsStringAsync();

                var doc = new XmlDocument();
                doc.LoadXml(content);
                var json = JsonConvert.SerializeXmlNode(doc);
                var jObject = JObject.Parse(json);
                var Envelope = JObject.Parse(jObject["soap:Envelope"].ToString());
                var Body = JObject.Parse(Envelope["soap:Body"].ToString());
                var Confirm = JObject.Parse(Body["ConfirmPaymentResponse"].ToString());
                var Result = JObject.Parse(Confirm["ConfirmPaymentResult"].ToString());
                resultConfirm.Status = Convert.ToInt16(Result.Value<string>("Status").ToString());
                resultConfirm.RRN = Convert.ToInt64(Result.Value<string>("RRN").ToString());
                resultConfirm.Token = Convert.ToInt64(Result.Value<string>("Token").ToString());
                resultConfirm.CardNumberMasked = Result.Value<string>("CardNumberMasked").ToString();

            }
            catch
            {
                resultConfirm.Status = Convert.ToInt16(Status.Status400);
                resultConfirm.RRN = 0;
                resultConfirm.Token = 0;
                resultConfirm.CardNumberMasked = "";
            }

            return resultConfirm;
        }
         
        public async Task<ResultReverse> Reverse(BodyReverse BodyReverse)
        {
            var resultReverse = new ResultReverse();
            var url = "https://pec.shaparak.ir/NewIPGServices/Reverse/ReversalService.asmx";
            var httpClient = new HttpClient();
            var xml = "<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:rev=\"https://pec.Shaparak.ir/NewIPGServices/Reversal/ReversalService\">";
            xml += "<soapenv:Header/>";
            xml += "<soapenv:Body>";
            xml += "<rev:ReversalRequest>";
            xml += "<rev:requestData>";
            xml += "<rev:LoginAccount>" + PinCode + "</rev:LoginAccount>";
            xml += "<rev:Token>" + BodyReverse.Token.ToString() + "</rev:Token>";
            xml += "</rev:requestData>";
            xml += "</rev:ReversalRequest>";
            xml += "</soapenv:Body>";
            xml += "</soapenv:Envelope>";
            var httpContent = new StringContent(xml, Encoding.UTF8, "text/xml");
            try
            {
                var result = await httpClient.PostAsync(url, httpContent);
                var content = await result.Content.ReadAsStringAsync();

                var doc = new XmlDocument();
                doc.LoadXml(content);
                var json = JsonConvert.SerializeXmlNode(doc);
                var jObject = JObject.Parse(json);
                var Envelope = JObject.Parse(jObject["soap:Envelope"].ToString());
                var Body = JObject.Parse(Envelope["soap:Body"].ToString());
                var Confirm = JObject.Parse(Body["ReversalRequestResponse"].ToString());
                var Result = JObject.Parse(Confirm["ReversalRequestResult"].ToString());
                resultReverse.Status = Convert.ToInt16(Result.Value<string>("Status").ToString());
                resultReverse.Token = Convert.ToInt64(Result.Value<string>("Token").ToString());
                resultReverse.Message = Result.Value<string>("Message").ToString();

            }
            catch
            {
                resultReverse.Status = Convert.ToInt16(Status.Status400);
                resultReverse.Token = 0;
                resultReverse.Message = Messages.Error.ToString();
            }

            return resultReverse;
        }
    }
}
