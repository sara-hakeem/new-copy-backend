using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public class User
    {
       public Guid Id { get; set; }
        public string name { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
      //  public List<UserFiles> Files { get; set; }
        public int Role { get; set; }//0 for admin, 1 for user
    }
}
