﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVCApplicationTest.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCApplicationTest.DAL.Contexts
{
    public class MVCApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public MVCApplicationDbContext(DbContextOptions<MVCApplicationDbContext> options) : base(options)
        {

        }


        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}
