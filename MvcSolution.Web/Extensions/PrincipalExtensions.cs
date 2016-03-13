using System.Security.Principal;
using MvcSolution.Data;

namespace MvcSolution
{
    public static class PrincipalExtensions
    {
        public static bool IsSuperAdmin(this IPrincipal principal)
        {
            return principal.IsInRole(Role.Names.SuperAdmin);
        }
    }
}
