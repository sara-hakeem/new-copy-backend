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
            List<UserFiles> f = repositoryManager.userFileRepository.GetFileByUserId(Id);
            return Ok(f);

        }

        [HttpGet("GetFileToAdmin")]
        public IActionResult GetFileToAdmin()
        {
            List<UserFiles> f = repositoryManager.userFileRepository.GetFileToAdmin();
            return Ok(f);

        }

        [HttpGet("SendFile")]
        public IActionResult SendFile(Guid id)
        {
            bool result = repositoryManager.userFileRepository.SendFile(id);
            repositoryManager.Save();
            return Ok(result);

        }

        [HttpPost("UploadFiles")]
        public async Task<IActionResult> UploadFiles(IFormFile file, Guid id, string filename)
        {
            User u = repositoryManager.userRepository.GetUseryId(id);
            if (u == null)
            {
                return Ok("user not found");
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
            return Ok("file uploaded");
        }


        [HttpGet("UploadFilesByLink")]
        public IActionResult UploadFiles(string filepath, Guid id, string filename)
        {
            User u = repositoryManager.userRepository.GetUseryId(id);
            if (u == null)
            {
                return Ok("user not found");
            }

            repositoryManager.userFileRepository.UploadFiles(filepath, u, filename);
            repositoryManager.Save();

            return Ok("file uploaded");
        }
    }
}