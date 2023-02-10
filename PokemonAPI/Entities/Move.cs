using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokemonAPI.Entities
{
    public class Move
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public int? Accuracy { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public int? Power { get; set; }
        public int? PP { get; set; }
        public string Type { get; set; }
        public int? TM { get; set; }

        public ICollection<BackpackMove> BackpackMoves { get; set; }
    }
}
