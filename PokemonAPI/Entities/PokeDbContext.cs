using Microsoft.EntityFrameworkCore;

namespace PokemonAPI.Entities
{
    public class PokeDbContext : DbContext
    {
        public DbSet<Backpack> Backpacks { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Move> Moves { get; set; }
        public DbSet<Pokemon> Pokemons { get; set; }
        public DbSet<Type> Types { get; set; }
        public DbSet<BackpackItem> BackpackItems { get; set; }
        public DbSet<BackpackMove> BackpackMoves { get; set; }
        public DbSet<BackpackPokemon> BackpackPokemons { get; set; }

        public DbSet<PokemonType> PokemonTypes { get; set; }
        
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }

        public PokeDbContext(DbContextOptions<PokeDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(u => u.Username)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired();

            modelBuilder.Entity<Role>()
                .Property(u => u.Name)
                .IsRequired();

            modelBuilder.Entity<BackpackItem>()
                .HasKey(b => new { b.BackpackId, b.ItemId });

            modelBuilder.Entity<BackpackMove>()
                .HasKey(b => new { b.BackpackId, b.MoveId });

            modelBuilder.Entity<BackpackPokemon>()
                .HasKey(b => new { b.BackpackId, b.PokemonId });


            modelBuilder.Entity<PokemonType>()
                .HasKey(b => new { b.PokemonId, b.TypeId });

        }
    }
}
