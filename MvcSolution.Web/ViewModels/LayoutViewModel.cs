using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcSolution.Web.ViewModels
{
    public class LayoutViewModel
    {
        public string Title { get; set; }
        public string Error { get; set; }
    }

    public class LayoutViewModel<T> : LayoutViewModel
    {
        public T Model { get; set; }
    }
}