using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UserCenter.Services;

namespace UserCenter.OpenAPI.Controllers
{
    public class DefaultController : ApiController
    {
        public string Get(int id)
        {
            using (UCDbContext ctx=new UCDbContext())
            {
                ctx.Database.CreateIfNotExists();
                return "Ok" + id;
            }
        }
    }
}
