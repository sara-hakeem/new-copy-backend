using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
   public  class UserFiles
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public User User { get; set; }
        public string Date { get; set; }
        public string Status { get; set; }//ready or send to admin
    }
}
