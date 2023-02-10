namespace PokemonAPI.Entities
{
    public class BackpackItem
    {
        public int BackpackId { get; set; }
        public virtual Backpack Backpack { get; set; }
        public int ItemId { get; set; }
        public virtual Item Item { get; set; }
    }
}
