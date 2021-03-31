using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Contracts.Repository;
using Entities.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace dizzlr_task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserFileController : ControllerBase
    {
        private IWebHostEnvironment webHost;
        IRepositoryManager repositoryManager;
        public UserFileController(IRepositoryManager repositoryManager, IWebHostEnvironment webHost)
        {
            this.repositoryManager = repositoryManager;
            this.webHost = webHost;
        }

        [HttpGet("getFileByUserId")]
        public IActionResult getFileByUserId(Guid Id)
        {
            User u = repositoryManager.userRepository.GetUseryId(Id);
            if (u == null || u.Role == 1)
            {
                var type = new
                {
                    msg = "user not user ",
                    sucess = false
                };
                return Ok(type);
            }
            else
            {
                List<UserFiles> f = repositoryManager.userFileRepository.GetFileByUserId(Id);
                var type = new
                {
                    msg = "ok ",
                    data = f,
                    sucess = true
                };
                return Ok(type);
            }


        }

        [HttpGet("GetFileToAdmin")]
        public IActionResult GetFileToAdmin(Guid id)
        {
            User u = repositoryManager.userRepository.GetUseryId(id);
            if (u == null || u.Role == 0)
            {
                var type = new
                {
                    msg = "user not admin",
                    sucess = false
                };
                return Ok(type);
            }

            else
            {
                List<UserFiles> f = repositoryManager.userFileRepository.GetFileToAdmin();
                var type = new
                {
                    msg = "ok ",
                    data = f,
                    sucess = true
                };
                return Ok(type);
            }

        }

        [HttpGet("SendFile")]
        public IActionResult SendFile(Guid uid , Guid fid)
        {
            User u = repositoryManager.userRepository.GetUseryId(uid);
            if (u == null)
            {
                var type = new
                {
                    msg = "faild ",
                    sucess = false
                };
                return Ok(type);
            }
            else
            {
                bool result = repositoryManager.userFileRepository.SendFile(fid);
                repositoryManager.Save();
                var type = new
                {
                    msg = "ok ",
                    sucess = true
                };
                return Ok(type);

            }


        }

        [HttpPost("UploadFiles")]
        public async Task<IActionResult> UploadFiles(IFormFile file, Guid id, string filename)
        {
            User u = repositoryManager.userRepository.GetUseryId(id);
            if (u == null)
            {
                var tt = new
                {
                    msg = "faild ",
                    sucess = false
                };
                return Ok(tt);
            }
            string uploadfolder = Path.Combine(webHost.ContentRootPath, "uploads");
            string filen = Guid.NewGuid().ToString() + "_" + id.ToString() + "_" + filename + "_" + file.FileName;
            string filepath = Path.Combine(uploadfolder, filen);
            using (var stream = new FileStream(filepath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
                repositoryManager.userFileRepository.UploadFiles(filepath, u, filename);
                repositoryManager.Save();

            }
            var type = new
            {
                msg = "ok ",
                sucess = true
            };
            return Ok(type);
        }


        [HttpGet("UploadFilesByLink")]
        public IActionResult UploadFiles(string filepath, Guid id, string filename)
        {
            User u = repositoryManager.userRepository.GetUseryId(id);
            if (u == null)
            {
                {
                    var tt = new
                    {
                        msg = "faild ",
                        sucess = false
                    };
                    return Ok(tt);
                }
            }

            repositoryManager.userFileRepository.UploadFiles(filepath, u, filename);
            repositoryManager.Save();

            var type = new
            {
                msg = "ok ",
                sucess = true
            };
            return Ok(type);
        }
    }
}