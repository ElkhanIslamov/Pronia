using System.ComponentModel.DataAnnotations;

namespace Pronia.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
<<<<<<< HEAD
        public string Fullname  { get; set; }
=======
        public string Fullname { get; set; }
>>>>>>> 57991215b1568482bb7c3fa294a1a116d2b1b740
        [Required]
        public string Username { get; set; }
        [Required,DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required,DataType(DataType.Password)]
<<<<<<< HEAD
        public string Password { get; set; }
        [DataType(DataType.Password),Compare(nameof(Password))]
        public string PasswordConfirm { get; set; }

=======
        public string   Password    { get; set; }
        [DataType(DataType.Password),Compare(nameof(Password))]
        public string PasswordConfirm { get; set; }
>>>>>>> 57991215b1568482bb7c3fa294a1a116d2b1b740
    }
}
