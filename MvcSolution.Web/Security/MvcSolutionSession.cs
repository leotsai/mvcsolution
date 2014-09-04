using System.Web;
using System.Web.SessionState;
using MvcSolution.Services.Users;
using MvcSolution.Web.Security;

namespace MvcSolution.Web.Security
{
    public class MvcSolutionSession
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

        public void Login(string username)
        {
            ReloadAll(username);
        }

        public void Init()
        {
            if (!IsAuthenticated)
            {
                Logout();
                return;
            }
            ReloadAll(HttpContext.Current.User.Identity.Name);
        }

        private void ReloadAll(string username)
        {
            _user = Ioc.GetService<IUserService>().GetSessionUser(username);
        }

        public bool IsAuthenticated
        {
            get { return HttpContext.Current.Request.IsAuthenticated; }
        }

    }
}

namespace MvcSolution
{
    public static class SessionExtensions
    {
        private const string MvcSolutionSessionKey = "MvcSolutionSession";

        public static MvcSolutionSession GetMvcSolutionSession(this HttpSessionState session)
        {
            if (session[MvcSolutionSessionKey] == null)
            {
                var mvcSession = new MvcSolutionSession();
                mvcSession.Init();
                session[MvcSolutionSessionKey] = mvcSession;
                return mvcSession;
            }
            return session[MvcSolutionSessionKey] as MvcSolutionSession;
        }
    }
}