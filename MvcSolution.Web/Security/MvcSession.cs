using System;
using System.Web;
using System.Web.SessionState;
using MvcSolution.Services;
using MvcSolution.Web.Security;

namespace MvcSolution.Web.Security
{
    public class MvcSession
    {
        private SessionUser _user;

        public SessionUser User
        {
            get { return _user; }
        }

        public void Logout()
        {
            _user = null;
        }

        public void Login(Guid userId)
        {
            ReloadAll(userId);
        }

        public void Init()
        {
            if (!IsAuthenticated)
            {
                Logout();
                return;
            }

            ReloadAll(Guid.Parse(HttpContext.Current.User.Identity.Name));
        }

        private void ReloadAll(Guid userId)
        {
            var service = Ioc.Get<IUserService>();
            _user = service.GetSessionUser(userId);
            this.WeixinUserId = userId;
        }

        public bool IsAuthenticated
        {
            get { return HttpContext.Current.Request.IsAuthenticated; }
        }

        public Guid? WeixinUserId { get; set; }
    }
}

namespace MvcSolution
{
    public static class SessionExtensions
    {
        private const string MvcSessionKey = "MvcSession";

        public static MvcSession GetMvcSession(this HttpSessionState session)
        {
            if (session[MvcSessionKey] == null)
            {
                var mvcSession = new MvcSession();
                mvcSession.Init();
                session[MvcSessionKey] = mvcSession;
                return mvcSession;
            }
            return session[MvcSessionKey] as MvcSession;
        }
    }
}