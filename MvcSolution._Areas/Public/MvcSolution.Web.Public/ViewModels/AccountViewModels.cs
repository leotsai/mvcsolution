using System.ComponentModel.DataAnnotations;
using MvcSolution.Data.Entities;
using MvcSolution.Web.ViewModels;

namespace MvcSolution.Web.Public.ViewModels
{
    public class AccountLoginViewModel : LayoutViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel : LayoutViewModel
    {
        [Required(ErrorMessage = "this is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "required password")]
        public string Password { get; set; }
    }
}