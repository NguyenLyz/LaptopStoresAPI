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
    public class NoticeController : ControllerBase
    {
        private readonly INoticeService _service;

        public NoticeController(INoticeService service)
        {
            _service = service;
        }
        [HttpPost]
        [Route("")]
        [Authorize(Roles = "6fd0f97a-1522-475c-aba1-92f3ce5aeb04")]
        public async Task<IActionResult> Add(NoticeRequestModel request)
        {
            try
            {
                return Ok(await _service.Add(request));
            }
            catch(Exception e)
            {
                return StatusCode(500, "Fail to Create Notice");
            }
        }
        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "6fd0f97a-1522-475c-aba1-92f3ce5aeb04")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.Delete(id);
                return Ok();
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        [HttpGet]
        [Route("")]
        [Authorize(Roles = "6fd0f97a-1522-475c-aba1-92f3ce5aeb04")]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_service.GetAll());
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = "116e0deb-f72f-45cf-8ef8-423748b8e9b1, a1d06430-35af-433a-aefb-283f559059fb, 6fd0f97a-1522-475c-aba1-92f3ce5aeb04")]
        public IActionResult GetById(int id)
        {
            try
            {
                return Ok(_service.GetById(id));
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        [HttpGet]
        [Route("Show")]
        [Authorize(Roles = "116e0deb-f72f-45cf-8ef8-423748b8e9b1, a1d06430-35af-433a-aefb-283f559059fb, 6fd0f97a-1522-475c-aba1-92f3ce5aeb04")]
        public IActionResult Show()
        {
            try
            {
                var claimsIdentity = this.User.Identity as ClaimsIdentity;
                string _userId = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
                string _roleId = claimsIdentity.FindFirst(ClaimTypes.Role)?.Value;
                return Ok(_service.Show(_userId, _roleId));
            }
            catch(Exception e)
            {
                return StatusCode(500, "Fail to Get Notice");
            }
        }
    }
}
