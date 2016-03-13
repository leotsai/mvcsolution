using System;
using System.Collections.Generic;
using MvcSolution.Data;
using MvcSolution;

namespace MvcSolution.Services.Admin
{
    public interface ITagService
    {
        List<SimpleEntity> GetAll();
        List<Guid> GetUserTags(Guid userId);
        void Save(Tag tag);
        void Delete(Guid tagId);
    }
}
