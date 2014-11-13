using System;
using System.Collections.Generic;
using MvcSolution.Libs.Statistics.Entities;
using MvcSolution.Libs.Statistics.Enums;

namespace MvcSolution.Libs.Statistics
{
    public interface IAnalysisService
    {
        void AddLog(int type, Platform? platform);
        void AddLogs(int type, List<PlatformLog> logs);
        DateTime? GetLatestLogTime(int type);
    }
}
