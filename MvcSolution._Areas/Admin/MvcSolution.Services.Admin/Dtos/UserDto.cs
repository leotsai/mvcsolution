using System;
using System.Linq;
using MvcSolution.Data;
using MvcSolution.Data;
using MvcSolution;
using MvcSolution.Services.Admin;

namespace MvcSolution.Services.Admin
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string NickName { get; set; }
        public string ImageKey { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public string InternalNotes { get; set; }
        public string RegisterAddress { get; set; }

        public string[] Tags { get; set; }
    }
}

namespace MvcSolution
{
    public static class UserDtoExtAdmin
    {
        public static IQueryable<UserDto> ToDtos(this IQueryable<User> query)
        {
            return from a in query
                   select new UserDto()
                   {
                       Id = a.Id,
                       Username = a.Username,
                       NickName = a.NickName,
                       ImageKey = a.ImageKey,
                       LastLoginTime = a.LastLoginTime,
                       InternalNotes = a.InternalNotes,
                       CreatedTime = a.CreatedTime,
                       RegisterAddress = a.RegisterAddress
                   };
        }

        public static PageResult<UserDto> Build(this PageResult<UserDto> result, MvcSolutionDbContext db)
        {
            if (result.Value.Count == 0)
            {
                return result;
            }
            var ids = result.Value.Select(x => x.Id).ToArray();
            var tagQuery = from a in db.UserTagRLs
                           where ids.Contains(a.UserId)
                           select new
                           {
                               a.UserId,
                               a.Tag.Name
                           };

            var tags = tagQuery.ToList();
            foreach (var dto in result.Value)
            {
                dto.Tags = tags.Where(x => x.UserId == dto.Id).Select(x => x.Name).ToArray();
            }
            return result;
        }
    }
}