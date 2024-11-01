using System.ComponentModel.DataAnnotations;

namespace Pronia.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Required,DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
