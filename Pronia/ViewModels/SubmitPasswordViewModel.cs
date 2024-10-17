using System.ComponentModel.DataAnnotations;

namespace Pronia.ViewModels
{
    public class SubmitPasswordViewModel
    {
<<<<<<< HEAD
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
        [Required, DataType(DataType.Password),Compare(nameof(Password))]
=======
        [Required,DataType(DataType.Password)]
        public string Password { get; set; }
        [Required,DataType(DataType.Password),Compare(nameof(Password))]
>>>>>>> 57991215b1568482bb7c3fa294a1a116d2b1b740
        public string ConfirmPassword { get; set; }
    }
}
