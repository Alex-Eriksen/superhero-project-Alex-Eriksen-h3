using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHeroUnitTests.Services
{
    public class SuperHeroServiceTests
    {
        private readonly SuperHeroService m_service;
        private readonly Mock<ISuperHeroRepository> m_superHeroRepositoryMock = new Mock<ISuperHeroRepository>();

        public SuperHeroServiceTests()
        {
            m_service = new SuperHeroService( m_superHeroRepositoryMock.Object );
        }

        [Fact]
        public async void GetAll_ShouldReturnListOfSuperHeroResponses_WhenSuperHeroesExists()
        {
            // Arrange
            List<SuperHero> superHeroes = new List<SuperHero>();

            superHeroes.Add( new SuperHero
            {
                SuperHeroID = 1,
                Name = "Superman",
                FirstName = "Clark",
                LastName = "Kent",
                Place = "Metropolis",
                Debut = 1938
            } );

            superHeroes.Add( new SuperHero
            {
                SuperHeroID = 2,
                Name = "Iron Man",
                FirstName = "Tony",
                LastName = "Stark",
                Place = "Malibu",
                Debut = 1963
            } );

            m_superHeroRepositoryMock.Setup( x => x.GetAll() ).ReturnsAsync( superHeroes );

            // Act
            var result = await m_service.GetAll();

            // Assert
            Assert.NotNull( result );
            Assert.Equal( 2, result.Count );
            Assert.IsType<List<SuperHeroResponse>>( result );
        }

        [Fact]
        public async void GetAll_ShouldReturnEmptyListOfSuperHeroesResponses_WhenNoSuperHeroesExists()
        {
            // Arrange
            List<SuperHero> superHeroes = new();

            m_superHeroRepositoryMock.Setup( x => x.GetAll() ).ReturnsAsync( superHeroes );

            // Act
            var result = await m_service.GetAll();

            // Assert
            Assert.NotNull( result );
            Assert.Empty( result );
            Assert.IsType<List<SuperHeroResponse>>( result );
        }

        [Fact]
        public async void GetById_ShouldReturnSuperHeroResponse_WhenSuperHeroExists()
        {
            // Arrange
            int superHeroId = 1;

            SuperHero superHero = new SuperHero()
            {
                SuperHeroID = 1,
                Name = "Superman",
                FirstName = "Clark",
                LastName = "Kent",
                TeamID = 1,
                Place = "Metropolis",
                Debut = 1938
            };

            m_superHeroRepositoryMock
                .Setup( x => x.GetById( It.IsAny<int>() ) )
                .ReturnsAsync(superHero);

            // Act
            var result = await m_service.GetById( superHeroId );

            // Assert
            Assert.NotNull( result );
            Assert.IsType<SuperHeroResponse>( result );
            Assert.Equal( superHeroId, result.Id );
            Assert.Equal( superHero.Name, result.Name );
            Assert.Equal( superHero.FirstName, result.FirstName );
            Assert.Equal( superHero.LastName, result.LastName );
            Assert.Equal( superHero.Place, result.Place );
            Assert.Equal( superHero.Debut, result.Debut );
        }

        [Fact]
        public async void GetById_ShouldReturnNull_WhenSuperHeroDoesNotExist()
        {
            // Arrange
            int superHeroId = 1;

            m_superHeroRepositoryMock
                .Setup( x => x.GetById( It.IsAny<int>() ) )
                .ReturnsAsync(() => null);

            // Act
            var result = await m_service.GetById( superHeroId );

            // Assert
            Assert.Null( result );
        }

        [Fact]
        public async void Create_ShouldReturnSuperHeroResponse_WhenCreatedIsSuccess()
        {
            // Arrange
            SuperHeroRequest newSuperHero = new SuperHeroRequest
            {
                Name = "Superman",
                FirstName = "Clark",
                LastName = "Kent",
                Place = "Metropolis",
                Debut = 1938
            };

            int superHeroId = 1;

            SuperHero superHero = new SuperHero
            {
                SuperHeroID = superHeroId,
                Name = "Superman",
                FirstName = "Clark",
                LastName = "Kent",
                Place = "Metropolis",
                Debut = 1938
            };

            m_superHeroRepositoryMock
                .Setup( x => x.Create( It.IsAny<SuperHero>() ) )
                .ReturnsAsync( superHero );

            // Act
            var result = await m_service.Create( newSuperHero );

            // Assert
            Assert.NotNull( result );
            Assert.IsType<SuperHeroResponse>( result );
            Assert.Equal( superHeroId, result.Id );
            Assert.Equal( superHero.Name, result.Name );
            Assert.Equal( superHero.FirstName, result.FirstName );
            Assert.Equal( superHero.LastName, result.LastName );
            Assert.Equal( superHero.Place, result.Place );
            Assert.Equal( superHero.Debut, result.Debut );
        }

        [Fact]
        public async void Create_ShouldReturnNull_WhenRepositoryReturnsNull()
        {
            // Arrange
            SuperHeroRequest newSuperHero = new SuperHeroRequest()
            {
                Name = "Superman",
                FirstName = "Clark",
                LastName = "Kent",
                TeamID = 1,
                Place = "Metropolis",
                Debut = 1938
            };

            m_superHeroRepositoryMock
                .Setup( x => x.Create( It.IsAny<SuperHero>() ) )
                .ReturnsAsync( () => null );

            // Act
            var result = await m_service.Create( newSuperHero );

            // Assert
            Assert.Null( result );
        }

        [Fact]
        public async void Update_ShouldReturnSuperHeroResponse_WhenUpdateIsSuccess()
        {
            // Arrange
            SuperHeroRequest superHeroRequest = new SuperHeroRequest
            {
                Name = "Superman",
                FirstName = "Clark",
                LastName = "Kent",
                TeamID = 1,
                Place = "Metropolis",
                Debut = 1938
            };

            int superHeroId = 1;

            SuperHero superHero = new SuperHero
            {
                SuperHeroID = superHeroId,
                Name = "Superman",
                FirstName = "Clark",
                LastName = "Kent",
                TeamID = 1,
                Place = "Metropolis",
                Debut = 1938
            };

            m_superHeroRepositoryMock
                .Setup( x => x.Update( It.IsAny<int>(), It.IsAny<SuperHero>() ) )
                .ReturnsAsync( superHero );

            // Act
            var result = await m_service.Update( superHeroId, superHeroRequest );

            // Assert
            Assert.NotNull( result );
            Assert.IsType<SuperHeroResponse>( result );
            Assert.Equal( superHero.SuperHeroID, result.Id );
            Assert.Equal( superHeroRequest.Name, result.Name );
            Assert.Equal( superHeroRequest.FirstName, result.FirstName );
            Assert.Equal( superHeroRequest.LastName, result.LastName );
            Assert.Equal( superHeroRequest.Place, result.Place );
            Assert.Equal( superHeroRequest.Debut, result.Debut );
        }

        [Fact]
        public async void Update_ShouldReturnNull_WhenSuperHeroDoesNotExist()
        {
            // Arrange
            SuperHeroRequest superHeroRequest = new SuperHeroRequest
            {
                Name = "Superman",
                FirstName = "Clark",
                LastName = "Kent",
                Place = "Metropolis",
                Debut = 1938
            };

            int superHeroId = 1;

            m_superHeroRepositoryMock
                .Setup( x => x.Update( It.IsAny<int>(), It.IsAny<SuperHero>() ) )
                .ReturnsAsync( () => null );

            // Act
            var result = await m_service.Update( superHeroId, superHeroRequest );

            // Assert
            Assert.Null( result );
        }

        [Fact]
        public async void Delete_ShouldReturnSuperHeroResponse_WhenDeleteIsSuccess()
        {
            // Arrange
            int superHeroId = 1;

            SuperHero superHero = new SuperHero
            {
                SuperHeroID = superHeroId,
                Name = "Superman",
                FirstName = "Clark",
                LastName = "Kent",
                Place = "Metropolis",
                Debut = 1938
            };

            m_superHeroRepositoryMock
                .Setup( x => x.Delete( It.IsAny<int>() ) )
                .ReturnsAsync( superHero );

            // Act
            var result = await m_service.Delete( superHeroId );

            // Assert
            Assert.NotNull( result );
            Assert.IsType<SuperHeroResponse>( result );
            Assert.Equal( superHeroId, result.Id );
        }

        [Fact]
        public async void Delete_ShouldReturnNull_WhenSuperHeroDoesNotExist()
        {
            // Arrange
            int superHeroId = 1;

            m_superHeroRepositoryMock
                .Setup( x => x.Delete( It.IsAny<int>() ) )
                .ReturnsAsync( () => null );

            // Act
            var result = await m_service.Delete( superHeroId );

            // Assert
            Assert.Null( result );
        }
    }
}
