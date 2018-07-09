using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserCenter.DTO;
using UserCenter.IServices;
using UserCenter.Services.Entities;

namespace UserCenter.Services
{
    public class AppInfoService : IAppInfoService
    {
        /// <summary>
        /// 根据appkey得到
        /// </summary>
        /// <param name="appkey"></param>
        /// <returns></returns>
        public async Task<AppInfoDTO> GetByAppKeyAsyc(string appkey)
        {
            using (UCDbContext ctx=new UCDbContext())
            {
                var appInfo =await ctx.AppInfos.SingleOrDefaultAsync(a => a.AppKey==appkey);
                if (appkey==null)
                {
                    return null;
                }
                else
                {
                    return TODTO(appInfo);
                }

            }
        }

        private AppInfoDTO TODTO(AppInfo appInfo)
        {
            AppInfoDTO dto = new AppInfoDTO();
            dto.AppSecret = appInfo.AppSecret;
            dto.Name = appInfo.Name;
            dto.IsEnabled = appInfo.IsEnabled;
            return dto; 
        }
    }
}
