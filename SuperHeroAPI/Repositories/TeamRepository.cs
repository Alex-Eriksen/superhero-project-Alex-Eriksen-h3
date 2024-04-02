namespace SuperHeroAPI.Repositories
{
    public interface ITeamRepository
    {
        Task<List<Team>> GetAll();
        Task<Team> GetById( int teamId );
        Task<Team> Create( Team newTeam );
        Task<Team> Update( int teamId, Team updateTeam );
        Task<Team> Delete( int teamId );
    }

    public class TeamRepository : ITeamRepository
    {
        private readonly SuperHeroDbContext m_context;

        public TeamRepository( SuperHeroDbContext context )
        {
            m_context = context;
        }

        public async Task<Team> Create( Team newTeam )
        {
            m_context.Team.Add( newTeam );
            await m_context.SaveChangesAsync();
            return newTeam;
        }

        public async Task<Team> Delete( int teamId )
        {
            Team team = await GetById( teamId );

            if (team != null)
            {
                m_context.Team.Remove( team );
                await m_context.SaveChangesAsync();
            }
            return team;
        }

        public async Task<List<Team>> GetAll()
        {
            return await m_context.Team.Include(e => e.SuperHeroes).ToListAsync();
        }

        public async Task<Team> GetById( int teamId )
        {
            return await m_context.Team.Include(e => e.SuperHeroes).FirstOrDefaultAsync( s => s.TeamID == teamId );
        }

        public async Task<Team> Update( int teamId, Team updateTeam )
        {
            Team team = await GetById( teamId );

            if (team != null)
            {
                team.TeamName = updateTeam.TeamName;

                await m_context.SaveChangesAsync();
            }

            return team;
        }
    }
}
