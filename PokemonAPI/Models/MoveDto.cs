namespace PokemonAPI.Models
{
    public class MoveDto
    {
        public int Id { get; set; }
        public int? Accuracy { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public int? Power { get; set; }
        public int? PP { get; set; }
        public string Type { get; set; }
        public int? TM { get; set; }
    }
}
