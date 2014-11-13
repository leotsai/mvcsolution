using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcSolution.Infrastructure;

namespace MvcSolution
{
    public static class EnumerableExtensionsWeb
    {
        public static List<SelectListItem> ToSelectList(this List<SimpleEntity> entities, string optionalLabel = null)
        {
            return entities.ToSelectList(x => x.Name, x => x.Id, optionalLabel);
        } 
    }
}