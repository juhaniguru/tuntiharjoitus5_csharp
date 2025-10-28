using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Interfaces;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IAuthService _authService) : ControllerBase
    {
        

        [HttpPost("login")]
        public async Task<ActionResult<LoginRes>> Login(LoginReq request)
        {
            // nopeasti katsottuna koodi näyttää olevan kunnossa, mutta controllerissa kaikki onkin paikallaan
            // ongelma on authRepossa (AuthSQLiteRepository)
            try
            {
                var res = await _authService.Login(request);
                return Ok(res);
            } catch(Exception e)
            {
                return Problem(title: "Error logging user in", detail: e.Message, statusCode: 500);
            }



            

            
        }
    }
    
    
}