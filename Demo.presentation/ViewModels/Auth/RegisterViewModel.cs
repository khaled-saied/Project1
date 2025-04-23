using System.ComponentModel.DataAnnotations;

namespace Demo.presentation.ViewModels.Auth
{
    public class RegisterViewModel
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        [Required(ErrorMessage = "The name is already in use") ]
        [MaxLength(50)]
        public string UserName { get; set; } // Unique 
        [Required(ErrorMessage = "The email is already in use")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
        public bool IsAgree { get; set; }
    }
}
