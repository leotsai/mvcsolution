using System.ComponentModel.DataAnnotations;
using MvcSolution.Web.ViewModels;

namespace MvcSolution.Web.Admin.ViewModels
{
    public class LoginViewModel : LayoutViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
