using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHeroUnitTests.Controllers
{
    public class TeamControllerTests
    {
        private readonly TeamController m_teamController;
        private Mock<ITeamService> m_teamServiceMock = new();

        public TeamControllerTests()
        {
            m_teamController = new TeamController( m_teamServiceMock.Object );
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode200_WhenTeamExists()
        {
            // Arrange
            List<TeamResponse> teams = new();

            teams.Add( new TeamResponse
            {
                TeamID = 1,
                TeamName = "Justice League"
            } );

            m_teamServiceMock.Setup( x => x.GetAll() ).ReturnsAsync(teams);

            // Act
            var result = (IStatusCodeActionResult) await m_teamController.GetAll();

            // Assert
            Assert.Equal( 200, result.StatusCode );
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode204_WhenNoTeamsExists()
        {
            // Arrange
            List<TeamResponse> teams = new();

            m_teamServiceMock.Setup( x => x.GetAll() ).ReturnsAsync( teams );

            // Act
            var result = (IStatusCodeActionResult) await m_teamController.GetAll();

            // Assert
            Assert.Equal( 204, result.StatusCode );
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode500_WhenServiceReturnsNull()
        {
            // Arrange
            m_teamServiceMock.Setup( x => x.GetAll() ).ReturnsAsync( () => null );

            // Act
            var result = (IStatusCodeActionResult) await m_teamController.GetAll();

            // Assert
            Assert.Equal( 500, result.StatusCode );
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            m_teamServiceMock.Setup( x => x.GetAll() ).ReturnsAsync( () => throw new Exception("This is an Exception.") );

            // Act
            var result = (IStatusCodeActionResult) await m_teamController.GetAll();

            // Assert
            Assert.Equal( 500, result.StatusCode );
        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode200_WhenTeamsExists()
        {
            // Arrange
            int teamId = 1;

            TeamResponse team = new TeamResponse()
            {
                TeamID = teamId,
                TeamName = "Justice League"
            };

            m_teamServiceMock
                .Setup( x => x.GetById(It.IsAny<int>()) )
                .ReturnsAsync( team );

            // Act
            var result = (IStatusCodeActionResult) await m_teamController.GetById( teamId );

            // Assert
            Assert.Equal( 200, result.StatusCode );
        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode404_WhenTeamDoestNotExist()
        {
            // Arrange
            int teamId = 1;

            m_teamServiceMock
                .Setup( x => x.GetById( It.IsAny<int>() ) )
                .ReturnsAsync( () => null );

            // Act
            var result = (IStatusCodeActionResult) await m_teamController.GetById( teamId );

            // Assert
            Assert.Equal( 404, result.StatusCode );
        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            m_teamServiceMock
                .Setup( x => x.GetById(It.IsAny<int>()) )
                .ReturnsAsync( () => throw new Exception( "This is an Exception." ) );

            // Act
            var result = (IStatusCodeActionResult) await m_teamController.GetById( 1 );

            // Assert
            Assert.Equal( 500, result.StatusCode );
        }

        [Fact]
        public async void Create_ShouldReturnStatusCode200_WhenTeamIsSuccessfullyCreated()
        {
            // Arrange
            TeamRequest teamRequest = new TeamRequest()
            {
                TeamName = "Justice League"
            };

            int teamId = 1;

            TeamResponse teamResponse = new TeamResponse()
            {
                TeamID = teamId,
                TeamName = "Justice League"
            };

            m_teamServiceMock
                .Setup( x => x.Create( It.IsAny<TeamRequest>() ) )
                .ReturnsAsync( teamResponse );

            // Act
            var result = (IStatusCodeActionResult) await m_teamController.Create( teamRequest );

            // Assert
            Assert.Equal( 200, result.StatusCode );
        }

        [Fact]
        public async void Create_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            TeamRequest teamRequest = new TeamRequest()
            {
                TeamName = "Justice League"
            };

            m_teamServiceMock
                .Setup( x => x.Create( It.IsAny<TeamRequest>() ) )
                .ReturnsAsync( () => throw new Exception( "This is an Exception." ) );

            // Act
            var result = (IStatusCodeActionResult) await m_teamController.Create( teamRequest );

            // Assert
            Assert.Equal( 500, result.StatusCode );
        }

        [Fact]
        public async void Update_ShouldReturnStatusCode200_WhenTeamIsSuccessfullyUpdated()
        {
            // Arrange
            TeamRequest updateTeam = new TeamRequest()
            {
                TeamName = "Justice League"
            };

            int teamId = 1;

            TeamResponse teamResponse = new TeamResponse()
            {
                TeamID = teamId,
                TeamName = "Justice League"
            };

            m_teamServiceMock
                .Setup( x => x.Update( It.IsAny<int>(), It.IsAny<TeamRequest>() ) )
                .ReturnsAsync( teamResponse );

            // Act
            var result = (IStatusCodeActionResult) await m_teamController.Update( teamId, updateTeam );

            // Assert
            Assert.Equal( 200, result.StatusCode );
        }

        [Fact]
        public async void Update_ShouldReturnStatusCode404_WhenTeamDoestNotExist()
        {
            // Arrange
            TeamRequest updateTeam = new TeamRequest()
            {
                TeamName = "Justice League"
            };

            int teamId = 1;

            m_teamServiceMock
                .Setup( x => x.Update( It.IsAny<int>(), It.IsAny<TeamRequest>() ) )
                .ReturnsAsync( () => null );

            // Act
            var result = (IStatusCodeActionResult) await m_teamController.Update( teamId, updateTeam );

            // Assert
            Assert.Equal( 404, result.StatusCode );
        }

        [Fact]
        public async void Update_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            TeamRequest updateTeam = new TeamRequest()
            {
                TeamName = "Justice League"
            };

            int teamId = 1;

            m_teamServiceMock
                .Setup( x => x.Update( It.IsAny<int>(), It.IsAny<TeamRequest>() ) )
                .ReturnsAsync( () => throw new Exception( "This is an Exception." ) );

            // Act
            var result = (IStatusCodeActionResult) await m_teamController.Update( teamId, updateTeam );

            // Assert
            Assert.Equal( 500, result.StatusCode );
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode200_WhenTeamIsSuccessfullyDeleted()
        {
            // Arrange
            int teamId = 1;

            TeamResponse teamResponse = new TeamResponse()
            {
                TeamID = teamId,
                TeamName = "Justice League"
            };

            m_teamServiceMock
                .Setup( x => x.Delete( It.IsAny<int>() ) )
                .ReturnsAsync( teamResponse );

            // Act
            var result = (IStatusCodeActionResult) await m_teamController.Delete( teamId );

            // Assert
            Assert.Equal( 200, result.StatusCode );
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode404_WhenTeamDoestNotExist()
        {
            // Arrange
            int teamId = 1;

            m_teamServiceMock
                .Setup( x => x.Delete( It.IsAny<int>() ) )
                .ReturnsAsync( () => null );

            // Act
            var result = (IStatusCodeActionResult) await m_teamController.Delete( teamId );

            // Assert
            Assert.Equal( 404, result.StatusCode );
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            int teamId = 1;

            m_teamServiceMock
                .Setup( x => x.Delete( It.IsAny<int>() ) )
                .ReturnsAsync( () => throw new Exception( "This is an Exception." ) );

            // Act
            var result = (IStatusCodeActionResult) await m_teamController.Delete( teamId );

            // Assert
            Assert.Equal( 500, result.StatusCode );
        }
    }
}
