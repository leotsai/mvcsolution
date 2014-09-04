using System;
using System.Collections.Generic;
using MvcSolution.Analysis.Enums;

namespace MvcSolution.Analysis
{
    public interface IReportService
    {
        List<RequestGroup> GetGroups(int[] types, Frequency frequency, Platform? platform, DateTime? from, DateTime? to);
        List<RequestGroup> GetPlatformsGroups(int type, Frequency frequency, DateTime? from, DateTime? to);
    }
}
