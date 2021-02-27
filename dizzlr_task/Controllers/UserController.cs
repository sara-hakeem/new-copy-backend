using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.Repository;
using dizzlr_task.Extention;
using Entities.HelperModel;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace dizzlr_task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        IRepositoryManager repositoryManager;
        public UserController(IRepositoryManager repositoryManager)
        {
            this.repositoryManager = repositoryManager;
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
                    data =u,
                    msg = " user or password missmatch"
                };
                return Ok(type);
            }
            else
            {
                var type = new
                {
                    data = u,
                    msg = " user login sucess"
                };
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
    }
}