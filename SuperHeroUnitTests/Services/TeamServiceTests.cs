using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHeroUnitTests.Services
{
    public class TeamServiceTests
    {
        private readonly TeamService m_service;
        private readonly Mock<ISuperHeroRepository> m_superHeroRepositoryMock = new Mock<ISuperHeroRepository>();
        private readonly Mock<ITeamRepository> m_teamRepositoryMock = new Mock<ITeamRepository>();

        public TeamServiceTests()
        {
            m_service = new TeamService( m_teamRepositoryMock.Object, m_superHeroRepositoryMock.Object );
        }

        [Fact]
        public async void GetAll_ShouldReturnListOfTeamResponses_WhenTeamsExists()
        {
            // Arrange
            List<Team> teams = new List<Team>();

            teams.Add( new Team
            {
                TeamID = 1,
                TeamName = "Justice League"
            } );

            teams.Add( new Team
            {
                TeamID = 2,
                TeamName = "Avengers"
            } );

            m_teamRepositoryMock.Setup( x => x.GetAll() ).ReturnsAsync( teams );

            // Act
            var result = await m_service.GetAll();

            // Assert
            Assert.NotNull( result );
            Assert.Equal( 2, result.Count );
            Assert.IsType<List<TeamResponse>>( result );
        }

        [Fact]
        public async void GetAll_ShouldReturnEmptyListOfTeamResponses_WhenNoTeamsExists()
        {
            // Arrange
            List<Team> teams = new();

            m_teamRepositoryMock.Setup( x => x.GetAll() ).ReturnsAsync( teams );

            // Act
            var result = await m_service.GetAll();

            // Assert
            Assert.NotNull( result );
            Assert.Empty( result );
            Assert.IsType<List<TeamResponse>>( result );
        }

        [Fact]
        public async void GetById_ShouldReturnTeamResponse_WhenTeamExists()
        {
            // Arrange
            int teamId = 1;

            Team team = new Team()
            {
                TeamID = 1,
                TeamName = "Justice League"
            };

            m_teamRepositoryMock
                .Setup( x => x.GetById( It.IsAny<int>() ) )
                .ReturnsAsync( team );

            // Act
            var result = await m_service.GetById( teamId );

            // Assert
            Assert.NotNull( result );
            Assert.IsType<TeamResponse>( result );
            Assert.Equal( teamId, result.TeamID );
            Assert.Equal( team.TeamName, result.TeamName );
        }

        [Fact]
        public async void GetById_ShouldReturnNull_WhenTeamDoesNotExist()
        {
            // Arrange
            int teamId = 1;

            m_teamRepositoryMock
                .Setup( x => x.GetById( It.IsAny<int>() ) )
                .ReturnsAsync(() => null);

            // Act
            var result = await m_service.GetById( teamId );

            // Assert
            Assert.Null( result );
        }

        [Fact]
        public async void Create_ShouldReturnTeamResponse_WhenCreatedIsSuccess()
        {
            // Arrange
            TeamRequest newTeam = new TeamRequest
            {
                TeamName = "Justice League"
            };

            int teamId = 1;

            Team team = new Team
            {
                TeamID = teamId,
                TeamName = "Justice League"
            };

            m_teamRepositoryMock
                .Setup( x => x.Create( It.IsAny<Team>() ) )
                .ReturnsAsync( team );

            // Act
            var result = await m_service.Create( newTeam );

            // Assert
            Assert.NotNull( result );
            Assert.IsType<TeamResponse>( result );
            Assert.Equal( teamId, result.TeamID );
            Assert.Equal( team.TeamName, result.TeamName );
        }

        [Fact]
        public async void Create_ShouldReturnNull_WhenRepositoryReturnsNull()
        {
            // Arrange
            TeamRequest newTeam = new TeamRequest()
            {
                TeamName = "Justice League"
            };

            m_teamRepositoryMock
                .Setup( x => x.Create( It.IsAny<Team>() ) )
                .ReturnsAsync( () => null );

            // Act
            var result = await m_service.Create( newTeam );

            // Assert
            Assert.Null( result );
        }

        [Fact]
        public async void Update_ShouldReturnTeamResponse_WhenUpdateIsSuccess()
        {
            // Arrange
            TeamRequest teamRequest = new TeamRequest
            {
                TeamName = "Justice League"
            };

            int teamId = 1;

            Team team = new Team
            {
                TeamID = teamId,
                TeamName = "Justice League"
            };

            m_teamRepositoryMock
                .Setup( x => x.Update( It.IsAny<int>(), It.IsAny<Team>() ) )
                .ReturnsAsync( team );

            // Act
            var result = await m_service.Update( teamId, teamRequest );

            // Assert
            Assert.NotNull( result );
            Assert.IsType<TeamResponse>( result );
            Assert.Equal( team.TeamID, result.TeamID );
            Assert.Equal( teamRequest.TeamName, result.TeamName );
        }

        [Fact]
        public async void Update_ShouldReturnNull_WhenTeamDoesNotExist()
        {
            // Arrange
            TeamRequest teamRequest = new TeamRequest
            {
                TeamName = "Justice League"
            };

            int teamId = 1;

            m_teamRepositoryMock
                .Setup( x => x.Update( It.IsAny<int>(), It.IsAny<Team>() ) )
                .ReturnsAsync( () => null );

            // Act
            var result = await m_service.Update( teamId, teamRequest );

            // Assert
            Assert.Null( result );
        }

        [Fact]
        public async void Delete_ShouldReturnTeamResponse_WhenDeleteIsSuccess()
        {
            // Arrange
            int teamId = 1;

            Team team = new Team
            {
                TeamID = teamId,
                TeamName = "Justice League"
            };

            m_teamRepositoryMock
                .Setup( x => x.Delete( It.IsAny<int>() ) )
                .ReturnsAsync( team );

            // Act
            var result = await m_service.Delete( teamId );

            // Assert
            Assert.NotNull( result );
            Assert.IsType<TeamResponse>( result );
            Assert.Equal( teamId, result.TeamID );
        }

        [Fact]
        public async void Delete_ShouldReturnNull_WhenTeamDoesNotExist()
        {
            // Arrange
            int teamId = 1;

            m_teamRepositoryMock
                .Setup( x => x.Delete( It.IsAny<int>() ) )
                .ReturnsAsync( () => null );

            // Act
            var result = await m_service.Delete( teamId );

            // Assert
            Assert.Null( result );
        }
    }
}
