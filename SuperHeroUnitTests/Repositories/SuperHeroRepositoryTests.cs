using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHeroUnitTests.Repositories
{
    public class SuperHeroRepositoryTests
    {
        private readonly DbContextOptions<SuperHeroDbContext> m_superHeroOptions;
        private readonly SuperHeroDbContext m_superHeroContext;
        private readonly SuperHeroRepository m_superHeroRepository;

        public SuperHeroRepositoryTests()
        {
            m_superHeroOptions = new DbContextOptionsBuilder<SuperHeroDbContext>()
                .UseInMemoryDatabase( databaseName: "SuperHeroRepository" )
                .Options;

            m_superHeroContext = new( m_superHeroOptions );

            m_superHeroRepository = new( m_superHeroContext );
        }

        [Fact]
        public async void GetAll_ShouldReturnListOfSuperHeroes_WhenSuperHeroExists()
        {
            // Arrange
            await m_superHeroContext.Database.EnsureDeletedAsync();

            Team team = new Team()
            {
                TeamID = 1,
                TeamName = "Justice League"
            };

            Team team1 = new Team()
            {
                TeamID = 2,
                TeamName = "Avengers"
            };

            m_superHeroContext.Add( team );
            m_superHeroContext.Add( team1 );

            await m_superHeroContext.SaveChangesAsync();

            m_superHeroContext.SuperHero.Add( new SuperHero
            {
                SuperHeroID = 1,
                Name = "Superman",
                FirstName = "Clark",
                LastName = "Kent",
                TeamID = 1,
                Place = "Metropolis",
                Debut = 1938
            } );

            m_superHeroContext.SuperHero.Add( new SuperHero
            {
                SuperHeroID = 2,
                Name = "Iron Man",
                FirstName = "Tony",
                LastName = "Stark",
                TeamID = 2,
                Place = "Malibu",
                Debut = 1963
            } );

            await m_superHeroContext.SaveChangesAsync();

            // Act
            var result = await m_superHeroRepository.GetAll();

            // Assert
            Assert.NotNull( result );
            Assert.IsType<List<SuperHero>>( result );
            Assert.Equal( 2, result.Count );
        }

        [Fact]
        public async void GetAll_ShouldReturnEmptyListOfSuperHeroes_WhenNoSuperHeroesExists()
        {
            // Arrange
            await m_superHeroContext.Database.EnsureDeletedAsync();

            // Act
            var result = await m_superHeroRepository.GetAll();

            // Assert
            Assert.NotNull( result );
            Assert.IsType<List<SuperHero>>( result );
            Assert.Empty( result );
        }

        [Fact]
        public async void GetById_ShouldReturnSuperHero_WhenSuperHeroExists()
        {
            // Arrange
            await m_superHeroContext.Database.EnsureDeletedAsync();

            Team team = new Team()
            {
                TeamID = 1,
                TeamName = "Justice League"
            };

            m_superHeroContext.Add( team );

            await m_superHeroContext.SaveChangesAsync();

            int superHeroId = 1;

            m_superHeroContext.SuperHero.Add( new SuperHero
            {
                SuperHeroID = superHeroId,
                Name = "Superman",
                FirstName = "Clark",
                LastName = "Kent",
                TeamID = 1,
                Place = "Metropolis",
                Debut = 1938
            } );

            await m_superHeroContext.SaveChangesAsync();

            // Act
            var result = await m_superHeroRepository.GetById( superHeroId );

            // Assert
            Assert.NotNull( result );
            Assert.IsType<SuperHero>( result );
            Assert.Equal(superHeroId, result.SuperHeroID );
        }

        [Fact]
        public async void GetById_ShouldReturnNull_WhenSuperHeroDoesNotExist()
        {
            // Arrange
            await m_superHeroContext.Database.EnsureDeletedAsync();

            // Act
            var result = await m_superHeroRepository.GetById( 1 );

            // Assert
            Assert.Null( result );
        }

        [Fact]
        public async void Create_ShouldAddNewIdToSuperHero_WhenSavingToDatabase()
        {
            // Arrange
            await m_superHeroContext.Database.EnsureDeletedAsync();

            int expectedNewId = 1;

            SuperHero superHero = new SuperHero()
            {
                Name = "Superman",
                FirstName = "Clark",
                LastName = "Kent",
                TeamID = 1,
                Place = "Metropolis",
                Debut = 1938
            };

            // Act
            var result = await m_superHeroRepository.Create( superHero );

            // Assert
            Assert.NotNull( result );
            Assert.IsType<SuperHero>( result );
            Assert.Equal(expectedNewId, result.SuperHeroID );
        }

        [Fact]
        public async void Create_ShoudFailToAddNewSuperHero_WhenSuperHeroAlreadyExists()
        {
            // Arrange
            await m_superHeroContext.Database.EnsureDeletedAsync();

            SuperHero superHero = new SuperHero()
            {
                SuperHeroID = 1,
                Name = "Superman",
                FirstName = "Clark",
                LastName = "Kent",
                Place = "Metropolis",
                Debut = 1938
            };

            await m_superHeroRepository.Create( superHero );

            // Act
            async Task action() => await m_superHeroRepository.Create( superHero );

            // Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>( action );
            Assert.Contains( "An item with the same key has already been added", ex.Message );
        }

        [Fact]
        public async void Update_ShouldChangeValuesOnSuperHero_WhenSuperHeroExists()
        {
            // Arrange
            await m_superHeroContext.Database.EnsureDeletedAsync();

            Team team = new Team()
            {
                TeamID = 1,
                TeamName = "Justice League"
            };

            Team team1 = new Team()
            {
                TeamID = 2,
                TeamName = "Avengers"
            };

            m_superHeroContext.Add( team );
            m_superHeroContext.Add( team1 );

            await m_superHeroContext.SaveChangesAsync();

            int superHeroId = 1;

            SuperHero superHero = new SuperHero()
            {
                SuperHeroID = superHeroId,
                Name = "Superman",
                FirstName = "Clark",
                LastName = "Kent",
                Place = "Metropolis",
                Debut = 1938,
                TeamID = 1
            };

            m_superHeroContext.Add( superHero );

            await m_superHeroContext.SaveChangesAsync();

            SuperHero updateSuperHero = new SuperHero()
            {
                SuperHeroID = superHeroId,
                Name = "new Superman",
                FirstName = "new Clark",
                LastName = "new Kent",
                Place = "new Metropolis",
                Debut = 1999,
                TeamID = 2
            };

            // Act
            var result = await m_superHeroRepository.Update( superHeroId, updateSuperHero );

            // Assert
            Assert.NotNull( result );
            Assert.IsType<SuperHero>( result );
            Assert.Equal(superHeroId, result.SuperHeroID );
            Assert.Equal( updateSuperHero.Name, result.Name );
            Assert.Equal( updateSuperHero.FirstName, result.FirstName );
            Assert.Equal( updateSuperHero.LastName, result.LastName );
            Assert.Equal( updateSuperHero.Place, result.Place );
            Assert.Equal( updateSuperHero.Debut, result.Debut );
            Assert.Equal( updateSuperHero.TeamID, result.TeamID );
        }

        [Fact]
        public async void Update_ShouldReturnNull_WhenSuperHeroDoesNotExist()
        {
            // Arrange
            await m_superHeroContext.Database.EnsureDeletedAsync();

            int superHeroId = 1;

            SuperHero updateSuperHero = new SuperHero()
            {
                SuperHeroID = superHeroId,
                Name = "Superman",
                FirstName = "Clark",
                LastName = "Kent",
                TeamID = 1,
                Place = "Metropolis",
                Debut = 1938
            };

            // Act
            var result = await m_superHeroRepository.Update( superHeroId, updateSuperHero );

            // Assert
            Assert.Null( result );
        }

        [Fact]
        public async void Delete_ShouldReturnDeletedSuperHero_WhenSuperHeroIsDeleted()
        {
            // Arrange
            await m_superHeroContext.Database.EnsureDeletedAsync();

            Team team = new Team()
            {
                TeamID = 2,
                TeamName = "Avengers"
            };

            m_superHeroContext.Add( team );

            await m_superHeroContext.SaveChangesAsync();

            int superHeroId = 1;

            SuperHero newSuperHero = new SuperHero()
            {
                SuperHeroID = superHeroId,
                Name = "new Superman",
                FirstName = "new Clark",
                LastName = "new Kent",
                TeamID = 2,
                Place = "new Metropolis",
                Debut = 1999
            };

            m_superHeroContext.SuperHero.Add( newSuperHero );
            await m_superHeroContext.SaveChangesAsync();

            // Act
            var result = await m_superHeroRepository.Delete( superHeroId );
            var superHero = await m_superHeroRepository.GetById( superHeroId );

            // Assert
            Assert.NotNull( result );
            Assert.IsType<SuperHero>( result );
            Assert.Equal(superHeroId, result.SuperHeroID );

            Assert.Null( superHero );
        }

        [Fact]
        public async void Delete_ShouldReturnNull_WhenSuperHeroDoesNotExist()
        {
            // Arrange
            await m_superHeroContext.Database.EnsureDeletedAsync();

            // Act
            var result = await m_superHeroRepository.Delete( 1 );

            // Assert
            Assert.Null( result );
        }
    }
}
