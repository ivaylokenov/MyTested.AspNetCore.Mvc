using System.ComponentModel.DataAnnotations;

namespace ApplicationParts.Models.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
