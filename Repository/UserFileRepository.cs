using Contracts.Repository;
using Entities.Data;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class UserFileRepository : RepositoryBase<UserFiles>, IUserfileRepository
    {
        RepositoryContext repositoryContext;
        public UserFileRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
            this.repositoryContext = repositoryContext;
        }
        public List<UserFiles> GetFileByUserId(Guid Id)//
        {
            return FindAllByCondition(u => u.User.Id == Id).Include(o => o.User).ToList();
        }

        public List<UserFiles> GetFileToAdmin()
        {
            return FindAllByCondition(u => u.Status == "Send").Include(o => o.User).ToList();
        }

        public bool SendFile(Guid id)
        {
            UserFiles file = FindAllByCondition(u => u.Id == id).SingleOrDefault();
            if (file == null) { return false; }
            file.Status = "Send";
            Update(file);
            return true;
        }

        public UserFiles UploadFiles(string FilePath, User User, string filename)
        {

            UserFiles file = new UserFiles();
            file.User = User;
            file.FilePath = FilePath;
            file.Date = DateTime.Now.ToString();
            file.Status = "Ready";
            file.FileName = filename;
            Create(file);
            return file;

        }
    }
}
