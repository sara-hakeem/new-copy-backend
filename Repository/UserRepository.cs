using Contracts.Repository;
using Entities.Data;
using Entities.HelperModel;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class UserRepository :RepositoryBase<User>, IUserRepository
    {
        RepositoryContext repositoryContext;
        public UserRepository(RepositoryContext repositoryContext):base(repositoryContext)
        {
            this.repositoryContext = repositoryContext;
        }

        public User GetUseryId(Guid id)
        {
            return FindAllByCondition(u => u.Id == id).FirstOrDefault();
        }

        public User Login(UserLoginModel userLogin)
        {
        return FindAll().Where(u=>u.name==userLogin.UserName&&u.Password==userLogin.Password).FirstOrDefault();
        }

        public User Register(UserRegisterModel userRegister)
        {
            User user = new User();
            user.name = userRegister.name;
            user.Password= userRegister.Password;
            user.Phone= userRegister.Phone;
            user.Role = 0;

            Create(user);
            return user;
        }

       
    }
}
