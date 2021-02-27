﻿using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Data
{
   public  class RepositoryContext:DbContext
    {
        public RepositoryContext(DbContextOptions options) : base(options)
        {

        }

        
        public DbSet<User> Users { get; set; }
        public DbSet<UserFiles> UserFile { get; set; }
    }
}
