using MVC_Store.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_Store.Models.ViewModels.Account
{
    public class UserProfileViewModel
    {
        public UserProfileViewModel() 
        { 
        }
        public UserProfileViewModel(UserDTO userDTO)
        {
            Id = userDTO.Id;
            FirstName = userDTO.FirstName;
            LastName = userDTO.LastName;
            EmailAddress = userDTO.EmailAddress;
            UserName = userDTO.UserName;
            Password = userDTO.Password;
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email address")]
        public string EmailAddress { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [StringLength(50, MinimumLength = 2)]
        public string Password { get; set; }

        [StringLength(50, MinimumLength = 2)]
        [Display(Name = "Confirm password")]
        public string ConfirmPassword { get; set; }
    }
}