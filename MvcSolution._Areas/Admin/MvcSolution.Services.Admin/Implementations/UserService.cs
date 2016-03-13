using System;
using System.Collections.Generic;
using System.Linq;
using MvcSolution.Data;

namespace MvcSolution.Services.Admin
{
    public class UserService : ServiceBase, IUserService
    {
        public PageResult<UserDto> Search(UserSearchCriteria criteria, PageRequest request)
        {
            using (var db = base.NewDB())
            {
                return db.Users
                    .WhereByTagId(criteria.TagId)
                    .WhereByKeyword(criteria.Keyword)
                    .ToDtos()
                    .ToPageResult(request)
                    .Build(db);
            }
        }
        
        public void Save(Guid userId, Guid[] tagIds, string notes)
        {
            using (var db = base.NewDB())
            {
                var user = db.Users.Get(userId);
                user.InternalNotes = notes;
                user.UserTagRLs.ToList().ForEach(x => db.UserTagRLs.Remove(x));
                if (tagIds != null)
                {
                    foreach (var id in tagIds)
                    {
                        db.UserTagRLs.Add(new UserTagRL(userId, id));
                    }
                }
                db.SaveChanges();
            }
        }
    }
}
