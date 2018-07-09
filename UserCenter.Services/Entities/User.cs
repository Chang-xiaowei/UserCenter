using System;
using System.Collections.Generic;
using System.Text;

namespace UserCenter.Services.Entities
{
   public  class User:BaseModel
    {
        public string PhoneNum { get; set; }
        public string  NickName { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public virtual ICollection<UserGroup> Groups { get; set; } = new List<UserGroup>();

    }
}
