using System;
using System.Collections.Generic;
using System.Text;

namespace UserCenter.Services.Entities
{
   public  class AppInfo:BaseModel
    {
        public string Name { get; set; }
        public string AppKey { get; set; }
        public string AppSecret { get; set; }
        /// <summary>
        /// 是否为启用状态
        /// </summary>
        public bool IsEnabled { get; set; }
    }
}
