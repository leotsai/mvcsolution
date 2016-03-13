using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MvcSolution.Data;
using MvcSolution.Services.Admin;
using MvcSolution.Web.ViewModels;

namespace MvcSolution.Web.Admin.ViewModels
{
    public class UserIndexViewModel: LayoutViewModel
    {
        public List<SelectListItem> Tags { get; set; }

        public UserIndexViewModel Build()
        {
            this.Tags = Ioc.Get<ITagService>().GetAll()
                .ToSelectList(x => x.Name, x => x.Id, "Any/All");
            return this;
        }
    }

    public class UserEditorViewModel
    {
        private readonly Guid _userId;

        public User User { get; set; }
        public List<SelectListItem> Tags { get; set; }

        public UserEditorViewModel(Guid userId)
        {
            _userId = userId;
        }

        public UserEditorViewModel Build()
        {
            this.User = Ioc.Get<Services.IUserService>().Get(_userId);
            var service = Ioc.Get<ITagService>();
            var userTags = service.GetUserTags(_userId);
            this.Tags = service.GetAll().ToSelectList(x => x.Name, x => x.Id, x => userTags.Any(u => u == x.Id));
            return this;
        }
    }
}
