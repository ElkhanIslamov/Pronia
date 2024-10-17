using System.ComponentModel.DataAnnotations;

namespace Pronia.ViewModels
{
    public class LoginViewModel
    {
        [Required]
<<<<<<< HEAD
        public string UsernameOrEmail { get; set; }
=======
        public string UserNameOrEmail { get; set; }
>>>>>>> 57991215b1568482bb7c3fa294a1a116d2b1b740
        [Required,DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
