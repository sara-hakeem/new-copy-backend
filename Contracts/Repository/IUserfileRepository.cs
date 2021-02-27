using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Repository
{
  public   interface IUserfileRepository
    {
        public UserFiles UploadFiles(string FilePath,User User,string filename);
        public List<UserFiles> GetFileByUserId(Guid Id);
        public bool SendFile(Guid id);

        public List<UserFiles> GetFileToAdmin();

        

    }
}
