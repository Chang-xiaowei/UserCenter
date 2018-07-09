using System;
using System.Collections.Generic;
using System.Text;

namespace UserCenter.Services.Entities
{
  public  class UserGroup:BaseModel
    {
        public string Name { get; set; }
        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}
