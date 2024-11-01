using System.ComponentModel.DataAnnotations;

namespace Pronia.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;
    }
}
