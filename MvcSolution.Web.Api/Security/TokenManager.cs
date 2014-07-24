using System;
using System.Text;
using MvcSolution.Infrastructure.Utilities;

namespace MvcSolution.Web.Api.Security
{
    public class TokenManager
    {
        public static MvcSolutionIdentity ParseIdentity(string token)
        {
            var json = Encoding.UTF8.GetString(Convert.FromBase64String(token));
            return Serializer.FromJson<MvcSolutionIdentity>(json);
        }

        public static string CreateToken(MvcSolutionIdentity identity)
        {
            var json = Serializer.ToJson(identity);
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(json));
        }
    }
}