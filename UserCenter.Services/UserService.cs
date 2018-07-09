using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using UserCenter.DTO;
using UserCenter.IServices;
using UserCenter.Services.Entities;

namespace UserCenter.Services
{
    public class UserService : IUserService
    {
        /// <summary>
        /// 新增一个用户
        /// </summary>
        /// <param name="phoneNum"></param>
        /// <param name="nickName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<long> AddNewAsync(string phoneNum, string nickName, string password)
        {
            User userEntity = new User();
            using (UCDbContext ctx =new UCDbContext())
            {
                if (await ctx.Users.AnyAsync(u=>u.PhoneNum==phoneNum))
                {
                    throw new ApplicationException("手机号"+phoneNum+"已经存在");
                }
                userEntity.PhoneNum = phoneNum;
                userEntity.NickName = nickName;
                string salt = new Random().Next(10000, 9999).ToString();
                userEntity.PasswordSalt = salt;
                string hash = CommonHelper.CalMD5(password+salt);
                userEntity.PasswordHash = hash;
                ctx.Users.Add(userEntity);
                await ctx.SaveChangesAsync();
                return userEntity.Id;
            }
        }
        /// <summary>
        /// 检查用户登陆状态
        /// </summary>
        /// <param name="phoneNum"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<bool> CheckLoginAsync(string phoneNum, string password)
        {
            using (UCDbContext ctx=new UCDbContext())
            {
                var user = await ctx.Users.SingleOrDefaultAsync(e=>e.PhoneNum==phoneNum);
                if (user==null)
                {
                    return false;
                }
                string salt = user.PasswordSalt;
                string hash = CommonHelper.CalMD5(password + salt);
                return hash == user.PasswordHash;
            }
        }
        /// <summary>
        /// 根据Id获得用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<UserDTO> GetByIdAsync(long id)
        {
            using (UCDbContext ctx=new UCDbContext())
            {
               
                var user =await ctx.Users.SingleOrDefaultAsync(e=>e.Id==id);
                if (user==null)
                {
                    return null;
                }
                else
                {
                    return TODTO(user);
                }
            }
        }
        /// <summary>
        /// 转存从数据库中得到的值
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private static UserDTO TODTO(User user)
        {
            UserDTO userDto = new UserDTO();
            userDto.NickName = user.NickName;
            userDto.Id = user.Id;
            userDto.PhoneNum = user.PhoneNum;
            return userDto;
        }
        /// <summary>
        ///根据手机号获取用户信息
        /// </summary>
        /// <param name="phoneNum"></param>
        /// <returns></returns>
        public async Task<UserDTO> GetByPhoneNumAsync(string phoneNum)
        {
            using (UCDbContext ctx=new UCDbContext())
            {
                var user = await ctx.Users.SingleOrDefaultAsync(s => s.PhoneNum == phoneNum);
                if (user == null)
                {
                    return null;
                }
                else
                {
                    return TODTO(user);
                }
            }
        }
        /// <summary>
        /// 根据手机号判断用户是否存在
        /// </summary>
        /// <param name="phoneNum"></param>
        /// <returns></returns>
        public async Task<bool> UserExistsAsync(string phoneNum)
        {
            using (UCDbContext ctx = new UCDbContext())
            {
                //var user = await ctx.Users.SingleOrDefaultAsync(s => s.PhoneNum == phoneNum);
                //return phoneNum == user.PhoneNum;
                return await ctx.Users.AnyAsync(e=>e.PhoneNum==phoneNum);
            }
        }
    }
}
