namespace PokemonAPI.Entities
{
    public class PokemonType
    {
        public int PokemonId { get; set; }
        public virtual Pokemon Pokemon { get; set; }

        public int TypeId { get; set; }
        public virtual Type Type { get; set; }
    }
}
