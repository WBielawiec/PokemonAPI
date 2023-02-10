using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokemonAPI.Models;
using PokemonAPI.Services;

namespace PokemonAPI.Controllers
{
    [Route("api/pokemons")]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        private readonly IPokemonService _pokemonService;

        public PokemonController(IPokemonService pokemonService)
        {
            _pokemonService = pokemonService;
        }

        [HttpGet]
        public ActionResult GetAllPokemons([FromQuery] PokemonQuery pokemonQuery)
        {
            var pokemons = _pokemonService.GetAllPokemons(pokemonQuery);

            return Ok(pokemons);
        }

        [HttpGet("{pokemonId}")]
        public ActionResult GetPokemon([FromRoute] int pokemonId)
        {
            var pokemon = _pokemonService.GetPokemon(pokemonId);

            return Ok(pokemon);
        }

        [HttpDelete("{pokemonId}")]
        [Authorize(Roles = "Admin")]
        public ActionResult RemovePokemon([FromRoute] int pokemonId)
        {
            _pokemonService.RemovePokemon(pokemonId);

            return NoContent();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult CreatePokemon([FromBody] PokemonDto pokemon)
        {
            var id = _pokemonService.CreatePokemon(pokemon);

            return Created($"/api/items/{id}", null);
        }

        [HttpPut("pokemonId")]
        [Authorize(Roles = "Admin")]
        public ActionResult UpdatePokemon([FromRoute] int pokemonId, [FromBody] PokemonDto pokemon)
        {
            _pokemonService.UpdatePokemon(pokemonId, pokemon);

            return Ok();
        }
    }
}
