using Entities.HelperModel;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Repository
{
   public interface IUserRepository
    {
        public User Login(UserLoginModel userLogin);
        public User Register(UserRegisterModel userRegister );
        public User GetUseryId(Guid id);


    }
}
