using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHeroUnitTests.Repositories
{
    public class TeamRepositoryTests
    {
        private readonly DbContextOptions<SuperHeroDbContext> m_teamContextOptions;
        private readonly SuperHeroDbContext m_teamContext;
        private readonly TeamRepository m_teamRepository;

        public TeamRepositoryTests()
        {
            m_teamContextOptions = new DbContextOptionsBuilder<SuperHeroDbContext>()
                .UseInMemoryDatabase( databaseName: "TeamRepository" )
                .Options;

            m_teamContext = new( m_teamContextOptions );

            m_teamRepository = new( m_teamContext );
        }

        [Fact]
        public async void GetAll_ShouldReturnListOfTeams_WhenTeamsExists()
        {
            // Arrange
            await m_teamContext.Database.EnsureDeletedAsync();

            m_teamContext.Team.Add( new Team
            {
                TeamID = 1,
                TeamName = "Justice League"
            } );

            m_teamContext.Team.Add( new Team
            {
                TeamID = 2,
                TeamName = "Avengers"
            } );

            await m_teamContext.SaveChangesAsync();

            // Act
            var result = await m_teamRepository.GetAll();

            // Assert
            Assert.NotNull( result );
            Assert.IsType<List<Team>>( result );
            Assert.Equal( 2, result.Count );
        }

        [Fact]
        public async void GetAll_ShouldReturnEmptyListOfTeams_WhenNoTeamsExists()
        {
            // Arrange
            await m_teamContext.Database.EnsureDeletedAsync();

            // Act
            var result = await m_teamRepository.GetAll();

            // Assert
            Assert.NotNull( result );
            Assert.IsType<List<Team>>( result );
            Assert.Empty( result );
        }

        [Fact]
        public async void GetById_ShouldReturnTeam_WhenTeamExists()
        {
            // Arrange
            await m_teamContext.Database.EnsureDeletedAsync();

            int teamId = 1;

            m_teamContext.Team.Add( new Team
            {
                TeamID = teamId,
                TeamName = "Justice League"
            } );

            await m_teamContext.SaveChangesAsync();

            // Act
            var result = await m_teamRepository.GetById( teamId );

            // Assert
            Assert.NotNull( result );
            Assert.IsType<Team>( result );
            Assert.Equal(teamId, result.TeamID );
        }

        [Fact]
        public async void GetById_ShouldReturnNull_WhenTeamDoesNotExist()
        {
            // Arrange
            await m_teamContext.Database.EnsureDeletedAsync();

            // Act
            var result = await m_teamRepository.GetById( 1 );

            // Assert
            Assert.Null( result );
        }

        [Fact]
        public async void Create_ShouldAddNewIdToTeam_WhenSavingToDatabase()
        {
            // Arrange
            await m_teamContext.Database.EnsureDeletedAsync();

            int expectedNewId = 1;

            Team team = new Team()
            {
                TeamName = "Justice League"
            };

            // Act
            var result = await m_teamRepository.Create( team );

            // Assert
            Assert.NotNull( result );
            Assert.IsType<Team>( result );
            Assert.Equal(expectedNewId, result.TeamID );
        }

        [Fact]
        public async void Create_ShouldFailToAddNewTeam_WhenTeamAlreadyExists()
        {
            // Arrange
            await m_teamContext.Database.EnsureDeletedAsync();

            Team team = new Team()
            {
                TeamID = 1,
                TeamName = "Justice League"
            };

            await m_teamRepository.Create( team );

            // Act
            async Task action() => await m_teamRepository.Create( team );

            // Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>( action );
            Assert.Contains( "An item with the same key has already been added", ex.Message );
        }

        [Fact]
        public async void Update_ShouldChangeValuesOnTeam_WhenTeamExists()
        {
            // Arrange
            await m_teamContext.Database.EnsureDeletedAsync();

            int teamId = 1;

            Team team = new Team()
            {
                TeamID = teamId,
                TeamName = "Justice League"
            };

            m_teamContext.Add( team );
            await m_teamContext.SaveChangesAsync();
            Team updateTeam = new Team()
            {
                TeamID = teamId,
                TeamName = "new Justice League"
            };

            // Act
            var result = await m_teamRepository.Update( teamId, updateTeam );

            // Assert
            Assert.NotNull( result );
            Assert.IsType<Team>( result );
            Assert.Equal(teamId, result.TeamID );
            Assert.Equal( updateTeam.TeamName, result.TeamName );
        }

        [Fact]
        public async void Update_ShouldReturnNull_WhenTeamDoesNotExist()
        {
            // Arrange
            await m_teamContext.Database.EnsureDeletedAsync();

            int teamId = 1;

            Team updateTeam = new Team()
            {
                TeamID = teamId,
                TeamName = "Justice League"
            };

            // Act
            var result = await m_teamRepository.Update( teamId, updateTeam );

            // Assert
            Assert.Null( result );
        }

        [Fact]
        public async void Delete_ShouldReturnDeletedTeam_WhenTeamIsDeleted()
        {
            // Arrange
            await m_teamContext.Database.EnsureDeletedAsync();

            int teamId = 1;

            Team newTeam = new Team()
            {
                TeamID = teamId,
                TeamName = "new Justice League"
            };

            m_teamContext.Team.Add( newTeam );
            await m_teamContext.SaveChangesAsync();

            // Act
            var result = await m_teamRepository.Delete( teamId );
            var team = await m_teamRepository.GetById( teamId );

            // Assert
            Assert.NotNull( result );
            Assert.IsType<Team>( result );
            Assert.Equal(teamId, result.TeamID );

            Assert.Null( team );
        }

        [Fact]
        public async void Delete_ShouldReturnNull_WhenTeamDoesNotExist()
        {
            // Arrange
            await m_teamContext.Database.EnsureDeletedAsync();

            // Act
            var result = await m_teamRepository.Delete( 1 );

            // Assert
            Assert.Null( result );
        }
    }
}
