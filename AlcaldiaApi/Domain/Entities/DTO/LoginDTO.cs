using System.ComponentModel.DataAnnotations;

namespace AlcaldiaApi.Domain.Entities.DTO
{
    public class LoginDTO
    {
        [EmailAddress]
        [Required]        
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
