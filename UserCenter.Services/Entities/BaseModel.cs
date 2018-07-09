using System;
using System.Collections.Generic;
using System.Text;

namespace UserCenter.Services.Entities
{
   public abstract class BaseModel
    {
        public long Id { get; set; }
        public DateTime CreateDateTime { get; set; } = DateTime.Now;
    }
}
