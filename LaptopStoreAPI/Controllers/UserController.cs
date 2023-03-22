﻿using LaptopStore.Service.RequestModels;
using LaptopStore.Service.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LaptopStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Add(UserRequestModel request)
        {
            try
            {
                if(await _service.Add(request))
                {
                    return Ok();
                }
                throw new Exception();
            }
            catch(Exception e)
            {
                return StatusCode(500, "Fail to Create User");
            }
        }
        [HttpPut]
        [Route("")]
        public async Task<IActionResult> Update(UserRequestModel request)
        {
            try
            {
                if(await _service.Update(request))
                {
                    return Ok();
                }
                throw new Exception();
            }
            catch(Exception e)
            {
                return StatusCode(500, "Fail to Update User");
            }
        }
        [HttpDelete]
        [Route("")]
        public async Task<IActionResult> Delete(UserRequestModel request)
        {
            try
            {
                if(await _service.Delete(request.Id))
                {
                    return Ok();
                }
                throw new Exception();
            }
            catch(Exception e)
            {
                return StatusCode(500, "Fail to Delete User");
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
                return StatusCode(500, "Fail to Get User");
            }
        }
    }
}
