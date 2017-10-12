namespace BikeTrips.Web.Areas.Admin.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    using Common.Constants;
    using Data.Models;
    using Infrastructure.Mappings;

    public class RegisterAdminViewModel : IMapTo<User>
    {
        [Required]
        [Display(Name = "UserName")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(CommonStringLengthConstants.StandardMaxLength, ErrorMessage = ErrorMessageConstants.InvalidPassordLength, MinimumLength = CommonStringLengthConstants.StandardMinLength)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = ErrorMessageConstants.PasswordConfirmationDoesNotMatch)]
        public string ConfirmPassword { get; set; }
    }
}