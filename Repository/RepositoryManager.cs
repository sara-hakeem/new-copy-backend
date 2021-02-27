using Contracts.Repository;
using Entities.Data;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        public RepositoryContext repositoryContext;
        public IUserfileRepository userfile;
        public IUserRepository user;
        public RepositoryManager(RepositoryContext repositoryContext)
        {
            this.repositoryContext = repositoryContext;
        }
        public IUserfileRepository userFileRepository
        {
            get
            {
                if (userfile == null)
                {
                    userfile = new UserFileRepository(repositoryContext);
                }
                return userfile;
            }
        }
        public IUserRepository userRepository
        {
            get
            {
                if (user == null)
                {
                    user = new UserRepository(repositoryContext);
                }
                return user;
            }


        }


        public void Save()
        {
            repositoryContext.SaveChanges();
        }
    }
}
