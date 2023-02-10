using Microsoft.AspNetCore.Mvc;
using PokemonAPI.Services;

namespace PokemonAPI.Controllers
{
    [Route("api/backpack")]
    [ApiController]
    public class BackpackController : ControllerBase
    {

        private readonly IBackpackService _backpackService;

        public BackpackController(IBackpackService backpackService)
        {
            _backpackService = backpackService;
        }

        [HttpPost("/items/{itemId}")]
        public ActionResult addItemToBackpack([FromRoute] int itemId)
        {
            _backpackService.AddItemToBackpack(itemId);

            return Ok();
        }

        [HttpGet("/items")]
        public ActionResult getAllItems()
        {
            var items = _backpackService.getAllItems();
            return Ok(items);
        }

        [HttpDelete("/items/{itemId}")]
        public ActionResult removeItemFromBackpack([FromRoute] int itemId)
        {
            _backpackService.RemoveItemFromBackpack(itemId);

            return NoContent();
        }


        [HttpPost("/moves/{moveId}")]
        public ActionResult addMoveToBackpack([FromRoute] int moveId)
        {
            _backpackService.AddMoveToBackpack(moveId);

            return Ok();
        }

        [HttpGet("/moves")]
        public ActionResult getAllMoves()
        {
            var moves = _backpackService.getAllMoves();
            return Ok(moves);
        }

        [HttpDelete("/moves/{moveId}")]
        public ActionResult removeMoveFromBackpack([FromRoute] int moveId)
        {
            _backpackService.RemoveMoveFromBackpack(moveId);

            return NoContent();
        }

        [HttpPost("/pokemons/{pokemonId}")]
        public ActionResult addPokemonToBackpack([FromRoute] int pokemonId)
        {
            _backpackService.AddPokemonToBackpack(pokemonId);

            return Ok();
        }

        [HttpGet("/pokemons")]
        public ActionResult getAllPokemons()
        {
            var pokemons = _backpackService.getAllPokemons();
            return Ok(pokemons);
        }

        [HttpDelete("/pokemons/{pokemonId}")]
        public ActionResult removePokemonFromBackpack([FromRoute] int pokemonId)
        {
            _backpackService.RemovePokemonFromBackpack(pokemonId);

            return NoContent();
        }
    }
}
