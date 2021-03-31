using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Contracts.Repository;
using dizzlr_task.Extention;
using Entities.HelperModel;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repository;

namespace dizzlr_task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        IRepositoryManager repositoryManager;
        public readonly IConfiguration _Configuration;
        public UserController(IRepositoryManager repositoryManager,IConfiguration configuration)
        {
            this.repositoryManager = repositoryManager;
            this._Configuration = configuration;
        }
        [HttpPost("Login")]
        public IActionResult Login([FromBody]UserLoginModel n)
        {
            n.Password = n.Password.MD5Hash();
            User u = repositoryManager.userRepository.Login(n);


            if (u == null)
            {
                var type = new
                {
                    data = u,
                    msg = " user or password missmatch",
                    role = "0"
                };
                return Ok(type);
            }
            else if (u.Role == 0)
            {
                var type = new
                {
                    data = u,
                    msg = " user login sucess",
                    role = "1"
                };
                return Ok(type);

            }
            else
            {
                var type = new
                {
                    data = u,
                    msg = " you are admin",
                    role = "2"
                };
               // Response.data.token = generateToken(new list<claims>(){new claim(claims.claimtype.role)});
                return Ok(type);
            }

        }
        [HttpPost("Register")]
        public IActionResult Register([FromBody]UserRegisterModel r)
        {
            r.Password = r.Password.MD5Hash();//make md5 incryption to password
            User u = repositoryManager.userRepository.Register(r);
            repositoryManager.Save();

            return Ok(u);

        }


        private string generateToken(List<Claim> claims)
        {

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Configuration["jwtoptions:key"]));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokeOptions = new JwtSecurityToken(
                issuer: _Configuration["jwtoptions:issuer"],
                audience: _Configuration["jwtoptions:Audience"],
               claims: claims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: signinCredentials
            );
        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
        return tokenString;
    }
    }

   
}