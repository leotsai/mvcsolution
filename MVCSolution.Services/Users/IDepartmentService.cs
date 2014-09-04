using System.Collections.Generic;
using MvcSolution.Infrastructure;

namespace MvcSolution.Services.Users
{
    public interface IDepartmentService
    {
        List<SimpleEntity> GetAll();
    }
}
