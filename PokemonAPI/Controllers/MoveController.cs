using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokemonAPI.Models;
using PokemonAPI.Services;

namespace PokemonAPI.Controllers
{
    [Route("api/moves")]
    [ApiController]
    public class MoveController : ControllerBase
    {
        public readonly IMoveService _moveService;

        public MoveController(IMoveService moveService)
        {
            _moveService = moveService;
        }

        [HttpGet]
        public ActionResult GetAllMoves([FromQuery] PokemonQuery pokemonQuery)
        {
            var moves = _moveService.GetAllMoves(pokemonQuery);

            return Ok(moves);
        }

        [HttpGet("{moveId}")]
        public ActionResult GetItem([FromRoute] int moveId)
        {
            var move = _moveService.GetMove(moveId);
            return Ok(move);
        }

        [HttpDelete("{moveId}")]
        [Authorize(Roles = "Admin")]
        public ActionResult RemoveMove([FromRoute] int moveId)
        {
            _moveService.RemoveMove(moveId);

            return NoContent();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult CreateMove([FromBody] MoveDto move)
        {
            var id = _moveService.CreateMove(move);

            return Created($"/api/items/{id}", null);
        }

        [HttpPut("{moveId}")]
        [Authorize(Roles = "Admin")]
        public ActionResult UpdateMove([FromRoute] int moveId, [FromBody] MoveDto move)
        {
            _moveService.UpdateMove(moveId, move);

            return Ok();
        }
    }
}
