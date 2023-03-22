using LaptopStore.Service.RequestModels;
using LaptopStore.Service.Services.Interfaces;
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
        public async Task<IActionResult> Add(AdvertisementRequestModel request)
        {
            try
            {
                return Ok(await _service.Add(request));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        [HttpPut]
        [Route("")]
        public IActionResult Update(AdvertisementRequestModel request)
        {
            try
            {
                return Ok(_service.Update(request));
            }
            catch (Exception e)
            {
                throw e;
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
            catch (Exception e)
            {
                throw e;
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
            catch (Exception e)
            {
                throw e;
            }
        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetByeId(int id)
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
    }
}
