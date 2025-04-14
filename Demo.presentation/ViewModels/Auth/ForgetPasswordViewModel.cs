using System.ComponentModel.DataAnnotations;

namespace Demo.presentation.ViewModels.Auth
{
    public class ForgetPasswordViewModel
    {
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; } = null!;
    }
}
