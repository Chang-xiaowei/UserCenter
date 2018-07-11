using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using UserCenter.IServices;
using RuPeng.Common;
using System.Text;

namespace UserCenter.OpenAPI.Filter
{
    public class UCAuthorFilter : IAuthorizationFilter
    {
        public bool AllowMultiple =>true;
        public IAppInfoService appInfoService { get; set; }
        public UCAuthorFilter()
        {
            this.appInfoService = appInfoService;
        }
        public async  Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            IEnumerable<string> appKeys;
            //从报文头里取出appKey的值
            var header = actionContext.Request.Headers.TryGetValues("AppKey",out appKeys);
            if (!header)
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized) { Content = new StringContent("报文头中的AppKey为空")};

            }
            IEnumerable<string> signs;
            //从报文头里取出sign的值
            var signheader = actionContext.Request.Headers.TryGetValues("Sign", out signs);
            if (!signheader)
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized) { Content = new StringContent("报文头中的Sign为空") };

            }
            string appkey = appKeys.First();
            string sign = signs.First();
            var appInfo=await appInfoService.GetByAppKeyAsyc(appkey);
            if (appInfo==null)
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized) { Content=new StringContent("不存在的appkey")};
            }
            if (!appInfo.IsEnabled)
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden) { Content = new StringContent("appKey已经为禁用") };
            }
            //计算用户输入参数的连接+AppSecret的Md5值
            //orderedQS就是按照key(参数的名字）进行排序的QueryString的集合
            var orderedQS = actionContext.Request.GetQueryNameValuePairs().OrderBy(kv=>kv.Key);
            var segments= orderedQS.Select(kv=>kv.Key+"="+kv.Value);//拼接key=value的数组
            string qs= string.Join("&", segments);//用&拼接起来
            //计算出来的sign
            string computedSign = MD5Helper.ComputeMd5(qs + appInfo.AppSecret);//计算qs+appSecret的MD5值
            //用户传进来的值和计算出来的对比，就知道数据是否被篡改过
            if (sign.Equals(computedSign,StringComparison.CurrentCultureIgnoreCase))
            {
                return await continuation();
            }
            else
            {
                return new HttpResponseMessage() { Content=new StringContent(" sign验证失败")};
            }          
        }
    }
}