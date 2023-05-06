using LaptopStore.Service.RequestModels;
using LaptopStore.Service.ResponseModels;
using LaptopStore.Service.Services;
using LaptopStore.Service.Services.Interfaces;
using LatopStore.MoMo.Models;
using LatopStore.MoMo.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Web;

namespace LaptopStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _serivce;
        private readonly IMoMoSerivce _momoSerivce;
        public OrderController(IOrderService serivce, IMoMoSerivce momoserivce)
        {
            _serivce = serivce;
            _momoSerivce = momoserivce;
        }
        [HttpPost]
        [Route("")]
        [Authorize(Roles = "116e0deb-f72f-45cf-8ef8-423748b8e9b1, 6fd0f97a-1522-475c-aba1-92f3ce5aeb04")]
        public async Task<IActionResult> Add(OrderRequestModel request)
        {
            try
            {
                var claimsIdentity = this.User.Identity as ClaimsIdentity;
                string _userId = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
                if(request.TransMethod == 2)
                {
                    return Ok(await _momoSerivce.QuickPay(await _serivce.Add(request, _userId), _userId));
                }
                return Ok();
            }
            catch(Exception e)
            {
                return StatusCode(500, "Fail to Create Order");
            }
        }
        [HttpGet]
        [Route("all")]
        [Authorize(Roles = "a1d06430-35af-433a-aefb-283f559059fb, 6fd0f97a-1522-475c-aba1-92f3ce5aeb04")]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_serivce.GetAll());
            }
            catch(Exception e)
            {
                return StatusCode(500, "Fail to Get Order");
            }
        }
        [HttpPost]
        [Route("process/{id}")]
        [Authorize(Roles = "a1d06430-35af-433a-aefb-283f559059fb, 6fd0f97a-1522-475c-aba1-92f3ce5aeb04")]
        public async Task<IActionResult> ProcessOrder(string id)
        {
            try
            {
                await _serivce.ProcessOrder(id);
                return Ok();
            }
            catch(Exception e)
            {
                return StatusCode(500, "Fail to Update Order");
            }
        }
        [HttpPost]
        [Route("{id}")]
        [Authorize(Roles = "116e0deb-f72f-45cf-8ef8-423748b8e9b1")]
        public async Task<IActionResult> CancelOrderUser(string id)
        {
            try
            {
                var claimsIdentity = this.User.Identity as ClaimsIdentity;
                string _userId = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
                await _serivce.CancelOrderUser(id, _userId);
                return Ok();
            }
            catch(Exception e)
            {
                return StatusCode(500, "Fail to Cancel Order");
            }
        }
        [HttpPost]
        [Route("cancel/{id}")]
        [Authorize(Roles = "a1d06430-35af-433a-aefb-283f559059fb, 6fd0f97a-1522-475c-aba1-92f3ce5aeb04")]
        public async Task<IActionResult> CancelOrder(string id)
        {
            try
            {
                await _serivce.CancelOrder(id);
                return Ok();
            }
            catch(Exception e)
            {
                return StatusCode(500, "Fail to Update Order");
            }
        }
        [HttpGet]
        [Route("")]
        [Authorize(Roles = "116e0deb-f72f-45cf-8ef8-423748b8e9b1, 6fd0f97a-1522-475c-aba1-92f3ce5aeb04")]
        public IActionResult GetByUserId()
        {
            try
            {
                var claimsIdentity = this.User.Identity as ClaimsIdentity;
                string _userId = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
                return Ok(_serivce.GetByUserId(_userId));
            }
            catch(Exception e)
            {
                return StatusCode(500, "Fail to Get Order");
            }
        }
        [HttpGet]
        [Route("Detail/{orderId}")]
        [Authorize(Roles = "116e0deb-f72f-45cf-8ef8-423748b8e9b1, 6fd0f97a-1522-475c-aba1-92f3ce5aeb04")]
        public async Task<IActionResult> GetById(string orderId)
        {
            try
            {
                var result = await _serivce.GetById(orderId);
                return Ok(result);
            }
            catch(Exception e)
            {
                return StatusCode(500, "Fail to Get Order");
            }
        }

        [HttpGet]
        [Route("income/{year}")]
        //[Authorize(Roles = "6fd0f97a-1522-475c-aba1-92f3ce5aeb04")]
        public IActionResult GetIncomeChart(int year)
        {
            try
            {
                var result = _serivce.GetIncomeChart(year);
                return Ok(result);
            }
            catch(Exception e)
            {
                return StatusCode(500, "Fail to Get Chart");
            }
        }
        [HttpGet]
        [Route("sold/{year}")]
        //[Authorize(Roles = "6fd0f97a-1522-475c-aba1-92f3ce5aeb04")]
        public IActionResult GetSoldChart(int year)
        {
            try
            {
                var result = _serivce.GetSoldChart(year);
                return Ok(result);
            }
            catch(Exception e)
            {
                return StatusCode(500, "Fail to Get Chart");
            }
        }
        [HttpGet]
        [Route("column-chart")]
        public IActionResult GetColChart(int year)
        {
            try
            {
                var incomechart = _serivce.GetIncomeChart(year);
                var soldchart = _serivce.GetSoldChart(year);
                var result = new List<List<ChartResponseModel>>
                {
                    incomechart,
                    soldchart,
                };
                return Ok(result);
            }
            catch(Exception e)
            {
                return StatusCode(500, "Fail to Get Column Chart");
            }
        }
        [HttpGet]
        [Route("brandcircle-chart")]
        [Authorize(Roles = "6fd0f97a-1522-475c-aba1-92f3ce5aeb04")]
        public IActionResult GetBrandChart(int month, int year)
        {
            try
            {
                var result = _serivce.GetBrandCircleChart(month, year);
                return Ok(result);
            }
            catch(Exception e)
            {
                return StatusCode(500, "Fail to Get Chart");
            }
        }
        [HttpGet]
        [Route("categoycircle-chart")]
        public IActionResult GetCategoryCharts(int month, int year)
        {
            try
            {
                var result = _serivce.GetCategoryCircleChart(month, year);
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Fail to Get Chart");
            }
        }
        [HttpGet]
        [Route("seriescircle-chart")]
        public IActionResult GetSeriesCharts(int month, int year)
        {
            try
            {
                var result = _serivce.GetSeriesCircleChart(month, year);
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Fail to Get Chart");
            }
        }
        [HttpGet]
        [Route("circle-chart")]
        public IActionResult GetCirChart(int month, int year)
        {
            try
            {
                var brandChart = _serivce.GetBrandCircleChart(month, year);
                var categoryChart = _serivce.GetCategoryCircleChart(month, year);
                var seriesChart = _serivce.GetSeriesCircleChart(month, year);
                var result = new List<List<ChartResponseModel>>
                {
                    brandChart,
                    categoryChart,
                    seriesChart,
                };
                return Ok(result);
            }
            catch(Exception e)
            {
                return StatusCode(500, "Fail to Get Circle Chart");
            }
        }
        [HttpGet]
        [Route("confirm")]
        public async Task<IActionResult> Confirm()
        {
            try
            {
                var param = Request.QueryString.ToString().Substring(0, Request.QueryString.ToString().IndexOf("signature") - 1);
                param = HttpUtility.UrlDecode(param);
                if (param == null) throw new Exception("");/*
                string secretKey = "K951B6PE1waDMi640xX08PD3vg6EkVlz";
                string signature = _momoSerivce.getSignature(param, secretKey);*/
                string status = "";
                var sign = Request.Query["signature"].ToString();
                MoMoRequest momoRequest = new MoMoRequest
                {
                    partnerCode = Request.Query["partnerCode"],
                    requestId = Request.Query["requestId"],
                    orderId = Request.Query["orderId"],
                    amount = Convert.ToInt64(Request.Query["amount"]),
                    responseTime = Convert.ToInt64(Request.Query["responseTime"]),
                    message = Request.Query["message"],
                    resultCode = Convert.ToInt32(Request.Query["resultCode"]),
                    orderInfo = Request.Query["orderInfo"],
                    orderType = Request.Query["orderType"],
                    transId = Convert.ToInt64(Request.Query["transId"]),
                    payType = Request.Query["payType"],
                    extraData = Request.Query["extraData"]
                };
                await _momoSerivce.ConfirmResponse(momoRequest);/*
                if (signature == sign)
                {
                    status = "Signature correct";
                }*/
                status = momoRequest.message;
                return Ok(status);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Fail to Confirm");
            }
        }
    }
}
