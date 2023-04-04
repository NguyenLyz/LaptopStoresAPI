using LaptopStore.Service.RequestModels;
using LaptopStore.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LaptopStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvertisementController : ControllerBase
    {
        private readonly IAdvertisementService _service;

        public AdvertisementController(IAdvertisementService service)
        {
            _service = service;
        }
        [HttpPost]
        [Route("")]
        [Authorize(Roles = "6fd0f97a-1522-475c-aba1-92f3ce5aeb04")]
        public async Task<IActionResult> Add(AdvertisementRequestModel request)
        {
            try
            {
                return Ok(await _service.Add(request));
            }
            catch (Exception e)
            {
                return StatusCode(500, "Fail to Create Advertisement");
            }
        }
        [HttpPut]
        [Route("")]
        [Authorize(Roles = "6fd0f97a-1522-475c-aba1-92f3ce5aeb04")]
        public IActionResult Update(AdvertisementRequestModel request)
        {
            try
            {
                return Ok(_service.Update(request));
            }
            catch (Exception e)
            {
                return StatusCode(500, "Fail to Update Advertisement");
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
            catch (Exception e)
            {
                return StatusCode(500, "Fail to Delete Advertisement");
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
            catch (Exception e)
            {
                return StatusCode(500, "Fail to Get Advertisement");
            }
        }
        [HttpGet]
        [Route("{id}")]
        [AllowAnonymous]
        public IActionResult GetById(int id)
        {
            try
            {
                return Ok(_service.GetById(id));
            }
            catch(Exception e)
            {
                return StatusCode(500, "Fail to Get Advertisement");
            }
        }
        [HttpGet]
        [Route("show")]
        [AllowAnonymous]
        public IActionResult Show()
        {
            try
            {
                return Ok(_service.Show());
            }
            catch(Exception e)
            {
                return StatusCode(500, "Fail to Get Advertisement");
            }
        }
    }
}
