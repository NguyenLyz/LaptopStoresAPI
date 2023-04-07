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
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }
        [HttpPost]
        [Route("log-in")]
        [AllowAnonymous]
        public IActionResult Login(LoginRequestModel request)
        {
            try
            {
                var token = _service.Login(request);

                if(token == null)
                {
                    return Unauthorized();
                }

                return Ok(token);
            }
            catch(Exception e)
            {
                return StatusCode(500, "Fail to Login");
            }
        }
        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public IActionResult Register(RegisterRequestModel request)
        {
            try
            {
                var token = _service.Register(request);

                if( token == null)
                {
                    return Unauthorized();
                }

                return Ok(token);
            }
            catch(Exception e)
            {
                return StatusCode(500, "Fail to Register");
            }
        }
        [HttpPut]
        [Route("change-password")]
        [Authorize(Roles = "116e0deb-f72f-45cf-8ef8-423748b8e9b1, a1d06430-35af-433a-aefb-283f559059fb, 6fd0f97a-1522-475c-aba1-92f3ce5aeb04")]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequestModel request)
        {
            try
            {
                var claimsIdentity = this.User.Identity as ClaimsIdentity;
                string _userId = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
                await _service.ChangePassword(request, _userId);
                return Ok();
            }
            catch(Exception e)
            {
                return StatusCode(500, "Fail to ChangePassword");
            }
        }
        [HttpGet]
        [Route("profile")]
        [Authorize(Roles = "116e0deb-f72f-45cf-8ef8-423748b8e9b1, a1d06430-35af-433a-aefb-283f559059fb, 6fd0f97a-1522-475c-aba1-92f3ce5aeb04")]
        public IActionResult GetProfile()
        {
            try
            {
                var claimsIdentity = this.User.Identity as ClaimsIdentity;
                string _userId = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
                var user = _service.GetProfile(_userId);
                return Ok(user);
            }
            catch(Exception e)
            {
                return StatusCode(500, "Fail to GetProfile");
            }
        }
        [HttpPut]
        [Route("")]
        [Authorize(Roles = "116e0deb-f72f-45cf-8ef8-423748b8e9b1, a1d06430-35af-433a-aefb-283f559059fb, 6fd0f97a-1522-475c-aba1-92f3ce5aeb04")]
        public IActionResult UpdateImg(string img)
        {
            try
            {
                var claimsIdentity = this.User.Identity as ClaimsIdentity;
                string _userId = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
                var user = _service.UpdateImg(img, _userId);
                return Ok(user);
            }
            catch(Exception e)
            {
                return StatusCode(500, "Fail to Update Image");
            }
        }
    }
}
