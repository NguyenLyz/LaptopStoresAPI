﻿using LaptopStore.Service.RequestModels;
using LaptopStore.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LaptopStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoryController(ICategoryService service)
        {
            _service = service;
        }
        [HttpPost]
        [Route("")]
        [Authorize(Roles = "6fd0f97a-1522-475c-aba1-92f3ce5aeb04")]
        public async Task<IActionResult> Add(CategoryRequestModel request)
        {
            try
            {
                return Ok(await _service.Add(request));
            }
            catch(Exception e)
            {
                return StatusCode(500, "Fail to Create Category");
            }
        }
        [HttpPut]
        [Route("")]
        [Authorize(Roles = "6fd0f97a-1522-475c-aba1-92f3ce5aeb04")]
        public async Task<IActionResult> Update(CategoryRequestModel request)
        {
            try
            {
                return Ok(await _service.Update(request));
            }
            catch(Exception e)
            {
                return StatusCode(500, "Fail to Update Catefory");
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
                return StatusCode(500, "Fail to Delete Catefory");
            }
        }
        [HttpGet]
        [Route("")]
        [AllowAnonymous]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_service.GetAll());
            }
            catch(Exception e)
            {
                return StatusCode(500, "Fail to Get Catefory");
            }
        }
        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = "6fd0f97a-1522-475c-aba1-92f3ce5aeb04")]
        public IActionResult GetById(int id)
        {
            try
            {
                return Ok(_service.GetById(id));
            }
            catch(Exception e)
            {
                return StatusCode(500, "Fail to Get Catefory");
            }
        }
    }
}
