using System;
using System.Collections.Generic;
using System.Linq;
using MvcSolution.Data;
using MvcSolution;

namespace MvcSolution.Services.Admin
{
    public class TagService: ServiceBase, ITagService
    {
        public List<SimpleEntity> GetAll()
        {
            using (var db = base.NewDB())
            {
                return db.Tags.ToSimpleEntities().OrderBy(x => x.Name).ToList();
            }
        }

        public List<Guid> GetUserTags(Guid userId)
        {
            using (var db = base.NewDB())
            {
                return db.UserTagRLs.Where(x => x.UserId == userId).Select(x => x.TagId).ToList();
            }
        }

        public void Save(Tag tag)
        {
            using (var db = base.NewDB())
            {
                if (tag.IsNew)
                {
                    tag.NewId();
                    db.Tags.Add(tag);
                }
                else
                {
                    var dbTag = db.Tags.Get(tag.Id);
                    dbTag.Name = tag.Name;
                }
                db.SaveChanges();
            }
        }

        public void Delete(Guid tagId)
        {
            using (var db = base.NewDB())
            {
                var tag = db.Tags.Get(tagId);
                tag.UserTagRLs.ToList().ForEach(x => db.UserTagRLs.Remove(x));
                db.Tags.Remove(tag);
                db.SaveChanges();
            }
        }
    }
}
