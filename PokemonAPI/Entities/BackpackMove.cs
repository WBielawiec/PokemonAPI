namespace PokemonAPI.Entities
{
    public class BackpackMove
    {
        public int BackpackId { get; set; }
        public virtual Backpack Backpack { get; set; }
        public int MoveId { get; set; }
        public virtual Move Move { get; set; }
    }
}
