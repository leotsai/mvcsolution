using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using MvcSolution.Data.Entities;
using MvcSolution.Services.Users;
using MvcSolution.Web.ViewModels;

namespace MvcSolution.Web.Admin.ViewModels
{
    public class UserViewModel : LayoutViewModel
    {
        public List<SelectListItem> Departments { get; set; } 
        public List<SelectListItem> Roles { get; set; } 

        public UserViewModel BuildDepartments()
        {
            if (User.IsSuperAdmin() == false)
            {
                return this;
            }
            var service = Ioc.GetService<IDepartmentService>();
            this.Departments = service.GetAll().ToSelectList("--All Dept.--");
            return this;
        }

        public UserViewModel BuildRoles()
        {
            var service = Ioc.GetService<IRoleService>();
            List<string> roles;
            if (User.IsSuperAdmin())
            {
                roles = service.GetAll();
            }
            else
            {
                roles = service.GetUserDepartmentRoles(User.Id);
            }
            this.Roles = roles.ToSelectList(x => x, x => x, "--All Roles--");
            return this;
        }
    }

    public class UserEditorViewModel : LayoutViewModel
    {
        private readonly Guid? _id;

        public User DbUser { get; set; }
        public List<SelectListItem> Roles { get; set; } 
        public List<SelectListItem> Departments { get; set; } 
        public string DepartmentName { get; set; }

        public bool IsNew
        {
            get { return _id == null; }
        }

        [Required]
        public string Password { get; set; }

        [Required, Compare("Password", ErrorMessage = "Passwords don't match.")]
        public string ConfirmPassword { get; set; }

        public string NewPassword { get; set; }

        [Compare("NewPassword", ErrorMessage = "Passwords don't match.")]
        public string ConfirmNewPassword { get; set; }

        public UserEditorViewModel(Guid? id)
        {
            _id = id;
        }

        public UserEditorViewModel BuildUser()
        {
            if (_id == null)
            {
                this.DbUser = new User();
            }
            else
            {
                this.DbUser = Ioc.GetService<IUserService>().Get(_id.Value);
            }
            return this;
        }

        public UserEditorViewModel BuildRoles()
        {
            var service = Ioc.GetService<IRoleService>();
            List<string> allRoles;
            if (User.IsSuperAdmin())
            {
                allRoles = service.GetAll();
            }
            else
            {
                allRoles = service.GetUserDepartmentRoles(User.Id);
            }
            var userRoles = IsNew ? new List<string>() : service.GetUserRoles(DbUser.Id);
            this.Roles = allRoles.ToSelectList(x => x, x => x, userRoles.Contains);
            return this;
        }

        public UserEditorViewModel BuildDepartments()
        {
            if (User.IsSuperAdmin())
            {
                var service = Ioc.GetService<IDepartmentService>();
                this.Departments = service.GetAll().ToSelectList("--All Dept.--");
            }
            else
            {
                var service = Ioc.GetService<IUserService>();
                this.DepartmentName = service.GetDepartmentName(User.Id);
            }
            return this;
        }
    }
}