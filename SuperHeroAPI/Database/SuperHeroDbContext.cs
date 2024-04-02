namespace SuperHeroAPI.Database
{
    public class SuperHeroDbContext : DbContext
    {
        public SuperHeroDbContext( DbContextOptions<SuperHeroDbContext> options ) : base( options ) { }

        public DbSet<SuperHero> SuperHero { get; set; }
        public DbSet<Team> Team { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder )
        {
            modelBuilder.Entity<Team>()
                .HasMany( e => e.SuperHeroes )
                .WithOne( e => e.Team )
                .OnDelete( DeleteBehavior.Restrict );

            modelBuilder.Entity<SuperHero>()
                .HasOne( e => e.Team )
                .WithMany( e => e.SuperHeroes );

            modelBuilder.Entity<Team>().HasData(
                new Team
                {
                    TeamID = 1,
                    TeamName = "Justice League"
                },
                new Team
                {
                    TeamID = 2,
                    TeamName = "Avengers"
                } );

            modelBuilder.Entity<SuperHero>().HasData(
                new SuperHero
                {
                    SuperHeroID = 1,
                    Name = "Superman",
                    FirstName = "Clark",
                    LastName = "Kent",
                    Place = "Metropolis",
                    Debut = 1938,
                    TeamID = 1
                },
                new SuperHero
                {
                    SuperHeroID = 2,
                    Name = "Iron Man",
                    FirstName = "Tony",
                    LastName = "Stark",
                    Place = "Malibu",
                    Debut = 1963,
                    TeamID = 2
                });
        }
    }
}
