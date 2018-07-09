using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Text;
using UserCenter.Services.Entities;

namespace UserCenter.Services.Config
{
  public  class UserConfig:EntityTypeConfiguration<User>
    {
      public UserConfig()
        {
            this.ToTable("T_User");
            HasMany(u => u.Groups).WithMany(e => e.Users).Map(e => e.ToTable("T_GroupUsers").MapLeftKey("UserId").MapRightKey("GroupId"));
            Property(u => u.PhoneNum).IsRequired().HasMaxLength(50);
            Property(u => u.NickName).IsRequired().HasMaxLength(20);
            Property(u => u.PasswordHash).IsRequired().HasMaxLength(100);
            Property(u => u.PasswordSalt).IsRequired().HasMaxLength(20);

        }
    }
}
