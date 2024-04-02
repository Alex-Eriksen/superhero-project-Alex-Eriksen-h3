using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHeroUnitTests.Controllers
{
    public class SuperHeroControllerTests
    {
        private readonly SuperHeroController m_superHeroController;
        private Mock<ISuperHeroService> m_superHeroServiceMock = new();

        public SuperHeroControllerTests()
        {
            m_superHeroController = new SuperHeroController( m_superHeroServiceMock.Object );
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode200_WhenSuperHeroesExists()
        {
            // Arrange
            List<SuperHeroResponse> superHeroes = new();

            superHeroes.Add( new SuperHeroResponse
            {
                Id = 1,
                Name = "Superman",
                FirstName = "Clark",
                LastName = "Kent",
                Place = "Metropolis",
                Debut = 1938
            } );

            m_superHeroServiceMock.Setup( x => x.GetAll() ).ReturnsAsync(superHeroes);

            // Act
            var result = (IStatusCodeActionResult) await m_superHeroController.GetAll();

            // Assert
            Assert.Equal( 200, result.StatusCode );
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode204_WhenNoSuperHeroesExists()
        {
            // Arrange
            List<SuperHeroResponse> superHeroes = new();

            m_superHeroServiceMock.Setup( x => x.GetAll() ).ReturnsAsync( superHeroes );

            // Act
            var result = (IStatusCodeActionResult) await m_superHeroController.GetAll();

            // Assert
            Assert.Equal( 204, result.StatusCode );
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode500_WhenServiceReturnsNull()
        {
            // Arrange
            m_superHeroServiceMock.Setup( x => x.GetAll() ).ReturnsAsync( () => null );

            // Act
            var result = (IStatusCodeActionResult) await m_superHeroController.GetAll();

            // Assert
            Assert.Equal( 500, result.StatusCode );
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            m_superHeroServiceMock.Setup( x => x.GetAll() ).ReturnsAsync( () => throw new Exception("This is an Exception.") );

            // Act
            var result = (IStatusCodeActionResult) await m_superHeroController.GetAll();

            // Assert
            Assert.Equal( 500, result.StatusCode );
        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode200_WhenSuperHeroesExists()
        {
            // Arrange
            int superHeroId = 1;

            SuperHeroResponse superHero = new SuperHeroResponse()
            {
                Id = superHeroId,
                Name = "Superman",
                FirstName = "Clark",
                LastName = "Kent",
                Place = "Metropolis",
                Debut = 1938
            };

            m_superHeroServiceMock
                .Setup( x => x.GetById(It.IsAny<int>()) )
                .ReturnsAsync( superHero );

            // Act
            var result = (IStatusCodeActionResult) await m_superHeroController.GetById( superHeroId );

            // Assert
            Assert.Equal( 200, result.StatusCode );
        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode404_WhenSuperHeroDoestNotExist()
        {
            // Arrange
            int superHeroId = 1;

            m_superHeroServiceMock
                .Setup( x => x.GetById( It.IsAny<int>() ) )
                .ReturnsAsync( () => null );

            // Act
            var result = (IStatusCodeActionResult) await m_superHeroController.GetById( superHeroId );

            // Assert
            Assert.Equal( 404, result.StatusCode );
        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            m_superHeroServiceMock
                .Setup( x => x.GetById(It.IsAny<int>()) )
                .ReturnsAsync( () => throw new Exception( "This is an Exception." ) );

            // Act
            var result = (IStatusCodeActionResult) await m_superHeroController.GetById( 1 );

            // Assert
            Assert.Equal( 500, result.StatusCode );
        }

        [Fact]
        public async void Create_ShouldReturnStatusCode200_WhenSuperHeroIsSuccessfullyCreated()
        {
            // Arrange
            SuperHeroRequest superHeroRequest = new SuperHeroRequest()
            {
                Name = "Superman",
                FirstName = "Clark",
                LastName = "Kent",
                TeamID = 1,
                Place = "Metropolis",
                Debut = 1938
            };

            int superHeroId = 1;

            SuperHeroResponse superHeroResponse = new SuperHeroResponse()
            {
                Id = superHeroId,
                Name = "Superman",
                FirstName = "Clark",
                LastName = "Kent",
                Place = "Metropolis",
                Debut = 1938
            };

            m_superHeroServiceMock
                .Setup( x => x.Create( It.IsAny<SuperHeroRequest>() ) )
                .ReturnsAsync( superHeroResponse );

            // Act
            var result = (IStatusCodeActionResult) await m_superHeroController.Create( superHeroRequest );

            // Assert
            Assert.Equal( 200, result.StatusCode );
        }

        [Fact]
        public async void Create_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            SuperHeroRequest superHeroRequest = new SuperHeroRequest()
            {
                Name = "Superman",
                FirstName = "Clark",
                LastName = "Kent",
                TeamID = 1,
                Place = "Metropolis",
                Debut = 1938
            };

            m_superHeroServiceMock
                .Setup( x => x.Create( It.IsAny<SuperHeroRequest>() ) )
                .ReturnsAsync( () => throw new Exception( "This is an Exception." ) );

            // Act
            var result = (IStatusCodeActionResult) await m_superHeroController.Create( superHeroRequest );

            // Assert
            Assert.Equal( 500, result.StatusCode );
        }

        [Fact]
        public async void Update_ShouldReturnStatusCode200_WhenSuperHeroIsSuccessfullyUpdated()
        {
            // Arrange
            SuperHeroRequest updateSuperHero = new SuperHeroRequest()
            {
                Name = "Superman",
                FirstName = "Clark",
                LastName = "Kent",
                TeamID = 1,
                Place = "Metropolis",
                Debut = 1938
            };

            int superHeroId = 1;

            SuperHeroResponse superHeroResponse = new SuperHeroResponse()
            {
                Id = superHeroId,
                Name = "Superman",
                FirstName = "Clark",
                LastName = "Kent",
                Place = "Metropolis",
                Debut = 1938
            };

            m_superHeroServiceMock
                .Setup( x => x.Update( It.IsAny<int>(), It.IsAny<SuperHeroRequest>() ) )
                .ReturnsAsync( superHeroResponse );

            // Act
            var result = (IStatusCodeActionResult) await m_superHeroController.Update( superHeroId, updateSuperHero );

            // Assert
            Assert.Equal( 200, result.StatusCode );
        }

        [Fact]
        public async void Update_ShouldReturnStatusCode404_WhenSuperHeroDoestNotExist()
        {
            // Arrange
            SuperHeroRequest updateSuperHero = new SuperHeroRequest()
            {
                Name = "Superman",
                FirstName = "Clark",
                LastName = "Kent",
                TeamID = 1,
                Place = "Metropolis",
                Debut = 1938
            };

            int superHeroId = 1;

            m_superHeroServiceMock
                .Setup( x => x.Update( It.IsAny<int>(), It.IsAny<SuperHeroRequest>() ) )
                .ReturnsAsync( () => null );

            // Act
            var result = (IStatusCodeActionResult) await m_superHeroController.Update( superHeroId, updateSuperHero );

            // Assert
            Assert.Equal( 404, result.StatusCode );
        }

        [Fact]
        public async void Update_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            SuperHeroRequest updateSuperHero = new SuperHeroRequest()
            {
                Name = "Superman",
                FirstName = "Clark",
                LastName = "Kent",
                TeamID = 1,
                Place = "Metropolis",
                Debut = 1938
            };

            int superHeroId = 1;

            m_superHeroServiceMock
                .Setup( x => x.Update( It.IsAny<int>(), It.IsAny<SuperHeroRequest>() ) )
                .ReturnsAsync( () => throw new Exception( "This is an Exception." ) );

            // Act
            var result = (IStatusCodeActionResult) await m_superHeroController.Update( superHeroId, updateSuperHero );

            // Assert
            Assert.Equal( 500, result.StatusCode );
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode200_WhenSuperHeroIsSuccessfullyDeleted()
        {
            // Arrange
            int superHeroId = 1;

            SuperHeroResponse superHeroResponse = new SuperHeroResponse()
            {
                Id = superHeroId,
                Name = "Superman",
                FirstName = "Clark",
                LastName = "Kent",
                Place = "Metropolis",
                Debut = 1938
            };

            m_superHeroServiceMock
                .Setup( x => x.Delete( It.IsAny<int>() ) )
                .ReturnsAsync( superHeroResponse );

            // Act
            var result = (IStatusCodeActionResult) await m_superHeroController.Delete( superHeroId );

            // Assert
            Assert.Equal( 200, result.StatusCode );
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode404_WhenSuperHeroDoestNotExist()
        {
            // Arrange
            int superHeroId = 1;

            m_superHeroServiceMock
                .Setup( x => x.Delete( It.IsAny<int>() ) )
                .ReturnsAsync( () => null );

            // Act
            var result = (IStatusCodeActionResult) await m_superHeroController.Delete( superHeroId );

            // Assert
            Assert.Equal( 404, result.StatusCode );
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            int superHeroId = 1;

            m_superHeroServiceMock
                .Setup( x => x.Delete( It.IsAny<int>() ) )
                .ReturnsAsync( () => throw new Exception( "This is an Exception." ) );

            // Act
            var result = (IStatusCodeActionResult) await m_superHeroController.Delete( superHeroId );

            // Assert
            Assert.Equal( 500, result.StatusCode );
        }
    }
}
