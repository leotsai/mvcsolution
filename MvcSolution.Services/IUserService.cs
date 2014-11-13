using System;
using System.Collections.Generic;
using MvcSolution.Data.Entities;

namespace MvcSolution.Services
{
    public interface IUserService
    {
        User Get(string username);
        bool CanLogin(string username, string password);
        void Register(User user);

        SessionUser GetSessionUser(string username);
        List<ManageUserListDto> GetManageUserListDtos(Guid? departmentId, string role, string name);
        Guid? GetDepartmentId(Guid userId);
        string GetDepartmentName(Guid userId);
        User Get(Guid id);
        void Save(User user, string[] roles);
    }
}
