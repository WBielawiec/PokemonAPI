using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokemonAPI.Entities
{
    public class Pokemon
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<PokemonType> Type { get; set; }
        public int BaseId { get; set; }
        public virtual Base Base { get; set; }
        public string Description { get; set; }

        public ICollection<BackpackPokemon> BackpackPokemons { get; set; }
    }

    public class Type
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<PokemonType> Pokemons { get; set; }
    }

    public class Base
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public int HP { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int SpAttack { get; set; }
        public int SpDefense { get; set; }
        public int Speed { get; set; }
        public virtual Pokemon Pokemon { get; set; }
    }
}