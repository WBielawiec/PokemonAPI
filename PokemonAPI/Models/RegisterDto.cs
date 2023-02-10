using System.ComponentModel.DataAnnotations;

namespace PokemonAPI.Models
{
    public class RegisterDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }

        /*public int BackpackId { get; set; }*/
        public int RoleId { get; set; } = 1;
    }
}
