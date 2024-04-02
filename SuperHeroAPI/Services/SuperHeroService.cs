using SuperHeroAPI.Helpers;

namespace SuperHeroAPI.Services
{
    public interface ISuperHeroService
    {
        Task<List<SuperHeroResponse>> GetAll();
        Task<SuperHeroResponse> GetById(int id);
        Task<SuperHeroResponse> Create( SuperHeroRequest newSuperHero );
        Task<SuperHeroResponse> Update( int superHeroId, SuperHeroRequest updateSuperHero );
        Task<SuperHeroResponse> Delete( int superHeroId );
    }

    public class SuperHeroService : ISuperHeroService
    {
        private readonly ISuperHeroRepository m_superHeroRepository;

        public SuperHeroService(ISuperHeroRepository superHeroRepository )
        {
            m_superHeroRepository = superHeroRepository;
        }

        public async Task<SuperHeroResponse> Create( SuperHeroRequest newSuperHero )
        {
            SuperHero superHero = Map.SuperHeroRequestToSuperHero( newSuperHero );

            SuperHero insertedSuperHero = await m_superHeroRepository.Create( superHero );

            if(insertedSuperHero != null)
            {
                return Map.SuperHeroToSuperHeroResponse( insertedSuperHero );
            }

            return null;
        }

        public async Task<SuperHeroResponse> Delete( int superHeroId )
        {
            SuperHero deletedSuperHero = await m_superHeroRepository.Delete( superHeroId );

            if(deletedSuperHero != null)
            {
                return Map.SuperHeroToSuperHeroResponse( deletedSuperHero );
            }

            return null;
        }

        public async Task<List<SuperHeroResponse>> GetAll()
        {
            List<SuperHero> superHeroes = await m_superHeroRepository.GetAll();

            if(superHeroes != null)
            {
                return superHeroes.Select( superHero => Map.SuperHeroToSuperHeroResponse(superHero)).ToList();
            }

            return null;
        }

        public async Task<SuperHeroResponse> GetById( int id )
        {
            SuperHero superHero = await m_superHeroRepository.GetById( id );

            if(superHero != null)
            {
                return Map.SuperHeroToSuperHeroResponse( superHero );
            }

            return null;
        }

        public async Task<SuperHeroResponse> Update( int superHeroId, SuperHeroRequest updateSuperHero )
        {
            SuperHero superHero = Map.SuperHeroRequestToSuperHero( updateSuperHero );

            SuperHero updatedSuperHero = await m_superHeroRepository.Update( superHeroId, superHero );

            if(updatedSuperHero != null)
            {
                return Map.SuperHeroToSuperHeroResponse( updatedSuperHero );
            }

            return null;
        }
    }
}
