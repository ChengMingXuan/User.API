using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.API.Models;

namespace User.API.Data
{
    public class UserContext : DbContext
    {
        public   UserContext(DbContextOptions<UserContext> options):base(options){

         }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //指定数据库表名称 Users
            modelBuilder.Entity<AppUser>().ToTable("Users").HasKey(u => u.Id);


            base.OnModelCreating(modelBuilder);
        }
        public DbSet<AppUser> Users { get; set; }
    }
}
