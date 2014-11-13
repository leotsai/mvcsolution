using System.Collections.Generic;
using MvcSolution.Data.Entities;

namespace MvcSolution.Services
{
    public interface ISettingService
    {
        void Init(List<Setting> settings);
        List<Setting> GetAll();
    }
}
