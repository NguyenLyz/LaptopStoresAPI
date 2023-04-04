using LaptopStore.Service.RequestModels;
using LaptopStore.Service.Services.Interfaces;
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
        [Route("cancel/{id}")]
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
        [Route("brandchart/{year}")]
        public IActionResult GetBrandChart(int year)
        {
            try
            {
                var result = _serivce.GetBrandCircleChart(year);
                return Ok(result);
            }
            catch(Exception e)
            {
                return StatusCode(500, "Fail to Get Chart");
            }
        }
    }
}
