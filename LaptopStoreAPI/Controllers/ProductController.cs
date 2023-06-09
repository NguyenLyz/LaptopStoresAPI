﻿using LaptopStore.Service.RequestModels;
using LaptopStore.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LaptopStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }
        [HttpPost]
        [Route("")]
        [Authorize(Roles = "6fd0f97a-1522-475c-aba1-92f3ce5aeb04")]
        public async Task<IActionResult> Add(ProductResquestModel request)
        {
            try
            {
                return Ok(await _service.Add(request));
            }
            catch(Exception e)
            {
                return StatusCode(500, "Fail to Creat Product");
            }
        }
        [HttpPut]
        [Route("")]
        [Authorize(Roles = "6fd0f97a-1522-475c-aba1-92f3ce5aeb04")]
        public async Task<IActionResult> Update(ProductResquestModel request)
        {
            try
            {
                return Ok(await _service.Update(request));
            }
            catch(Exception e)
            {
                return StatusCode(500, "Fail to Update Product");
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
                return StatusCode(500, "Fail to Delete Product");
            }
        }
        [HttpGet]
        [Route("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var claimsIdentity = this.User.Identity as ClaimsIdentity;
                string _userId = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
                return Ok(await _service.GetById(id, _userId));
            }
            catch(Exception e)
            {
                return StatusCode(500, "Fail to Get Product");
            }
        }
        [HttpGet]
        [Route("slug/{slug}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetBySlug(string slug)
        {
            try
            {
                var claimsIdentity = this.User.Identity as ClaimsIdentity;
                string _userId = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
                return Ok(await _service.GetBySlug(slug, _userId));
            }
            catch (Exception e)
            {
                return StatusCode(500, "Fail to Get Product");
            }
        }
        [HttpPost]
        [Route("filter")]
        [AllowAnonymous]
        public async Task<IActionResult> Filter(FilterRequestModel request)
        {
            try
            {
                return Ok(await _service.Filter(request));
            }
            catch(Exception e)
            {
                return StatusCode(500, "Fail to Get Product");
            }
        }
        [HttpGet]
        [Route("show")]
        [AllowAnonymous]
        public async Task<IActionResult> Show()
        {
            try
            {
                var result = new Dictionary<string, object>();
                result.Add("Best Seller", await _service.ShowBestSeller());
                result.Add("Newest Product", await _service.ShowNewestProduct());
                return Ok(result);
            }
            catch(Exception e)
            {
                return StatusCode(500, "Fail to Show");
            }
        }
    }
}
