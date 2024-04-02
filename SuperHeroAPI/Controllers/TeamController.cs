using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SuperHeroAPI.Controllers
{
    [Route( "api/[controller]" )]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService m_teamService;

        public TeamController(ITeamService teamService)
        {
            m_teamService = teamService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<TeamResponse> teams = await m_teamService.GetAll();

                if (teams == null)
                {
                    return Problem( "Nothing was returned from the service, this is unexpected." );
                }

                if (teams.Count == 0)
                {
                    return NoContent();
                }

                return Ok( teams );
            }
            catch (Exception ex)
            {
                return Problem( ex.Message );
            }
        }

        [HttpGet]
        [Route( "{teamId}" )]
        public async Task<IActionResult> GetById( int teamId )
        {
            try
            {
                TeamResponse teamReponse = await m_teamService.GetById( teamId );

                if (teamReponse == null)
                {
                    return NotFound();
                }

                return Ok( teamReponse );
            }
            catch (Exception ex)
            {
                return Problem( ex.Message );
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create( TeamRequest teamRequest )
        {
            try
            {
                TeamResponse teamResponse = await m_teamService.Create( teamRequest );

                if (teamResponse == null)
                {
                    return Problem( "SuperHero was not created, something went wrong..." );
                }

                return Ok( teamResponse );
            }
            catch (Exception ex)
            {
                return Problem( ex.Message );
            }
        }

        [HttpPut]
        [Route( "{teamId}" )]
        public async Task<IActionResult> Update( int teamId, TeamRequest teamRequest )
        {
            try
            {
                TeamResponse teamResponse = await m_teamService.Update( teamId, teamRequest );

                if (teamResponse == null)
                {
                    return NotFound();
                }

                return Ok( teamResponse );
            }
            catch (Exception ex)
            {
                return Problem( ex.Message );
            }
        }

        [HttpDelete]
        [Route( "{teamId}/{newTeamId}" )]
        public async Task<IActionResult> Delete( int teamId )
        {
            try
            {
                TeamResponse teamResponse = await m_teamService.Delete( teamId );

                if (teamResponse == null)
                {
                    return NotFound();
                }

                return Ok( teamResponse );
            }
            catch (Exception ex)
            {
                return Problem( ex.Message );
            }
        }
    }
}
