using System.ComponentModel.DataAnnotations;

namespace Pronia.ViewModels
{
    public class ResetPasswordViewModel
    {
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
