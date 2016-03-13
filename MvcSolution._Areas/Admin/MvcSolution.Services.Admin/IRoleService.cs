using System;
using System.Collections.Generic;
using MvcSolution;

namespace MvcSolution.Services.Admin
{
    public interface IRoleService
    {
        List<SimpleEntity> GetAll();
        void SaveUserRoles(string username, List<Guid> roleIds);
        void DeleteAllRoles(string username);

        PageResult<UserRoleDto> SearchUsers(string keyword, Guid? roleId, PageRequest request);
    }
}
