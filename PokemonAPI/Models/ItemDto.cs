using System.ComponentModel.DataAnnotations;

namespace PokemonAPI.Models
{
    public class ItemDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
