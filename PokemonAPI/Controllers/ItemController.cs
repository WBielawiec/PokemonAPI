using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokemonAPI.Entities;
using PokemonAPI.Models;
using PokemonAPI.Services;
using System.Threading.Tasks;

namespace PokemonAPI.Controllers
{
    [Route("api/items")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        public readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet]
        public ActionResult GetAllItems([FromQuery] PokemonQuery pokemonQuery)
        {
            var items = _itemService.GetAllItems(pokemonQuery);

             return  Ok(items);
        }

        [HttpGet("{itemId}")]
        public ActionResult GetItem([FromRoute] int itemId)
        {
            var item = _itemService.GetItem(itemId);
            return Ok(item);
        }

        [HttpDelete("{itemId}")]
        [Authorize(Roles = "Admin")]
        public ActionResult RemoveItem([FromRoute] int itemId)
        {
            _itemService.RemoveItem(itemId);

            return NoContent();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult CreateItem([FromBody] ItemDto item)
        {
            var id = _itemService.CreateItem(item);

            return Created($"/api/items/{id}", null);
        }

        [HttpPut("{itemId}")]
        [Authorize(Roles = "Admin")]
        public ActionResult UpdateItem([FromRoute]int itemId, [FromBody] ItemDto item)
        {
            _itemService.UpdateItem(itemId, item);

            return Ok();
        }
    }
}
