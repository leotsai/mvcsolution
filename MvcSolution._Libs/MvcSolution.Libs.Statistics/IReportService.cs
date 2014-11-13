using System;
using System.Collections.Generic;
using MvcSolution.Libs.Statistics.Enums;

namespace MvcSolution.Libs.Statistics
{
    public interface IReportService
    {
        List<RequestGroup> GetGroups(int[] types, Frequency frequency, Platform? platform, DateTime? from, DateTime? to);
        List<RequestGroup> GetPlatformsGroups(int type, Frequency frequency, DateTime? from, DateTime? to);
    }
}
