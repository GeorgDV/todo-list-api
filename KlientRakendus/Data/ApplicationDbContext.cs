using KlientRakendus.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KlientRakendus.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace KlientRakendus.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        { }

        //public DbSet<TaskListItemModel> Tasks { get; set; }
        public DbSet<KlientRakendus.Data.TaskModel> Tasks { get; set; }
    }
}
