using System;
using System.Collections.Generic;
using System.Web.Mvc;
using MvcSolution.Analysis;
using MvcSolution.Analysis.Enums;
using MvcSolution.Analysis.Implementations;
using MvcSolution.Infrastructure.Mvc;
using MvcSolution.Web.Admin.ViewModels;

namespace MvcSolution.Web.Admin.Controllers
{
    public class AnalysisController : AdminControllerBase
    {
        public ActionResult Index()
        {
            var model = new AnalysisViewModel()
                .BuildFrequncies()
                .BuildPlatforms();
            return AreaView("Analysis/index.cshtml", model);
        }

        public ActionResult Platforms()
        {
            var model = new AnalysisViewModel()
                .BuildFrequncies();
            return AreaView("Analysis/platforms.cshtml", model);
        }

        [HttpPost]
        public StandardJsonResult GetData(Platform? platform, Frequency frequency, DateTime? from, DateTime? to, int[] types)
        {
            var result = new StandardJsonResult<List<RequestGroup>>();
            result.Try(() =>
            {
                var service = Ioc.GetService<IReportService>();
                result.Value = service.GetGroups(types, frequency, platform, from, to);
            });
            return result;
        }

        [HttpPost]
        public StandardJsonResult GetPlatformsData(Frequency frequency, DateTime? from, DateTime? to, int type)
        {
            var result = new StandardJsonResult<List<RequestGroup>>();
            result.Try(() =>
            {
                var service = Ioc.GetService<IReportService>();
                result.Value = service.GetPlatformsGroups(type, frequency, from, to);
            });
            return result;
        }

        [AllowAnonymous, HttpGet]
        public string Log(int type, Platform? platform = null)
        {
            try
            {
                IAnalysisService analysisService = new AnalysisService();
                analysisService.AddLog(type, platform);
            }
            catch (Exception)
            {
                return "error";
            }
            return "ok";
        }
    }
}
