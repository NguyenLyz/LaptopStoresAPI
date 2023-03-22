﻿using LaptopStore.Service.RequestModels;
using LaptopStore.Service.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LaptopStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _service;

        public ImageController(IImageService service)
        {
            _service = service;
        }
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Add(ImageRequestModel request)
        {
            try
            {
                return Ok(await _service.Add(request));
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        [HttpPut]
        [Route("")]
        public async Task<IActionResult> Update(ImageRequestModel request)
        {
            try
            {
                return Ok(await _service.Update(request));
            }
            catch(Exception e)
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
            catch(Exception e)
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
            catch(Exception e)
            {
                throw e;
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
                throw e;
            }
        }
    }
}
