namespace SuperHeroAPI.Repositories
{
    public interface ISuperHeroRepository
    {
        Task<List<SuperHero>> GetAll();
        Task<SuperHero> GetById( int superHeroId );
        Task<SuperHero> Create( SuperHero newSuperHero );
        Task<SuperHero> Update( int superHeroId, SuperHero updateSuperHero );
        Task<SuperHero> Delete( int superHeroId );
    }

    public class SuperHeroRepository : ISuperHeroRepository
    {
        private readonly SuperHeroDbContext m_context;

        public SuperHeroRepository(SuperHeroDbContext context )
        {
            m_context = context;
        }

        public async Task<SuperHero> Create( SuperHero newSuperHero )
        {
            m_context.SuperHero.Add( newSuperHero );
            await m_context.SaveChangesAsync();
            return newSuperHero;
        }

        public async Task<SuperHero> Delete( int superHeroId )
        {
            SuperHero superHero = await GetById( superHeroId );
            if(superHero != null)
            {
                m_context.SuperHero.Remove( superHero );
                await m_context.SaveChangesAsync();
            }
            return superHero;
        }

        public async Task<List<SuperHero>> GetAll()
        {
            return await m_context.SuperHero.Include(e => e.Team).ToListAsync();
        }

        public async Task<SuperHero> GetById( int superHeroId )
        {
            return await m_context.SuperHero.Include(e => e.Team).FirstOrDefaultAsync( s => s.SuperHeroID == superHeroId );
        }

        public async Task<SuperHero> Update( int superHeroId, SuperHero updateSuperHero )
        {
            SuperHero superHero = await GetById(superHeroId);

            if(superHero != null)
            {
                superHero.Name = updateSuperHero.Name;
                superHero.FirstName = updateSuperHero.FirstName;
                superHero.LastName = updateSuperHero.LastName;
                superHero.Place = updateSuperHero.Place;
                superHero.Debut = updateSuperHero.Debut;
                superHero.TeamID = updateSuperHero.TeamID;

                await m_context.SaveChangesAsync();
            }

            return superHero;
        }
    }
}
