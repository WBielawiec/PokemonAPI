using System.Collections.Generic;

namespace PokemonAPI.Entities
{
    public class Backpack
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual User Owner { get; set; }

        public virtual ICollection<BackpackPokemon> BackpackPokemons { get; set; }
        public virtual ICollection<BackpackItem> BackpackItems  { get; set; }
        public virtual ICollection<BackpackMove> BackpackMoves  { get; set; }

    }
}
