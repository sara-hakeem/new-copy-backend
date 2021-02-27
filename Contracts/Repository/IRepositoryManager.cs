using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Repository
{
    public interface IRepositoryManager
    {
        public IUserfileRepository userFileRepository { get;}
        public IUserRepository userRepository { get;}


        public void Save();
    }


   
}
