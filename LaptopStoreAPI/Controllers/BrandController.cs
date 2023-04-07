using LaptopStore.Service.RequestModels;
using LaptopStore.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LaptopStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "6fd0f97a-1522-475c-aba1-92f3ce5aeb04")]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _service;

        public BrandController(IBrandService service)
        {
            _service = service;
        }
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Add(BrandRequestModel request)
        {
            try
            {
                return Ok(await _service.Add(request));
            }
            catch(Exception e)
            {
                return StatusCode(500, "Fail to Create Brand");
            }
        }
        [HttpPut]
        [Route("")]
        public async Task<IActionResult> Update(BrandRequestModel request)
        {
            try
            {
                return Ok(await _service.Update(request));
            }
            catch(Exception e)
            {
                return StatusCode(500, "Fail to Update Brand");
            }
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.Delete(id);
                return Ok();
            }
            catch(Exception e)
            {
                return StatusCode(500, "Fail to Delete Brand");
            }
        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                return Ok(_service.GetById(id));
            }
            catch(Exception e)
            {
                return StatusCode(500, "Fail to Get Brand");
            }
        }
        [HttpGet]
        [Route("")]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_service.GetAll());
            }
            catch(Exception e)
            {
                return StatusCode(500, "Fail to All Brand");
            }
        }
    }
}
