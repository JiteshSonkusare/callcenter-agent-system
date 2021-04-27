using CallCenter.Agent.Server.Application.Agent.Commands;
using CallCenter.Agent.Server.Application.Agent.Queries;
using CallCenter.Agent.Server.Common.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CallCenter.Agent.Server.Controllers
{
    public class AgentController : BaseController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IList<AgentDto>>> GetAll()
        {
            try
            {
                var result = await Mediator.Send(new GetAgentsListQuery());

                if (result == null)
                {
                    return NotFound("No agents found.");
                }

                return Ok(result);
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AgentDto>> Get(int id)
        {
            try
            {
                var result = await Mediator.Send(new GetAgentDetailQuery { Id = id });

                if (result == null)
                {
                    return NotFound($"No agent found with id {id}.");
                }

                return Ok(result);
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Produces("application/json")]
        public async Task<ActionResult> Upsert([FromBody] CreateOrUpdateAgentCommand command)
        {
            try
            {
                var id = await Mediator.Send(command);

                return Ok(id);
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await Mediator.Send(new DeleteAgentCommand { Id = id });

                return NoContent();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
