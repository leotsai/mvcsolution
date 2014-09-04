using System;
using System.Collections.Generic;
using MvcSolution.Analysis.Entities;
using MvcSolution.Analysis.Enums;

namespace MvcSolution.Analysis
{
    public interface IAnalysisService
    {
        void AddLog(int type, Platform? platform);
        void AddLogs(int type, List<PlatformLog> logs);
        DateTime? GetLatestLogTime(int type);
    }
}
