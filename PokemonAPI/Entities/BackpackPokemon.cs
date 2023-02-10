namespace PokemonAPI.Entities
{
    public class BackpackPokemon
    {
        public int BackpackId { get; set; }
        public virtual Backpack Backpack { get; set; }
        public int PokemonId { get; set; }
        public virtual Pokemon pokemon { get; set; }
    }
}
