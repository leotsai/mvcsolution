using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MvcSolution.Data.Entities;
using MvcSolution.Infrastructure.Mvc;
using MvcSolution.Services.Users;
using MvcSolution.Web.Admin.ViewModels;

namespace MvcSolution.Web.Admin.Controllers
{
    public class UserController : AdminControllerBase
    {
        public ActionResult Index()
        {
            var model = new UserViewModel().BuildDepartments().BuildRoles();
            return AreaView("user/index.cshtml", model);
        }

        [HttpPost]
        public PartialViewResult List(Guid? departmentId, string role, string name)
        {
            var service = Ioc.GetService<IUserService>();
            var session = GetSession();
            if (session.User.IsSuperAdmin() == false)
            {
                departmentId = service.GetDepartmentId(session.User.Id);
                if (departmentId == null)
                {
                    return PartialAreaView("user/list.cshtml",new List<ManageUserListDto>());
                }
            }
            var items = service.GetManageUserListDtos(departmentId, role, name);
            return PartialAreaView("user/list.cshtml", items);
        }

        public PartialViewResult Editor(Guid? id)
        {
            var model = new UserEditorViewModel(id)
                .BuildUser()
                .BuildRoles()
                .BuildDepartments();
            return PartialAreaView("user/editor.cshtml", model);
        }

        [HttpPost]
        public StandardJsonResult Save(User dbUser, string[] roles)
        {
            var result = new StandardJsonResult();
            result.Try(() =>
            {
                var service = Ioc.GetService<IUserService>();
                var session = GetSession();
                if (!session.User.IsSuperAdmin())
                {
                    if (roles != null && roles.Length > 0)
                    {
                        dbUser.DepartmentId = service.GetDepartmentId(session.User.Id);
                        if (dbUser.DepartmentId == null)
                        {
                            roles = new string[0];
                        }
                        else
                        {
                            var roleService = Ioc.GetService<IRoleService>();
                            var departmentRoles = roleService.GetUserDepartmentRoles(session.User.Id);
                            roles = roles.Where(departmentRoles.Contains).ToArray();
                        }
                    }
                }
                service.Save(dbUser, roles);
            });
            return result;
        }
    }
}
