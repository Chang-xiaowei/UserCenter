using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Text;
using UserCenter.Services.Entities;

namespace UserCenter.Services.Config
{
   public  class UserGroupConfig:EntityTypeConfiguration<UserGroup>
    {
        public UserGroupConfig()
        {
            this.ToTable("T_UserGroup");
            Property(e => e.Name).IsRequired().HasMaxLength(50);
        }
    }
}
