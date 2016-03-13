using System;
using System.Linq;
using MvcSolution.Data;

namespace MvcSolution
{
    public static class UserExtensions
    {
        public static IQueryable<User> WhereByKeyword(this IQueryable<User> query, string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return query;
            }
            return from a in query
                where a.NickName.Contains(keyword)
                      || a.Username.Contains(keyword)
                      || a.InternalNotes.Contains(keyword)
                select a;
        }

        public static IQueryable<User> WhereByTagId(this IQueryable<User> query, Guid? tagId)
        {
            if (tagId == null)
            {
                return query;
            }
            return from a in query
                   from rl in a.UserTagRLs
                   where rl.TagId == tagId
                   select a;
        }
        
        public static string GetNickName(this IQueryable<User> query, Guid userId)
        {
            return query.Where(x => x.Id == userId).Select(x => x.NickName).FirstOrDefault();
        }
    }
}
