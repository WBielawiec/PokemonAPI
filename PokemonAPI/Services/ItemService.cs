using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PokemonAPI.Entities;
using PokemonAPI.Exceptions;
using PokemonAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace PokemonAPI.Services
{

    public interface IItemService
    {
        IEnumerable<ItemDto> GetAllItems(PokemonQuery pokemonQuery);
        ItemDto GetItem(int ItemId);
        void RemoveItem(int ItemId);
        int CreateItem(ItemDto item);
        void UpdateItem(int itemId, ItemDto item);
    }

    public class ItemService : IItemService
    {
        private readonly IUserContextService _userContextService;
        public readonly PokeDbContext _dbContext;
        private readonly IMapper _mapper;

        public ItemService(IUserContextService userContextService, PokeDbContext dbContext, IMapper mapper)
        {
            _userContextService = userContextService;
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public IEnumerable<ItemDto> GetAllItems(PokemonQuery pokemonQuery) 
        {
            var items = _dbContext.Items
                .Skip(pokemonQuery.PageSize * (pokemonQuery.PageNumber - 1))
                .Take(pokemonQuery.PageSize)
                .ToList();

            if (items is null)
                throw new NotFoundException("No items in base.");

            var itemsDtos = _mapper.Map<IEnumerable<ItemDto>>(items);

            return itemsDtos;
        }

        public ItemDto GetItem(int itemId)
        {
            var item = _dbContext.Items.FirstOrDefault(i => i.Id == itemId);

            if(item is null)
                throw new NotFoundException("Item not found");

            var itemDto = _mapper.Map<ItemDto>(item);

            return itemDto;
        }

        public void RemoveItem(int itemId)
        {
            var item = _dbContext.Items.FirstOrDefault(i => i.Id == itemId);

            if (item is null)
                throw new NotFoundException("Item not found");

            _dbContext.Items.Remove(item);
            _dbContext.SaveChanges();
        }

        public int CreateItem(ItemDto item)
        {
            if (!(_dbContext.Items.FirstOrDefault(i => i.Id == item.Id) is null))
                throw new BadRequestException("Item ID already exist.");

            var itemToAdd = _mapper.Map<Item>(item);

            _dbContext.Items.Add(itemToAdd);
            _dbContext.SaveChanges();

            return itemToAdd.Id;
        }

        public void UpdateItem(int itemId, ItemDto item)
        {
            var itemToChange = _dbContext.Items.FirstOrDefault(i => i.Id == itemId);

            if (itemToChange is null)
                throw new NotFoundException("Item not found.");

            itemToChange.Name = item.Name;
            itemToChange.Description = item.Description;
            itemToChange.Type = item.Type;

            _dbContext.SaveChanges();

        }
    }
}
