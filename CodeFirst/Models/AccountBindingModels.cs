using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace CodeFirst.Models
{
    public class CreateUserBindingModel
    {
        [Required]
        [EmailAddress]
        [JsonProperty(PropertyName = "email")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Username"), StringLength(12, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        [JsonProperty(PropertyName = "usuario")]
        public string Username { get; set; }

        [Required]
        [Display(Name = "First Name"), StringLength(60, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        [JsonProperty(PropertyName = "nome")]
        public string Nome { get; set; }

        [Required]
        [Display(Name = "Last Name"), StringLength(60, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        [JsonProperty(PropertyName = "sobrenome")]
        public string Sobrenome { get; set; }

        [Display(Name = "Role Name")]
        [JsonProperty(PropertyName = "roleName")]
        public string RoleName { get; set; }

        [Required]
        [JsonProperty(PropertyName = "password")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [JsonProperty(PropertyName = "confirmePassword")]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}