using System;
using System.Collections.Generic;

namespace MvcSolution.Services
{
    public interface IRoleService
    {
        List<string> GetAll();
        List<string> GetUserDepartmentRoles(Guid departmentId);
        List<string> GetUserRoles(Guid userId);
    }
}
