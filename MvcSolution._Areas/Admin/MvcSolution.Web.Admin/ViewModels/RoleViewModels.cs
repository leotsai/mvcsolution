using System.Collections.Generic;
using System.Web.Mvc;
using MvcSolution.Services.Admin;
using MvcSolution.Web.ViewModels;

namespace MvcSolution.Web.Admin.ViewModels
{
    public class RoleIndexViewModel: LayoutViewModel
    {
        public List<SelectListItem> Roles { get; set; }

        public RoleIndexViewModel Build()
        {
            var service = Ioc.Get<IRoleService>();
            this.Roles = service.GetAll().ToSelectList(x => x.Name, x => x.Id, "Any/All");
            return this;
        }
    }
}
