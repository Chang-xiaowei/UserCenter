using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace UserCenter.OpenAPI.Controllers.v1
{
    public class DefaultController : ApiController
    {
        public string Get(string name)
        {
            
                return "Ok" + name;
            
        }
    }
}
