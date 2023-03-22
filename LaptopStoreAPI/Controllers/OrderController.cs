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
        [Route("All")]
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
        [Route("")]
        public async Task<IActionResult> UpdateOrderStatus(OrderRequestModel request)
        {
            try
            {
                await _serivce.UpdateOrderStatus(request.Id, request.Status);
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
                return StatusCode(500, " Fail to Get Order");
            }
        }
    }
}
