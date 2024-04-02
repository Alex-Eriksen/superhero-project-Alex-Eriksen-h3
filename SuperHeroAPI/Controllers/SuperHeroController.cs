using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperHeroAPI.DTOs;

namespace SuperHeroAPI.Controllers
{
    [Route( "api/[controller]" )]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly ISuperHeroService m_superHeroService;
        public SuperHeroController(ISuperHeroService superHeroService )
        {
            m_superHeroService = superHeroService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<SuperHeroResponse> superHeroes = await m_superHeroService.GetAll();

                if(superHeroes == null)
                {
                    return Problem( "Nothing was returned from the service, this is unexpected." );
                }

                if(superHeroes.Count == 0)
                {
                    return NoContent();
                }

                return Ok( superHeroes );
            }
            catch (Exception ex)
            {
                return Problem( ex.Message );
            }
        }

        [HttpGet]
        [Route("{superHeroId}")]
        public async Task<IActionResult> GetById( int superHeroId )
        {
            try
            {
                SuperHeroResponse superHeroResponse = await m_superHeroService.GetById( superHeroId );

                if (superHeroResponse == null)
                {
                    return NotFound();
                }

                return Ok( superHeroResponse );
            }
            catch (Exception ex)
            {
                return Problem( ex.Message );
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create( SuperHeroRequest superHeroRequest )
        {
            try
            {
                SuperHeroResponse superHeroResponse = await m_superHeroService.Create( superHeroRequest );

                if (superHeroResponse == null)
                {
                    return Problem( "SuperHero was not created, something went wrong..." );
                }

                return Ok( superHeroResponse );
            }
            catch (Exception ex)
            {
                return Problem( ex.Message );
            }
        }

        [HttpPut]
        [Route("{superHeroId}")]
        public async Task<IActionResult> Update( int superHeroId, SuperHeroRequest superHeroRequest )
        {
            try
            {
                SuperHeroResponse superHeroResponse = await m_superHeroService.Update( superHeroId, superHeroRequest );

                if (superHeroResponse == null)
                {
                    return NotFound();
                }

                return Ok( superHeroResponse );
            }
            catch (Exception ex)
            {
                return Problem( ex.Message );
            }
        }

        [HttpDelete]
        [Route("{superHeroId}")]
        public async Task<IActionResult> Delete( int superHeroId )
        {
            try
            {
                SuperHeroResponse superHeroResponse = await m_superHeroService.Delete( superHeroId );

                if (superHeroResponse == null)
                {
                    return NotFound();
                }

                return Ok( superHeroResponse );
            }
            catch (Exception ex)
            {
                return Problem( ex.Message );
            }
        }
    }
}
