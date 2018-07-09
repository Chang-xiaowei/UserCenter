using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using UserCenter.DTO;
using UserCenter.IServices;
using UserCenter.Services.Entities;
using System.Linq;
namespace UserCenter.Services
{
    public class UserGroupService : IUserGroupService
    {
        public async Task AddUserToGroupAsync(long userGroupId, long userId)
        {
            using (UCDbContext db=new UCDbContext())
            {
                var group =  db.UserGroups.SingleOrDefaultAsync(u => u.Id == userGroupId);
                if (group==null)
                {
                    throw new ArgumentException("userGroupId"+userGroupId+"已存在", nameof(userGroupId));
                }
                
                var user =  db.Users.SingleOrDefaultAsync(u=>u.Id==userId);
                if (user == null)
                {
                    throw new ArgumentException("userId=" + userId + "不存在", nameof(userId));
                }               
                await db.SaveChangesAsync();

            }
        }

        public async Task<UserGroupDTO> GetByIdAsync(long id)
        {
            using (UCDbContext db = new UCDbContext())
            {
              var userGroup= await db.UserGroups.SingleOrDefaultAsync(e=>e.Id==id);
                if (userGroup==null)
                {
                    return null;
                }
                else
                {
                    return ToDTO(userGroup);
                }
            }
        }
        private UserGroupDTO ToDTO(UserGroup userGroup)
        {
            UserGroupDTO dto = new UserGroupDTO();
            dto.Name = userGroup.Name;
            dto.Id = userGroup.Id;
            return dto;
        }
        /// <summary>
        /// 根据userId得到用户组信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<UserGroupDTO[]> GetGroupsAsync(long userId)
        {
            using (UCDbContext db = new UCDbContext())
            {
                var user =await db.Users.SingleOrDefaultAsync(e=>e.Id==userId);
                if (user==null)
                {
                    return null;
                }
                var groups = user.Groups;
                List<UserGroupDTO> dtos = new List<UserGroupDTO>();
                foreach (var group in groups)
                {
                    dtos.Add(ToUserGroupDTO(group));
                }
                return dtos.ToArray();
            }
        }

        private UserGroupDTO ToUserGroupDTO(UserGroup group)
        {
            UserGroupDTO dto = new UserGroupDTO();
            dto.Name = group.Name;
            dto.Id = group.Id;
            return dto;
        }
        /// <summary>
        /// 根据用户组Id得到用户信息
        /// </summary>
        /// <param name="userGroupId"></param>
        /// <returns></returns>
        public async Task<UserDTO[]> GetGroupUsersAsync(long userGroupId)
        {
            using (UCDbContext db = new UCDbContext())
            {
                var group =await db.UserGroups.SingleOrDefaultAsync(s => s.Id == userGroupId);
                if (group==null)
                {
                    return null;
                }
                var users = group.Users;
                List<UserDTO> dtos = new List<UserDTO>();
                foreach (var item in users)
                {
                    UserDTO dto = new UserDTO();
                    dto.NickName = item.NickName;
                    dto.Id = item.Id;
                    dto.PhoneNum = item.PhoneNum;
                    dtos.Add(dto);
                }
                return dtos.ToArray();
            }
        }
        /// <summary>
        /// 从用户组关系表中删除用户
        /// </summary>
        /// <param name="userGroupId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task RemoveUserFromGroupAsync(long userGroupId, long userId)
        {
            using (UCDbContext db = new UCDbContext())
            {
                var group = await db.UserGroups.SingleOrDefaultAsync(g => g.Id == userGroupId);
                if (group==null)
                {
                    throw new ArgumentException("userGroupId" + userGroupId + "的用户组", nameof(userGroupId));
                }
                var user = await db.Users.SingleOrDefaultAsync(u=>u.Id== userId);
                if (user==null)
                {
                    throw new ArgumentException("不存在userId=" + userId + "的用户", nameof(userGroupId));
                }
                group.Users.Remove(user);
                user.Groups.Remove(group);
                await db.SaveChangesAsync();

            }
        }
    }
}
