using Annex.InterfaceService.InterfacesBase;
using AutoMapper;
using Kavenegar.Core.Json;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using UC.ClassDTO.DTOs;
using UC.InterfaceService.InterfacesBase;
using WCS.ClassDomain.Domains;
using WCS.ClassDTO.DTOs;
using WCS.ClassDTO.DTOs.Bank;
using WCS.ClassDTO.DTOs.Customs;
using WCS.Common.Authorization;
using WCS.Common.Exceptions;
using WCS.Common.Mapping;
using WCS.InterfaceService.InterfacesBase;

namespace WCS.WebApi.Controllers.Api
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private IUnitOfWorkUCService _UnitOfWorkUCService; private IUnitOfWorkWCSService _UnitOfWorkWCSService; private IUnitOfWorkAnnexService _UnitOfWorkAnnexService; private readonly IConfiguration _Configuration;
        public PaymentController(IUnitOfWorkUCService UnitOfWorkUCService, IUnitOfWorkWCSService UnitOfWorkWCSService, IUnitOfWorkAnnexService UnitOfWorkAnnexService, IConfiguration Configuration) { _UnitOfWorkUCService = UnitOfWorkUCService; _UnitOfWorkWCSService = UnitOfWorkWCSService; _UnitOfWorkAnnexService = UnitOfWorkAnnexService; _Configuration = Configuration; }

        //[AuthorizeCommon]
        [Route("Payment")]
        [HttpPost]
        public async Task<IActionResult> Payment([FromBody] DtoOrdersCustomer Orders)
        {
            Random random = new Random();
            int _OrderId = random.Next(10000000, 2000000000);

            if (Orders.OrdersProducts.Count == 0) return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
            var TenantID = Convert.ToInt32(_Configuration["CompanyInfo:TenantID"]);
            string _Token = string.Empty;
            _Token = AuthenticationHttpContext.GetUserTokenHash(this.HttpContext);
            if (_Token == "") return Ok(new { Message = MessageException.Messages.NoFoundToken.ToString(), Status = MessageException.Status.Status400 });
            var _UserID = (await _UnitOfWorkUCService._ITokenService.GetByWhere(T => T.Token_HashCode == _Token)).Token_UserID;
            if (_UserID == -1) return Ok(new { Message = MessageException.Messages.NotFoundUser.ToString(), Status = MessageException.Status.Status400 });
            var _User = await _UnitOfWorkUCService._IUserService.GetByWhere(U => U.User_ID == _UserID);
            _User.User_FirstName = Orders.FName;
            _User.User_LastName = Orders.LName;
            _User.User_Province_ID = Orders.Province;
            _User.User_City_ID = Orders.City;
            _User.User_Address = Orders.Address;
            _User.User_Tell = Orders.Tell;
            _UnitOfWorkUCService._IUserService.Update(_User);
            await _UnitOfWorkUCService.SaveChange_DataBase_Async();
            var _NewListOrder = new List<Order>();
            foreach (var item in Orders.OrdersProducts)
            {
                _NewListOrder.Add(new Order
                {
                    Order_Count = item.PCount,
                    Order_DateRegister = DateTime.Now,
                    Order_Desc = "",
                    Order_UserID = _UserID,
                    Order_ProductID = item.PID,
                    Order_Price = (await _UnitOfWorkWCSService._IProductPriceService.GetLastProductPrice(item.PID)).ProductPrice_Price,
                    Order_OrderCode = _OrderId,
                    Order_ResultComment = 0,
                    Order_PType = 1,
                });
            }

            if (_NewListOrder != null)
            {
                await _UnitOfWorkWCSService._IOrderService.InsertRange(_NewListOrder);
                await _UnitOfWorkWCSService.SaveChange_DataBase_Async();
            }
            long TotalPrice = 0;
            foreach (var Order in _NewListOrder)
            {
                TotalPrice += Order.Order_Price;
            }

            if (TotalPrice > 0)
            {
                var bodyPayment = new BodyPayment()
                {
                    Amount = TotalPrice,
                    CallBackUrl = "https://www.asetcoyadak.com/api/Payment/PaymentCallBack",
                    AdditionalData = "آستکو یدک",
                    Originator = _User.User_Mobile,
                    OrderId = _OrderId,
                };
                var _ResultPeyment = (await _UnitOfWorkWCSService._IBank.Payment(bodyPayment));
                if (_ResultPeyment.Status == 0 && _ResultPeyment.Token > 0)
                {
                    var _Url = "https://pec.shaparak.ir/NewIPG/?token=" + _ResultPeyment.Token.ToString();
                    return Ok(new { Url = _Url, Message = _ResultPeyment.Message.ToString(), Status = MessageException.Status.Status200 });
                }
                return Ok(new { Message = _ResultPeyment.Message.ToString(), Status = MessageException.Status.Status400 });
            }
            return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
        }

        [Route("PaymentCallBack")]
        [HttpPost]
        public async Task<IActionResult> PaymentCallBack()
        {
            var Status = Convert.ToInt16(Request.Form["status"].ToString());
            var OrderId = Convert.ToInt64(Request.Form["OrderId"].ToString());
            if (Status == -138)
            {
                try
                {
                    var _Order = await _UnitOfWorkWCSService._IOrderService.GetAll(O => O.Order_OrderCode == OrderId);
                    if (_Order != null)
                    {
                        _UnitOfWorkWCSService._IOrderService.DeleteRange(_Order);
                        await _UnitOfWorkWCSService.SaveChange_DataBase_Async();
                    }
                }
                catch { }

                var Html = "<!DOCTYPE html><html lang=\"en\"> <head> <meta charset=\"UTF-8\"/> <meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\"/> <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\"/> <title>انتقال به آستکو</title> <link rel=\"preconnect\" href=\"//v1.fontapi.ir\"/> <style>@import url(\"https://v1.fontapi.ir/css/Vazir\"); body{margin: unset; position: relative; overflow: hidden; font-family: Vazir, sans-serif;}.container{width: 100vw; height: 100vh; background-color: rgb(250, 250, 250); display: flex; justify-content: center; align-items: center; position: relative; direction: rtl; flex-direction: column;}.blueItems{height: 50px; background-color: #188aec; position: absolute; filter: blur(0.2px); z-index: 100; border-radius: 0 20px 20px 0; left: 0;}.yellowItems{height: 50px; background-color: #dea412; position: absolute; filter: blur(0.2px); z-index: 100; border-radius: 20px 0 0 20px; right: 0;}.blue-one{width: 0; top: 10px; animation: resizeAnime30 0.7s both;}.blue-two{width: 0; top: 70px; animation: resizeAnime50 0.7s 0.7s both;}.blue-three{width: 0; top: 140px; animation: resizeAnime70 0.7s 1.4s both;}.yellow-one{width: 0; bottom: 10px; animation: resizeAnime70 0.7s both;}.yellow-two{width: 0; bottom: 70px; animation: resizeAnime50 0.7s 0.7s both;}.yellow-three{width: 0; bottom: 140px; animation: resizeAnime30 0.7s 1.4s both;}.icon{margin-left: 20px; animation: iconAnime 2s infinite;}.timer-content{display: flex; gap: 16px;}@keyframes iconAnime{0%{transform: rotateX(0);}100%{transform: rotateY(360deg);}}@keyframes resizeAnime30{0%{width: 0%;}/* 50%{width: 50%;}*/ 100%{width: 30%;}}@keyframes resizeAnime50{0%{width: 0%;}/* 50%{width: 40%;}*/ 100%{width: 50%;}}@keyframes resizeAnime70{0%{width: 0%;}/* 50%{width: 30%;}*/ 100%{width: 70%;}}</style> </head> <body> <div class=\"blueItems blue-one\"></div><div class=\"blueItems blue-two\"></div><div class=\"blueItems blue-three\"></div><div class=\"yellowItems yellow-one\"></div><div class=\"yellowItems yellow-two\"></div><div class=\"yellowItems yellow-three\"></div><div class=\"container\"> <div class=\"icon\"> <svg xmlns=\"http://www.w3.org/2000/svg\" width=\"106\" height=\"28\" fill=\"none\" xmlns:v=\"https://vecta.io/nano\" > <path d=\"M12.787.057c3.496-.326 7.098.757 9.85 2.934 1.912 1.501 3.428 3.501 4.34 5.754a14 14 0 0 1 .63 8.544c-.606 2.516-1.932 4.854-3.783 6.665-1.855 1.842-4.241 3.143-6.795 3.704-1.405.297-2.856.432-4.287.274-2.304-.205-4.554-.994-6.477-2.28a14.05 14.05 0 0 1-4.846-5.525C-.097 17.055-.416 13.419.547 10.13a14.03 14.03 0 0 1 4.986-7.268A13.94 13.94 0 0 1 12.787.057zm.002 1.054c-3.341.297-6.523 1.977-8.676 4.542-1.608 1.894-2.655 4.26-2.959 6.726-.296 2.322.055 4.724 1.011 6.861a13.05 13.05 0 0 0 4.736 5.573 12.9 12.9 0 0 0 11.652 1.299c2.313-.866 4.359-2.418 5.834-4.398 1.801-2.394 2.719-5.433 2.546-8.423a12.9 12.9 0 0 0-1.891-6.051 13.07 13.07 0 0 0-5.689-5.029 12.92 12.92 0 0 0-6.564-1.102zm-.501 11.485l-.011-3.745 6.922 2.616-.001 3.716-.273-.078-6.637-2.51zm-3.479 3.609l6.912 2.609v3.506c.027.127-.125.215-.228.144l-6.687-2.532.004-3.728z\" fill=\"#188aec\"/> <path d=\"M13.541 5.507c.403-.099.847-.083 1.223.106a1.71 1.71 0 0 1 .973 1.454 1.74 1.74 0 0 1-.613 1.415c-.448.371-1.086.5-1.639.311a1.72 1.72 0 0 1-1.181-1.65c-.006-.739.529-1.435 1.237-1.636zm-1.269 7.7l3.472 1.314.009 3.71-3.465-1.283-.017-3.741z\" fill=\"#dea412\"/> <path d=\"M51.776 5.164a5.7 5.7 0 0 1 3.587.292l-.457 1.651c-.66-.313-1.397-.471-2.127-.419-.347.031-.725.144-.94.438-.226.306-.16.775.138 1.011.387.318.874.476 1.331.662.696.261 1.403.595 1.889 1.177.58.69.662 1.701.349 2.526-.267.706-.88 1.235-1.573 1.504-.867.34-1.825.381-2.742.269-.593-.08-1.186-.22-1.723-.49l.418-1.692c.726.356 1.53.573 2.342.562.382-.011.794-.081 1.088-.345a.88.88 0 0 0-.015-1.279c-.433-.381-1.003-.537-1.529-.745-.671-.267-1.335-.642-1.765-1.239-.559-.766-.539-1.865-.041-2.657.392-.629 1.063-1.039 1.769-1.228zm30.209.158c.911-.326 1.901-.382 2.856-.266.451.059.906.152 1.321.346l-.42 1.627c-.719-.292-1.516-.403-2.285-.293-.67.097-1.32.426-1.732.971-.478.619-.623 1.431-.586 2.197.031.708.262 1.436.77 1.949.485.501 1.184.743 1.871.78.665.034 1.337-.066 1.966-.285l.309 1.597c-.645.283-1.359.354-2.056.386-1.195.043-2.456-.193-3.421-.937-.864-.652-1.388-1.681-1.537-2.741-.17-1.213.004-2.513.67-3.56.523-.832 1.351-1.449 2.274-1.772zm10.996-.271c1.249-.176 2.614.098 3.57.958 1.031.912 1.48 2.332 1.447 3.679-.01 1.286-.418 2.632-1.379 3.529-.987.938-2.434 1.252-3.757 1.075-1.001-.127-1.957-.635-2.582-1.433-.804-1.007-1.084-2.348-.982-3.612.084-1.123.517-2.245 1.326-3.045.63-.632 1.477-1.029 2.357-1.151zm.287 1.612c-.708.118-1.255.687-1.514 1.332-.358.878-.394 1.873-.192 2.793.136.575.412 1.143.885 1.516a1.97 1.97 0 0 0 2.256.097c.483-.323.786-.854.948-1.4a4.9 4.9 0 0 0 .114-2.187c-.12-.659-.397-1.323-.923-1.76-.43-.363-1.026-.496-1.574-.392zM40.472 5.151l2.691.001 2.805 9.04c-.735-.002-1.47.001-2.205-.002l-.701-2.315-2.588.001-.646 2.315h-2.124l2.767-9.041zm.871 3.149l-.572 2.048 1.998-.001-1.03-3.692-.396 1.645zm18.436-3.149h5.589l-.001 1.673-3.535.001-.001 1.873 3.332-.001.002 1.667c-1.112.001-2.223-.002-3.335.001v2.145l3.724.002v1.674h-5.775V5.151zm9.257-.006h6.96l.001 1.72-2.474.002-.001 7.32c-.684-.001-1.367-.002-2.05 0l-.002-7.321c-.812-.001-1.623.002-2.434-.001v-1.72z\" fill=\"#188aec\"/> <path d=\"M37 17.817h1.493l1.042 2.496 1.025-2.495h1.468l-1.884 3.329-.017 2.416h-1.301v-2.236c.013-.149-.089-.269-.15-.395L37 17.817zm14.763 0l1.721.006 1.782 5.741h-1.405l-.446-1.473c-.55-.001-1.099.001-1.648-.001l-.413 1.474c-.451.001-.903-.001-1.354.001l1.763-5.747zm.191 3.293h1.272l-.657-2.328-.614 2.327zm12.639-3.214a12.12 12.12 0 0 1 2.188-.107c.71.033 1.45.192 2.014.651.525.407.824 1.053.887 1.706.089.86-.076 1.796-.654 2.468-.459.546-1.161.813-1.847.931-.855.14-1.729.097-2.588.004v-5.654zm1.31.929v3.771c.607.056 1.282.024 1.776-.376.484-.384.648-1.034.643-1.627.008-.534-.16-1.113-.6-1.45-.511-.397-1.206-.415-1.819-.319zM80.3 17.822l1.72-.005 1.79 5.746h-1.408l-.443-1.471c-.551-.002-1.101-.002-1.652 0l-.41 1.472h-1.356l1.759-5.741zm.81.95l-.613 2.339 1.272.001-.659-2.34zm12.043-.956h1.291l.004 2.565 1.724-2.565h1.604l-1.908 2.446 2.011 3.3c-.512-.001-1.024.007-1.535-.004l-1.397-2.466c-.18.189-.334.399-.502.598v1.873c-.432-.002-.863.005-1.294-.004l.003-5.743z\" fill=\"#dea412\"/> </svg> </div><h1 class=\"title\">در حال انتقال به سایت آستکو یدک</h1> <div class=\"timer-content\"> <div> اگر به صورت اتوماتیک وارد سایت آستکو یدک نشدید <a href='https://www.asetcoyadak.com/payment/failed' >اینجا کلیک کنید</a> </div><div id=\"timer\">00:03</div></div></div><script>var time=3; var countDown=setInterval(function (){ if (time <=0){clearInterval(countDown); window.location.href='https://www.asetcoyadak.com/payment/failed';return; }time -=1; var timerEl=document.getElementById(\"timer\"); timerEl.innerHTML=`00:${time < 10 && \"0\"}` + time;}, 1000); </script> </body></html>";
                return base.Content(Html, "text/html", System.Text.Encoding.UTF8);
                // return Ok(new { Message = MessageException.Messages.CancelPayMentCallBack.ToString(), Status = MessageException.Status.Status400 });
            }
            var Token = Convert.ToInt64(Request.Form["Token"].ToString());
            var TerminalNo = Convert.ToInt32(Request.Form["TerminalNo"].ToString());
            var RRN = Convert.ToInt64(Request.Form["RRN"].ToString());
            var Amount = Request.Form["Amount"].ToString().Replace(",", "").ToString();
            var DiscountAmount = Request.Form["SwAmount"].ToString().Replace(",", "").ToString();
            var CardNumberHas = Request.Form["HashCardNumber"].ToString();
            var resultPaymentCallBack = new ResultPaymentCallBack()
            {
                Amount = Convert.ToInt64(Amount),
                HashCardNumber = CardNumberHas,
                OrderId = OrderId,
                RRN = RRN,
                status = Status,
                TerminalNo = TerminalNo,
                Token = Token,
                TspToken = ""
            };
            if (resultPaymentCallBack.status == 0 && resultPaymentCallBack.RRN > 0)
            {
                var _BodyConfirm = new BodyConfirm() { Token = resultPaymentCallBack.Token };
                var _ResultConfirm = (await _UnitOfWorkWCSService._IBank.Confirm(_BodyConfirm));
                if (_ResultConfirm.Status == 0)
                {
                    try
                    {
                        var UserID = (await _UnitOfWorkWCSService._IOrderService.GetByWhere(O => O.Order_OrderCode == OrderId)).Order_UserID;
                        var _OrderTransaction = new OrderTransaction()
                        {
                            OrderTransaction_UserID = UserID,
                            OrderTransaction_DateTrans = DateTime.Now,
                            OrderTransaction_OrderCode = (int)OrderId,
                            OrderTransaction_Price = resultPaymentCallBack.Amount,
                            OrderTransaction_TrackingCode = resultPaymentCallBack.RRN.ToString()
                        };
                        await _UnitOfWorkWCSService._IOrderTransactionService.Insert(_OrderTransaction);
                        await _UnitOfWorkWCSService.SaveChange_DataBase_Async();
                    }
                    catch { }

                   // var Html = "<html><body><div style='background-color:red; height:60%;width:60%;'> <span style='color:black ; font-size:15px; font-weight:bold;  text-align: center; font-family: tahoma;'>در حال انتقال به سایت استکو یدک</span> <script>  setTimeout(function () {window.location.href='https://www.asetcoyadak.com/Success?TrackCode=" + resultPaymentCallBack.RRN.ToString() + "' ;}, 200)  </script> </div></body></html>";
                    var Html = "<!DOCTYPE html><html lang=\"en\"> <head> <meta charset=\"UTF-8\"/> <meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\"/> <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\"/> <title>انتقال به آستکو</title> <link rel=\"preconnect\" href=\"//v1.fontapi.ir\"/> <style>@import url(\"https://v1.fontapi.ir/css/Vazir\"); body{margin: unset; position: relative; overflow: hidden; font-family: Vazir, sans-serif;}.container{width: 100vw; height: 100vh; background-color: rgb(250, 250, 250); display: flex; justify-content: center; align-items: center; position: relative; direction: rtl; flex-direction: column;}.blueItems{height: 50px; background-color: #188aec; position: absolute; filter: blur(0.2px); z-index: 100; border-radius: 0 20px 20px 0; left: 0;}.yellowItems{height: 50px; background-color: #dea412; position: absolute; filter: blur(0.2px); z-index: 100; border-radius: 20px 0 0 20px; right: 0;}.blue-one{width: 0; top: 10px; animation: resizeAnime30 0.7s both;}.blue-two{width: 0; top: 70px; animation: resizeAnime50 0.7s 0.7s both;}.blue-three{width: 0; top: 140px; animation: resizeAnime70 0.7s 1.4s both;}.yellow-one{width: 0; bottom: 10px; animation: resizeAnime70 0.7s both;}.yellow-two{width: 0; bottom: 70px; animation: resizeAnime50 0.7s 0.7s both;}.yellow-three{width: 0; bottom: 140px; animation: resizeAnime30 0.7s 1.4s both;}.icon{margin-left: 20px; animation: iconAnime 2s infinite;}.timer-content{display: flex; gap: 16px;}@keyframes iconAnime{0%{transform: rotateX(0);}100%{transform: rotateY(360deg);}}@keyframes resizeAnime30{0%{width: 0%;}/* 50%{width: 50%;}*/ 100%{width: 30%;}}@keyframes resizeAnime50{0%{width: 0%;}/* 50%{width: 40%;}*/ 100%{width: 50%;}}@keyframes resizeAnime70{0%{width: 0%;}/* 50%{width: 30%;}*/ 100%{width: 70%;}}</style> </head> <body> <div class=\"blueItems blue-one\"></div><div class=\"blueItems blue-two\"></div><div class=\"blueItems blue-three\"></div><div class=\"yellowItems yellow-one\"></div><div class=\"yellowItems yellow-two\"></div><div class=\"yellowItems yellow-three\"></div><div class=\"container\"> <div class=\"icon\"> <svg xmlns=\"http://www.w3.org/2000/svg\" width=\"106\" height=\"28\" fill=\"none\" xmlns:v=\"https://vecta.io/nano\" > <path d=\"M12.787.057c3.496-.326 7.098.757 9.85 2.934 1.912 1.501 3.428 3.501 4.34 5.754a14 14 0 0 1 .63 8.544c-.606 2.516-1.932 4.854-3.783 6.665-1.855 1.842-4.241 3.143-6.795 3.704-1.405.297-2.856.432-4.287.274-2.304-.205-4.554-.994-6.477-2.28a14.05 14.05 0 0 1-4.846-5.525C-.097 17.055-.416 13.419.547 10.13a14.03 14.03 0 0 1 4.986-7.268A13.94 13.94 0 0 1 12.787.057zm.002 1.054c-3.341.297-6.523 1.977-8.676 4.542-1.608 1.894-2.655 4.26-2.959 6.726-.296 2.322.055 4.724 1.011 6.861a13.05 13.05 0 0 0 4.736 5.573 12.9 12.9 0 0 0 11.652 1.299c2.313-.866 4.359-2.418 5.834-4.398 1.801-2.394 2.719-5.433 2.546-8.423a12.9 12.9 0 0 0-1.891-6.051 13.07 13.07 0 0 0-5.689-5.029 12.92 12.92 0 0 0-6.564-1.102zm-.501 11.485l-.011-3.745 6.922 2.616-.001 3.716-.273-.078-6.637-2.51zm-3.479 3.609l6.912 2.609v3.506c.027.127-.125.215-.228.144l-6.687-2.532.004-3.728z\" fill=\"#188aec\"/> <path d=\"M13.541 5.507c.403-.099.847-.083 1.223.106a1.71 1.71 0 0 1 .973 1.454 1.74 1.74 0 0 1-.613 1.415c-.448.371-1.086.5-1.639.311a1.72 1.72 0 0 1-1.181-1.65c-.006-.739.529-1.435 1.237-1.636zm-1.269 7.7l3.472 1.314.009 3.71-3.465-1.283-.017-3.741z\" fill=\"#dea412\"/> <path d=\"M51.776 5.164a5.7 5.7 0 0 1 3.587.292l-.457 1.651c-.66-.313-1.397-.471-2.127-.419-.347.031-.725.144-.94.438-.226.306-.16.775.138 1.011.387.318.874.476 1.331.662.696.261 1.403.595 1.889 1.177.58.69.662 1.701.349 2.526-.267.706-.88 1.235-1.573 1.504-.867.34-1.825.381-2.742.269-.593-.08-1.186-.22-1.723-.49l.418-1.692c.726.356 1.53.573 2.342.562.382-.011.794-.081 1.088-.345a.88.88 0 0 0-.015-1.279c-.433-.381-1.003-.537-1.529-.745-.671-.267-1.335-.642-1.765-1.239-.559-.766-.539-1.865-.041-2.657.392-.629 1.063-1.039 1.769-1.228zm30.209.158c.911-.326 1.901-.382 2.856-.266.451.059.906.152 1.321.346l-.42 1.627c-.719-.292-1.516-.403-2.285-.293-.67.097-1.32.426-1.732.971-.478.619-.623 1.431-.586 2.197.031.708.262 1.436.77 1.949.485.501 1.184.743 1.871.78.665.034 1.337-.066 1.966-.285l.309 1.597c-.645.283-1.359.354-2.056.386-1.195.043-2.456-.193-3.421-.937-.864-.652-1.388-1.681-1.537-2.741-.17-1.213.004-2.513.67-3.56.523-.832 1.351-1.449 2.274-1.772zm10.996-.271c1.249-.176 2.614.098 3.57.958 1.031.912 1.48 2.332 1.447 3.679-.01 1.286-.418 2.632-1.379 3.529-.987.938-2.434 1.252-3.757 1.075-1.001-.127-1.957-.635-2.582-1.433-.804-1.007-1.084-2.348-.982-3.612.084-1.123.517-2.245 1.326-3.045.63-.632 1.477-1.029 2.357-1.151zm.287 1.612c-.708.118-1.255.687-1.514 1.332-.358.878-.394 1.873-.192 2.793.136.575.412 1.143.885 1.516a1.97 1.97 0 0 0 2.256.097c.483-.323.786-.854.948-1.4a4.9 4.9 0 0 0 .114-2.187c-.12-.659-.397-1.323-.923-1.76-.43-.363-1.026-.496-1.574-.392zM40.472 5.151l2.691.001 2.805 9.04c-.735-.002-1.47.001-2.205-.002l-.701-2.315-2.588.001-.646 2.315h-2.124l2.767-9.041zm.871 3.149l-.572 2.048 1.998-.001-1.03-3.692-.396 1.645zm18.436-3.149h5.589l-.001 1.673-3.535.001-.001 1.873 3.332-.001.002 1.667c-1.112.001-2.223-.002-3.335.001v2.145l3.724.002v1.674h-5.775V5.151zm9.257-.006h6.96l.001 1.72-2.474.002-.001 7.32c-.684-.001-1.367-.002-2.05 0l-.002-7.321c-.812-.001-1.623.002-2.434-.001v-1.72z\" fill=\"#188aec\"/> <path d=\"M37 17.817h1.493l1.042 2.496 1.025-2.495h1.468l-1.884 3.329-.017 2.416h-1.301v-2.236c.013-.149-.089-.269-.15-.395L37 17.817zm14.763 0l1.721.006 1.782 5.741h-1.405l-.446-1.473c-.55-.001-1.099.001-1.648-.001l-.413 1.474c-.451.001-.903-.001-1.354.001l1.763-5.747zm.191 3.293h1.272l-.657-2.328-.614 2.327zm12.639-3.214a12.12 12.12 0 0 1 2.188-.107c.71.033 1.45.192 2.014.651.525.407.824 1.053.887 1.706.089.86-.076 1.796-.654 2.468-.459.546-1.161.813-1.847.931-.855.14-1.729.097-2.588.004v-5.654zm1.31.929v3.771c.607.056 1.282.024 1.776-.376.484-.384.648-1.034.643-1.627.008-.534-.16-1.113-.6-1.45-.511-.397-1.206-.415-1.819-.319zM80.3 17.822l1.72-.005 1.79 5.746h-1.408l-.443-1.471c-.551-.002-1.101-.002-1.652 0l-.41 1.472h-1.356l1.759-5.741zm.81.95l-.613 2.339 1.272.001-.659-2.34zm12.043-.956h1.291l.004 2.565 1.724-2.565h1.604l-1.908 2.446 2.011 3.3c-.512-.001-1.024.007-1.535-.004l-1.397-2.466c-.18.189-.334.399-.502.598v1.873c-.432-.002-.863.005-1.294-.004l.003-5.743z\" fill=\"#dea412\"/> </svg> </div><h1 class=\"title\"> در حال انتقال به سایت آستکو یدک</h1> <div class=\"timer-content\"> <div> اگر به صورت اتوماتیک وارد سایت آستکو یدک نشدید <a href='https://www.asetcoyadak.com/payment/success?trackcode=" + resultPaymentCallBack.RRN.ToString() + "'>اینجا کلیک کنید</a> </div><div id=\"timer\">00:03</div></div></div><script>var time=3; var countDown=setInterval(function (){ if (time <=0){clearInterval(countDown); window.location.href='https://www.asetcoyadak.com/payment/success?trackCode=" + resultPaymentCallBack.RRN.ToString() + "';return; }time -=1; var timerEl=document.getElementById(\"timer\"); timerEl.innerHTML=`00:${time < 10 && \"0\"}` + time;}, 1000); </script> </body></html>";
                    return base.Content(Html, "text/html", System.Text.Encoding.UTF8);
                    //CodeRahgiri = resultPaymentCallBack.RRN.ToString(), Token = resultPaymentCallBack.Token.ToString(), CardNumberMasked = resultPaymentCallBack.HashCardNumber.ToString(), Status = MessageException.Status.Status200 
                }
                else
                {
                    try
                    {
                        var _Order = await _UnitOfWorkWCSService._IOrderService.GetAll(O => O.Order_OrderCode == OrderId);
                        if (_Order != null)
                        {
                            _UnitOfWorkWCSService._IOrderService.DeleteRange(_Order);
                            await _UnitOfWorkWCSService.SaveChange_DataBase_Async();
                        }
                    }
                    catch { }

                    var _BodyReverse = new BodyReverse() { Token = resultPaymentCallBack.Token };
                    var _ResultReverse = (await _UnitOfWorkWCSService._IBank.Reverse(_BodyReverse));
                    if (_ResultReverse.Status == 0)
                    {
                      
                        var Html = "<!DOCTYPE html><html lang=\"en\"> <head> <meta charset=\"UTF-8\"/> <meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\"/> <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\"/> <title>انتقال به آستکو</title> <link rel=\"preconnect\" href=\"//v1.fontapi.ir\"/> <style>@import url(\"https://v1.fontapi.ir/css/Vazir\"); body{margin: unset; position: relative; overflow: hidden; font-family: Vazir, sans-serif;}.container{width: 100vw; height: 100vh; background-color: rgb(250, 250, 250); display: flex; justify-content: center; align-items: center; position: relative; direction: rtl; flex-direction: column;}.blueItems{height: 50px; background-color: #188aec; position: absolute; filter: blur(0.2px); z-index: 100; border-radius: 0 20px 20px 0; left: 0;}.yellowItems{height: 50px; background-color: #dea412; position: absolute; filter: blur(0.2px); z-index: 100; border-radius: 20px 0 0 20px; right: 0;}.blue-one{width: 0; top: 10px; animation: resizeAnime30 0.7s both;}.blue-two{width: 0; top: 70px; animation: resizeAnime50 0.7s 0.7s both;}.blue-three{width: 0; top: 140px; animation: resizeAnime70 0.7s 1.4s both;}.yellow-one{width: 0; bottom: 10px; animation: resizeAnime70 0.7s both;}.yellow-two{width: 0; bottom: 70px; animation: resizeAnime50 0.7s 0.7s both;}.yellow-three{width: 0; bottom: 140px; animation: resizeAnime30 0.7s 1.4s both;}.icon{margin-left: 20px; animation: iconAnime 2s infinite;}.timer-content{display: flex; gap: 16px;}@keyframes iconAnime{0%{transform: rotateX(0);}100%{transform: rotateY(360deg);}}@keyframes resizeAnime30{0%{width: 0%;}/* 50%{width: 50%;}*/ 100%{width: 30%;}}@keyframes resizeAnime50{0%{width: 0%;}/* 50%{width: 40%;}*/ 100%{width: 50%;}}@keyframes resizeAnime70{0%{width: 0%;}/* 50%{width: 30%;}*/ 100%{width: 70%;}}</style> </head> <body> <div class=\"blueItems blue-one\"></div><div class=\"blueItems blue-two\"></div><div class=\"blueItems blue-three\"></div><div class=\"yellowItems yellow-one\"></div><div class=\"yellowItems yellow-two\"></div><div class=\"yellowItems yellow-three\"></div><div class=\"container\"> <div class=\"icon\"> <svg xmlns=\"http://www.w3.org/2000/svg\" width=\"106\" height=\"28\" fill=\"none\" xmlns:v=\"https://vecta.io/nano\" > <path d=\"M12.787.057c3.496-.326 7.098.757 9.85 2.934 1.912 1.501 3.428 3.501 4.34 5.754a14 14 0 0 1 .63 8.544c-.606 2.516-1.932 4.854-3.783 6.665-1.855 1.842-4.241 3.143-6.795 3.704-1.405.297-2.856.432-4.287.274-2.304-.205-4.554-.994-6.477-2.28a14.05 14.05 0 0 1-4.846-5.525C-.097 17.055-.416 13.419.547 10.13a14.03 14.03 0 0 1 4.986-7.268A13.94 13.94 0 0 1 12.787.057zm.002 1.054c-3.341.297-6.523 1.977-8.676 4.542-1.608 1.894-2.655 4.26-2.959 6.726-.296 2.322.055 4.724 1.011 6.861a13.05 13.05 0 0 0 4.736 5.573 12.9 12.9 0 0 0 11.652 1.299c2.313-.866 4.359-2.418 5.834-4.398 1.801-2.394 2.719-5.433 2.546-8.423a12.9 12.9 0 0 0-1.891-6.051 13.07 13.07 0 0 0-5.689-5.029 12.92 12.92 0 0 0-6.564-1.102zm-.501 11.485l-.011-3.745 6.922 2.616-.001 3.716-.273-.078-6.637-2.51zm-3.479 3.609l6.912 2.609v3.506c.027.127-.125.215-.228.144l-6.687-2.532.004-3.728z\" fill=\"#188aec\"/> <path d=\"M13.541 5.507c.403-.099.847-.083 1.223.106a1.71 1.71 0 0 1 .973 1.454 1.74 1.74 0 0 1-.613 1.415c-.448.371-1.086.5-1.639.311a1.72 1.72 0 0 1-1.181-1.65c-.006-.739.529-1.435 1.237-1.636zm-1.269 7.7l3.472 1.314.009 3.71-3.465-1.283-.017-3.741z\" fill=\"#dea412\"/> <path d=\"M51.776 5.164a5.7 5.7 0 0 1 3.587.292l-.457 1.651c-.66-.313-1.397-.471-2.127-.419-.347.031-.725.144-.94.438-.226.306-.16.775.138 1.011.387.318.874.476 1.331.662.696.261 1.403.595 1.889 1.177.58.69.662 1.701.349 2.526-.267.706-.88 1.235-1.573 1.504-.867.34-1.825.381-2.742.269-.593-.08-1.186-.22-1.723-.49l.418-1.692c.726.356 1.53.573 2.342.562.382-.011.794-.081 1.088-.345a.88.88 0 0 0-.015-1.279c-.433-.381-1.003-.537-1.529-.745-.671-.267-1.335-.642-1.765-1.239-.559-.766-.539-1.865-.041-2.657.392-.629 1.063-1.039 1.769-1.228zm30.209.158c.911-.326 1.901-.382 2.856-.266.451.059.906.152 1.321.346l-.42 1.627c-.719-.292-1.516-.403-2.285-.293-.67.097-1.32.426-1.732.971-.478.619-.623 1.431-.586 2.197.031.708.262 1.436.77 1.949.485.501 1.184.743 1.871.78.665.034 1.337-.066 1.966-.285l.309 1.597c-.645.283-1.359.354-2.056.386-1.195.043-2.456-.193-3.421-.937-.864-.652-1.388-1.681-1.537-2.741-.17-1.213.004-2.513.67-3.56.523-.832 1.351-1.449 2.274-1.772zm10.996-.271c1.249-.176 2.614.098 3.57.958 1.031.912 1.48 2.332 1.447 3.679-.01 1.286-.418 2.632-1.379 3.529-.987.938-2.434 1.252-3.757 1.075-1.001-.127-1.957-.635-2.582-1.433-.804-1.007-1.084-2.348-.982-3.612.084-1.123.517-2.245 1.326-3.045.63-.632 1.477-1.029 2.357-1.151zm.287 1.612c-.708.118-1.255.687-1.514 1.332-.358.878-.394 1.873-.192 2.793.136.575.412 1.143.885 1.516a1.97 1.97 0 0 0 2.256.097c.483-.323.786-.854.948-1.4a4.9 4.9 0 0 0 .114-2.187c-.12-.659-.397-1.323-.923-1.76-.43-.363-1.026-.496-1.574-.392zM40.472 5.151l2.691.001 2.805 9.04c-.735-.002-1.47.001-2.205-.002l-.701-2.315-2.588.001-.646 2.315h-2.124l2.767-9.041zm.871 3.149l-.572 2.048 1.998-.001-1.03-3.692-.396 1.645zm18.436-3.149h5.589l-.001 1.673-3.535.001-.001 1.873 3.332-.001.002 1.667c-1.112.001-2.223-.002-3.335.001v2.145l3.724.002v1.674h-5.775V5.151zm9.257-.006h6.96l.001 1.72-2.474.002-.001 7.32c-.684-.001-1.367-.002-2.05 0l-.002-7.321c-.812-.001-1.623.002-2.434-.001v-1.72z\" fill=\"#188aec\"/> <path d=\"M37 17.817h1.493l1.042 2.496 1.025-2.495h1.468l-1.884 3.329-.017 2.416h-1.301v-2.236c.013-.149-.089-.269-.15-.395L37 17.817zm14.763 0l1.721.006 1.782 5.741h-1.405l-.446-1.473c-.55-.001-1.099.001-1.648-.001l-.413 1.474c-.451.001-.903-.001-1.354.001l1.763-5.747zm.191 3.293h1.272l-.657-2.328-.614 2.327zm12.639-3.214a12.12 12.12 0 0 1 2.188-.107c.71.033 1.45.192 2.014.651.525.407.824 1.053.887 1.706.089.86-.076 1.796-.654 2.468-.459.546-1.161.813-1.847.931-.855.14-1.729.097-2.588.004v-5.654zm1.31.929v3.771c.607.056 1.282.024 1.776-.376.484-.384.648-1.034.643-1.627.008-.534-.16-1.113-.6-1.45-.511-.397-1.206-.415-1.819-.319zM80.3 17.822l1.72-.005 1.79 5.746h-1.408l-.443-1.471c-.551-.002-1.101-.002-1.652 0l-.41 1.472h-1.356l1.759-5.741zm.81.95l-.613 2.339 1.272.001-.659-2.34zm12.043-.956h1.291l.004 2.565 1.724-2.565h1.604l-1.908 2.446 2.011 3.3c-.512-.001-1.024.007-1.535-.004l-1.397-2.466c-.18.189-.334.399-.502.598v1.873c-.432-.002-.863.005-1.294-.004l.003-5.743z\" fill=\"#dea412\"/> </svg> </div><h1 class=\"title\">در حال انتقال به سایت آستکو یدک</h1> <div class=\"timer-content\"> <div> اگر به صورت اتوماتیک وارد سایت آستکو یدک نشدید <a href='https://www.asetcoyadak.com/payment/failed?trackcode=" + resultPaymentCallBack.RRN.ToString() + "'>اینجا کلیک کنید</a> </div><div id=\"timer\">00:03</div></div></div><script>var time=3; var countDown=setInterval(function (){ if (time <=0){clearInterval(countDown); window.location.href='https://www.asetcoyadak.com/payment/failed?trackCode=" + resultPaymentCallBack.RRN.ToString() + "';return; }time -=1; var timerEl=document.getElementById(\"timer\"); timerEl.innerHTML=`00:${time < 10 && \"0\"}` + time;}, 1000); </script> </body></html>";
                        return base.Content(Html, "text/html", System.Text.Encoding.UTF8);
                        // return Ok(new { CodeRahgiri = resultPaymentCallBack.RRN.ToString(), Message = MessageException.Messages.ReversePayMent.ToString(), Status = MessageException.Status.Status200 });
                    }

                }
            }
            return Ok(new { Message = MessageException.Messages.RequestFailt.ToString(), Status = MessageException.Status.Status400 });
        }

    }
}
