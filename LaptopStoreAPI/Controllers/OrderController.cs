using LaptopStore.Service.RequestModels;
using LaptopStore.Service.ResponseModels;
using LaptopStore.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LaptopStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _serivce;

        public OrderController(IOrderService serivce)
        {
            _serivce = serivce;
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
                return Ok(await _serivce.Add(request, _userId));
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
        [HttpPut]
        [Route("process/{id}")]
        [Authorize(Roles = "a1d06430-35af-433a-aefb-283f559059fb, 6fd0f97a-1522-475c-aba1-92f3ce5aeb04")]
        public async Task<IActionResult> ProcessOrder(int id)
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
        [HttpPut]
        [Route("{id}")]
        [Authorize(Roles = "116e0deb-f72f-45cf-8ef8-423748b8e9b1")]
        public async Task<IActionResult> CancelOrderUser(int id)
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
                return StatusCode(500, "Fail to Candel Order");
            }
        }
        [HttpPut]
        [Route("cancel/{id}")]
        [Authorize(Roles = "a1d06430-35af-433a-aefb-283f559059fb, 6fd0f97a-1522-475c-aba1-92f3ce5aeb04")]
        public async Task<IActionResult> CancelOrder(int id)
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
        [Route("Detail/{orderId:int}")]
        [Authorize(Roles = "116e0deb-f72f-45cf-8ef8-423748b8e9b1, 6fd0f97a-1522-475c-aba1-92f3ce5aeb04")]
        public async Task<IActionResult> GetById(int orderId)
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
        [Route("brandcirclechart")]
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
        [Route("categoycirclechart")]
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
        [Route("seriescirclechart")]
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
    }
}
