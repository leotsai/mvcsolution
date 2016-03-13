using System;
using System.Collections.Generic;
using System.Web.Mvc;
using MvcSolution.Data;
using MvcSolution;
using MvcSolution.Services.Admin;

namespace MvcSolution.Web.Admin.Controllers
{
    public class TagController: AdminControllerBase
    {
        [HttpPost]
        public PartialViewResult List()
        {
            var list = Ioc.Get<ITagService>().GetAll();
            return AreaPartialView("tag/list.cshtml", list);
        }

        [HttpPost]
        public StandardJsonResult<List<SimpleEntity>> EntityList()
        {
            return base.Try(() => Ioc.Get<ITagService>().GetAll());
        }

        [HttpPost]
        public StandardJsonResult<Guid> Save(Tag tag)
        {
            return base.Try(() =>
            {
                if (string.IsNullOrWhiteSpace(tag.Name))
                {
                    throw new KnownException("请输入名称");
                }
                Ioc.Get<ITagService>().Save(tag);
                return tag.Id;
            });
        }

        [HttpPost]
        public StandardJsonResult Delete(Guid tagId)
        {
            return base.Try(() => Ioc.Get<ITagService>().Delete(tagId));
        }
    }
}
