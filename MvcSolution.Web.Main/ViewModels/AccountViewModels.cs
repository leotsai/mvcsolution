using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using MvcSolution.Web.ViewModels;

namespace MvcSolution.Web.Main.ViewModels
{
    public class AccountLoginViewModel : LayoutViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }

}