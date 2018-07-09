using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Reflection;
using System.Text;
using UserCenter.Services.Entities;

namespace UserCenter.Services
{
   public class UCDbContext:DbContext
    {
        public UCDbContext():base("name=conn")
        {
            Database.CreateIfNotExists();
           // Database.SetInitializer<UCDbContext>(null);
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());
        }
        public DbSet<User> Users { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<AppInfo> AppInfos { get; set; }
    }
}
