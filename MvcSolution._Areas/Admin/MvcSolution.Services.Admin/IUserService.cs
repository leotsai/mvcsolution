using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvcSolution.Data.Entities;

namespace MvcSolution.Services.Admin
{
    public interface IUserService
    {
        List<ManageUserListDto> GetManageUserListDtos(Guid? departmentId, string role, string name);
        Guid? GetDepartmentId(Guid userId);
        string GetDepartmentName(Guid userId);
        void Save(User user, string[] roles);
    }
}
