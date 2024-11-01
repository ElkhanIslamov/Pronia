using System.ComponentModel.DataAnnotations;

namespace Pronia.ViewModels
{
    public class SubmitResetPasswordViewModel
    {
        [Required,DataType(DataType.Password)]
        public string Password { get; set; }
        [Required, DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
