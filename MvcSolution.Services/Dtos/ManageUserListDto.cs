using System;
using System.Collections.Generic;
using System.Linq;
using MvcSolution.Data.Context;
using MvcSolution.Data.Entities;
using MvcSolution.Services;

namespace MvcSolution.Services
{
    public class ManageUserListDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public bool IsDisabled { get; set; }
        public string Department { get; set; }
        public string[] Roles { get; set; }
    }
}

namespace MvcSolution
{
    public static class ManageUserListDtoExtensions
    {
        public static IQueryable<ManageUserListDto> ToManageUserListDtos(this IQueryable<User> query)
        {
            return from a in query
                select new ManageUserListDto()
                {
                    Id = a.Id,
                    Name = a.Name,
                    Username = a.Username,
                    IsDisabled = a.IsDisabled,
                    Department = a.Department.Name
                };
        }

        public static List<ManageUserListDto> BuildRoles(this List<ManageUserListDto> dtos, MvcSolutionDataContext db)
        {
            var ids = dtos.Select(x => x.Id).ToArray();
            var query = from a in db.UserRoleRls
                let r = a.Role
                where ids.Contains(a.UserId)
                select new
                {
                    a.UserId,
                    r.Name
                };
            var roles = query.ToList();
            foreach (var dto in dtos)
            {
                dto.Roles = roles.Where(x => x.UserId == dto.Id).Select(x => x.Name).ToArray();
            }
            return dtos;
        }
    }
}