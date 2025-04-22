using System.ComponentModel.DataAnnotations;

namespace Demo.presentation.ViewModels.Auth
{
    public class LoginViewModel
    {
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
       
        public bool RememberMe { get; set; }
    }
}
