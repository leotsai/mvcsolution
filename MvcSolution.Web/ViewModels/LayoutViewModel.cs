using System;
using System.Web;
using MvcSolution.Web.Security;

namespace MvcSolution.Web.ViewModels
{
    public class LayoutViewModel
    {
        public string Title { get; set; }
        public string Error { get; set; }

        public MvcSession GetSession()
        {
            return HttpContext.Current.Session.GetMvcSession();
        }

        public bool HasError => !string.IsNullOrEmpty(this.Error);

        protected Guid GetUserId()
        {
            return HttpContext.Current.Request.GetUserId();
        }

        public void Try(Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                this.Error = (ex is KnownException) ? ex.Message : ex.GetAllMessages();
            }
        }
    }

    public class LayoutViewModel<T> : LayoutViewModel
    {
        public T Model { get; set; }
    }
}