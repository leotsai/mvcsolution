using System;
using System.Collections.Generic;
using System.Web.Mvc;
using MvcSolution;
using MvcSolution.Services.Admin;
using MvcSolution.Web.Admin.ViewModels;

namespace MvcSolution.Web.Admin.Controllers
{
    public class RoleController : AdminControllerBase
    {
        public ActionResult Index()
        {
            var model = new RoleIndexViewModel().Build();
            return AreaView("role/index.cshtml", model);
        }

        public PartialViewResult SearchUsers(string keyword, Guid? roleId, PageRequest request)
        {
            var service = Ioc.Get<IRoleService>();
            var list = service.SearchUsers(keyword, roleId, request);
            return AreaPartialView("role/usersList.cshtml", list);
        }

        public PartialViewResult Add()
        {
            var service = Ioc.Get<IRoleService>();
            var roles = service.GetAll();
            return AreaPartialView("role/add.cshtml", roles);
        }

        [HttpPost]
        public StandardJsonResult Save(string username, List<Guid> roleIds)
        {
            return base.Try(() =>
            {
                var service = Ioc.Get<IRoleService>();
                service.SaveUserRoles(username, roleIds);
            });
        }

        [HttpPost]
        public StandardJsonResult Delete(string username)
        {
            return base.Try(() =>
            {
                var service = Ioc.Get<IRoleService>();
                service.DeleteAllRoles(username);
            });
        }
    }
}
