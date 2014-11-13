using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvcSolution.Data.Entities;

namespace MvcSolution.Services.Public
{
    public interface IUserService
    {
        void Register(User user);
    }
}
