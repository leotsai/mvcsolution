using System.Collections.Generic;
using MvcSolution.Data;

namespace MvcSolution.Services
{
    public interface ISettingService
    {
        void Init(List<Setting> settings);
        List<Setting> GetAll();
        void Update(string key, string value);
    }
}
