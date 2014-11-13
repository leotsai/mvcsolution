using System.Collections.Generic;
using System.Linq;
using MvcSolution.Infrastructure;

namespace MvcSolution.Services
{
    public class DepartmentService : ServiceBase, IDepartmentService
    {
        public List<SimpleEntity> GetAll()
        {
            using (var db = base.NewDB())
            {
                return db.Departments.ToSimpleEntities().OrderByName().ToList();
            }
        }
    }
}
