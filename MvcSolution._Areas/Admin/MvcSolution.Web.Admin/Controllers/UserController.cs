using System;
using System.Web.Mvc;
using MvcSolution.Data;
using MvcSolution;
using MvcSolution.Services.Admin;
using MvcSolution.Web.Admin.ViewModels;

namespace MvcSolution.Web.Admin.Controllers
{
    public class UserController : AdminControllerBase
    {
        public ActionResult Index()
        {
            var model = new UserIndexViewModel().Build();
            return AreaView("user/index.cshtml", model);
        }

        public PartialViewResult List(UserSearchCriteria criteria, PageRequest request)
        {
            var service = Ioc.Get<Services.Admin.IUserService>();
            var list = service.Search(criteria, request);
            return AreaPartialView("user/list.cshtml", list);
        }
        
        public PartialViewResult Editor(Guid userId)
        {
            var model = new UserEditorViewModel(userId).Build();
            return AreaPartialView("user/editor.cshtml", model);
        }

        [HttpPost]
        public StandardJsonResult Save(Guid userId, Guid[] tagIds, string notes)
        {
            return base.Try(() =>
            {
                var service = Ioc.Get<Services.Admin.IUserService>();
                service.Save(userId, tagIds, notes);
            });
        }

        [HttpPost, ValidateInput(false)]
        public StandardJsonResult SendWeixin(UserSearchCriteria criteria, string htmlContent, string text, string[] types)
        {
            return base.Try(() =>
            {
                throw new KnownException("This method is not implemented for the demo");
            });
        }
    }
}
