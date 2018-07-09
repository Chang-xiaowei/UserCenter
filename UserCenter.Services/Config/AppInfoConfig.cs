using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Text;
using UserCenter.Services.Entities;

namespace UserCenter.Services.Config
{
   public class AppInfoConfig:EntityTypeConfiguration<AppInfo>
    {
        public AppInfoConfig()
        {
            this.ToTable("T_AppInfo");
            Property(a => a.Name).IsRequired().HasMaxLength(100);
            Property(a => a.AppKey).IsRequired().HasMaxLength(100);
            Property(a => a.AppSecret).IsRequired().HasMaxLength(100);
            Property(a => a.IsEnabled).IsRequired();
        }
    }
}
