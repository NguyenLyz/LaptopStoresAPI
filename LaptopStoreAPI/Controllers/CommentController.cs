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
    [Authorize(Roles = "116e0deb-f72f-45cf-8ef8-423748b8e9b1")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _serivce;

        public CommentController(ICommentService serivce)
        {
            _serivce = serivce;
        }

        [HttpPost]
        [Route("")]
        public IActionResult Add(CommentRequestModel request)
        {
            try
            {
                var claimsIdentity = this.User.Identity as ClaimsIdentity;
                string _userId = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
                return Ok(_serivce.Add(request, _userId));
            }
            catch(Exception e)
            {
                return StatusCode(500, "Fail to Create Comment");
            }
        }
        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var claimsIdentity = this.User.Identity as ClaimsIdentity;
                string _userId = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
                _serivce.Delete(id, _userId);
                return Ok();
            }
            catch(Exception e)
            {
                return StatusCode(500, "Fail to Delete Comment");
            }
        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetByProductId(int productid)
        {
            try
            {
                return Ok(_serivce.GetByProductId(productid));
            }
            catch(Exception e)
            {
                return StatusCode(500, " Fail to Get Comment");
            }
        }
    }
}
