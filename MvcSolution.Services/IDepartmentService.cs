using System.Collections.Generic;
using MvcSolution.Infrastructure;

namespace MvcSolution.Services
{
    public interface IDepartmentService
    {
        List<SimpleEntity> GetAll();
    }
}
