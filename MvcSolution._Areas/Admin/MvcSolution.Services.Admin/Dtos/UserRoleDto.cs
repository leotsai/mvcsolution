using System;
using System.Linq;
using MvcSolution.Data;
using MvcSolution.Data;
using MvcSolution;
using MvcSolution.Services.Admin;

namespace MvcSolution.Services.Admin
{
    public class UserRoleDto
    {
        public Guid UserId { get; set; }
        public string ImageKey { get; set; }
        public string Nickname { get; set; }
        public string Username { get; set; }
        public string InternalNotes { get; set; }
        public string[] Roles { get; set; }
    }
}

namespace MvcSolution
{
    public static class UserRoleDtoExts
    {
        public static IQueryable<UserRoleDto> ToRoleDtos(this IQueryable<User> query)
        {
            return from a in query
                select new UserRoleDto()
                {
                    UserId = a.Id,
                    ImageKey = a.ImageKey,
                    Nickname = a.NickName,
                    Username = a.Username,
                    InternalNotes = a.InternalNotes
                };
        }

        public static IQueryable<UserRoleDto> WhereByKeyword(this IQueryable<UserRoleDto> query, string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return query;
            }
            return from a in query
                where a.Nickname.Contains(keyword)
                      || a.Username.Contains(keyword)
                      || a.InternalNotes.Contains(keyword)
                select a;
        }

        public static PageResult<UserRoleDto> Build(this PageResult<UserRoleDto> result, MvcSolutionDbContext db)
        {
            if (result.Value.Count == 0)
            {
                return result;
            }
            var ids = result.Value.Select(x => x.UserId).ToArray();
            var query = from a in db.UserRoleRLs
                where ids.Contains(a.UserId)
                select new
                {
                    a.UserId,
                    a.Role.Name
                };
            var items = query.Distinct().ToList();
            foreach (var dto in result.Value)
            {
                dto.Roles = items.Where(x => x.UserId == dto.UserId).Select(x => x.Name).OrderBy(x => x).ToArray();
            }
            return result;
        } 
    }

}