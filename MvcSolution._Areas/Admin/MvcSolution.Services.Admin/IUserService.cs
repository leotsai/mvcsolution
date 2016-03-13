using System;
using MvcSolution;

namespace MvcSolution.Services.Admin
{
    public interface IUserService
    {
        PageResult<UserDto> Search(UserSearchCriteria criteria, PageRequest request);
        void Save(Guid userId, Guid[] tagIds, string notes);
    }
}
