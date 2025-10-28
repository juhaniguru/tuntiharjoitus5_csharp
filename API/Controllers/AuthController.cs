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
    public class AuthController(IAuthRepo _authRepo, ILogRepo _logRepo) : ControllerBase
    {
        

        [HttpPost("login")]
        public async Task<ActionResult<LoginRes>> Login(LoginReq request)
        {
            // nopeasti katsottuna koodi näyttää olevan kunnossa, mutta controllerissa kaikki onkin paikallaan
            // ongelma on authRepossa (AuthSQLiteRepository)
            var user = await _authRepo.Login(request.UserName, request.Password);
            if (user == null)
            {
                return NotFound(new
                {
                    Message = "user not found"
                });
            }

            if (request.Password != user.Password)
            {
                return NotFound(new
                {
                    Message = "user not found"
                });
            }


            await _logRepo.Create(new AddLogEntryReq
            {
                UserName = user.Username
            });

            return new LoginRes
            {
                Token = "jwt"
            };



            

            
        }
    }
    
    
}