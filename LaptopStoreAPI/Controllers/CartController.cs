using LaptopStore.Service.RequestModels;
using LaptopStore.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LaptopStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "116e0deb-f72f-45cf-8ef8-423748b8e9b1, 6fd0f97a-1522-475c-aba1-92f3ce5aeb04")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _service;

        public CartController(ICartService service)
        {
            _service = service;
        }
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Add(CartRequestModel request)
        {
            try
            {
                var claimsIdentity = this.User.Identity as ClaimsIdentity;
                string _userId = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
                return Ok(await _service.Add(request, _userId));
            }
            catch(Exception e)
            {
                return StatusCode(500, "Fail to Add Cart");
            }
        }
        [HttpPut]
        [Route("")]
        public IActionResult Update(CartRequestModel request)
        {
            try
            {
                var claimsIdentity = this.User.Identity as ClaimsIdentity;
                string _userId = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
                return Ok(_service.Update(request, _userId));
            }
            catch(Exception e)
            {
                return StatusCode(500, "Fail to Update Cart");
            }
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var claimsIdentity = this.User.Identity as ClaimsIdentity;
                string _userId = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
                await _service.Delete(id, _userId);
                return Ok();
            }
            catch(Exception e)
            {
                return StatusCode(500, "Fail to Delete Product");
            }
        }
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetByUserId()
        {
            try
            {
                var claimsIdentity = this.User.Identity as ClaimsIdentity;
                string _userId = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
                return Ok(await _service.GetByUserId(_userId));
            }
            catch(Exception e)
            {
                return StatusCode(500, "Fail to Get Cart");
            }
        }
    }
}
