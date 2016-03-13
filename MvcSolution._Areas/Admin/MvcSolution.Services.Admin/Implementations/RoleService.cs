using System;
using System.Collections.Generic;
using System.Linq;
using MvcSolution.Data;
using MvcSolution;

namespace MvcSolution.Services.Admin
{
    public class RoleService : ServiceBase, IRoleService
    {
        public List<SimpleEntity> GetAll()
        {
            using (var db = base.NewDB())
            {
                return db.Roles.ToSimpleEntities().OrderBy(x => x.Name).ToList();
            }
        }

        public void SaveUserRoles(string username, List<Guid> roleIds)
        {
            using (var db = base.NewDB())
            {
                var user = db.Users.FirstOrDefault(x => x.Username == username);
                if (user == null)
                {
                    throw new KnownException("该手机号未注册");
                }
                var rls = db.UserRoleRLs.Where(x => x.UserId == user.Id).ToList();
                rls.ForEach(x => db.UserRoleRLs.Remove(x));
                if (roleIds != null)
                {
                    foreach (var roleId in roleIds)
                    {
                        db.UserRoleRLs.Add(new UserRoleRL(user.Id, roleId));
                    }
                }
                db.SaveChanges();
            }
        }

        public void DeleteAllRoles(string username)
        {
            using (var db = base.NewDB())
            {
                var user = db.Users.FirstOrDefault(x => x.Username == username);
                if (user == null)
                {
                    throw new KnownException("该手机号未注册");
                }
                var rls = db.UserRoleRLs.Where(x => x.UserId == user.Id).ToList();
                rls.ForEach(x => db.UserRoleRLs.Remove(x));
                db.SaveChanges();
            }
        }

        public PageResult<UserRoleDto> SearchUsers(string keyword, Guid? roleId, PageRequest request)
        {
            using (var db = base.NewDB())
            {
                var query = from a in db.UserRoleRLs
                    where (roleId == null || roleId == a.RoleId)
                    select a.User;
                return query.ToRoleDtos()
                    .WhereByKeyword(keyword)
                    .Distinct()
                    .ToPageResult(request)
                    .Build(db);
            }
        }
    }
}
