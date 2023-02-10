using PokemonAPI.Entities;
using System.Collections.Generic;

namespace PokemonAPI.Models
{
    public class PokemonDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        //base information
        public int HP { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int SpAttack { get; set; }
        public int SpDefense { get; set; }
        public int Speed { get; set; }

        //pokemon type list
        //public ICollection<PokemonType> Types { get; set; }

        public ICollection<string> Types { get; set; }
    }
}
