using MVC_Store.Models.Data;
using System.ComponentModel.DataAnnotations;

namespace MVC_Store.Models.ViewModels.Account
{
    public class UserViewModel
    {
        public UserViewModel()
        {
        }

        public UserViewModel(UserDTO userDTO)
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
        [Display(Name ="First name")]
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

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Password { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        [Display(Name = "Confirm password")]
        public string ConfirmPassword { get; set; }
    }
}