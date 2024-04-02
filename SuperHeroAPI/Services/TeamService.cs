using SuperHeroAPI.Helpers;

namespace SuperHeroAPI.Services
{
    public interface ITeamService
    {
        Task<List<TeamResponse>> GetAll();
        Task<TeamResponse> GetById( int id );
        Task<TeamResponse> Create( TeamRequest newTeam );
        Task<TeamResponse> Update( int teamId, TeamRequest updateTeam );
        Task<TeamResponse> Delete( int teamId );
    }
    public class TeamService : ITeamService
    {
        private readonly ITeamRepository m_teamRepository;
        private readonly ISuperHeroRepository m_superHeroRepository;

        public TeamService( ITeamRepository teamRepository, ISuperHeroRepository superHeroRepository )
        {
            m_teamRepository = teamRepository;
            m_superHeroRepository = superHeroRepository;
        }

        public async Task<TeamResponse> Create( TeamRequest newTeam )
        {
            Team team = Map.TeamRequestToTeam( newTeam );

            Team insertedTeam = await m_teamRepository.Create( team );

            if (insertedTeam != null)
            {
                return Map.TeamToTeamResponse( insertedTeam );
            }

            return null;
        }

        public async Task<TeamResponse> Delete( int teamId )
        {
            Team team = await m_teamRepository.GetById( teamId );

            List<SuperHero> deletedSuperHeroes = new();

            if (team != null)
            {
                foreach (SuperHero superHero in team.SuperHeroes!)
                {
                    SuperHero deletedSuperHero = await m_superHeroRepository.Delete( superHero.SuperHeroID );

                    if (deletedSuperHero != null)
                    {
                        deletedSuperHeroes.Add( deletedSuperHero );
                    }
                }
            }

            Team deletedTeam = await m_teamRepository.Delete( teamId );

            if (deletedTeam != null)
            {
                return Map.TeamToTeamResponse( deletedTeam );
            }

            return null;
        }

        public async Task<List<TeamResponse>> GetAll()
        {
            List<Team> teams = await m_teamRepository.GetAll();

            if (teams != null)
            {
                return teams.Select( team => Map.TeamToTeamResponse( team ) ).ToList();
            }

            return null;
        }

        public async Task<TeamResponse> GetById( int id )
        {
            Team team = await m_teamRepository.GetById( id );

            if (team != null)
            {
                return Map.TeamToTeamResponse( team );
            }

            return null;
        }

        public async Task<TeamResponse> Update( int teamId, TeamRequest updateTeam )
        {
            Team team = Map.TeamRequestToTeam( updateTeam );

            foreach(TeamSuperHeroRequest superHeroRequest in updateTeam.SuperHeroes)
            {
                await m_superHeroRepository.Update( superHeroRequest.Id, Map.TeamSuperHeroRequestToSuperHero( superHeroRequest ) );
            }

            Team updatedTeam = await m_teamRepository.Update( teamId, team );

            if (updatedTeam != null)
            {
                return Map.TeamToTeamResponse( updatedTeam );
            }

            return null;
        }
    }
}
