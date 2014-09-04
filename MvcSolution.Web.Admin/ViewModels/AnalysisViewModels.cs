using System.Collections.Generic;
using System.Web.Mvc;
using MvcSolution.Analysis.Enums;
using MvcSolution.Web.ViewModels;
using MvcSolution.Analysis;

namespace MvcSolution.Web.Admin.ViewModels
{
    public class AnalysisViewModel : LayoutViewModel
    {
        public List<SelectListItem> Platforms { get; set; }
        public List<SelectListItem> Frequencies { get; set; } 

        public AnalysisViewModel BuildPlatforms()
        {
            this.Platforms = PlatformHelper.GetAll().ToSelectList(x => x.GetText(), x => x, "所有平台");
            return this;
        }

        public AnalysisViewModel BuildFrequncies()
        {
            var frequencies = new List<Frequency>();
            frequencies.Add(Frequency.Hour);
            frequencies.Add(Frequency.Day);
            frequencies.Add(Frequency.Week);
            frequencies.Add(Frequency.Month);
            this.Frequencies = frequencies.ToSelectList(x => x.GetText(), x => (int) x);
            return this;
        }
    }
}