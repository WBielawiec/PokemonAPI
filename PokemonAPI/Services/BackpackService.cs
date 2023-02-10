using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PokemonAPI.Entities;
using PokemonAPI.Exceptions;
using PokemonAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace PokemonAPI.Services
{
    public interface IBackpackService
    {
        IEnumerable<ItemDto> getAllItems();
        IEnumerable<MoveDto> getAllMoves();
        IEnumerable<PokemonDto> getAllPokemons();
        void AddItemToBackpack(int itemId);
        void AddMoveToBackpack(int moveId);
        void AddPokemonToBackpack(int pokemonId);
        void RemoveItemFromBackpack(int itemId);
        void RemoveMoveFromBackpack(int moveId);
        void RemovePokemonFromBackpack(int pokemonId);
    }


    public class BackpackService : IBackpackService
    {
        private readonly IUserContextService _userContextService;
        public readonly PokeDbContext _dbContext;
        private readonly IMapper _mapper;

        public BackpackService(PokeDbContext dbContext, IUserContextService userContextService, IMapper mapper)
        {
            _dbContext = dbContext;
            _userContextService = userContextService;
        }

        public IEnumerable<ItemDto> getAllItems()
        {
            var user = _userContextService.GetUserId;
            var backpack = _dbContext.Backpacks.FirstOrDefault(b => b.UserId == user);

            if (backpack is null)
                throw new NotFoundException("Nie powinno się nigdy pojawić ale jak już się pojawi to wiedz, że coś jest nie tak z plecakiem.");

            var backpackItem = _dbContext.BackpackItems.Where(bi => bi.BackpackId == backpack.Id);
            List<ItemDto> items = new List<ItemDto>();

            foreach(var count in backpackItem)
            {
                var item = _dbContext.Items.FirstOrDefault(i => i.Id == count.ItemId);

                ItemDto itemDto = new ItemDto()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    Type = item.Type
                };
                items.Add(itemDto);
            }
            return items;
        }

        public void AddItemToBackpack(int itemId)
        {
            var user = _userContextService.GetUserId;
            var backpack = _dbContext.Backpacks.FirstOrDefault(b => b.UserId == user);
            var item = _dbContext.Items.FirstOrDefault(i => i.Id == itemId);

            if (backpack is null)
                throw new NotFoundException("Nie powinno się nigdy pojawić ale jak już się pojawi to wiedz, że coś jest nie tak z plecakiem.");

            _dbContext.BackpackItems.Add(new BackpackItem
            {
                BackpackId = backpack.Id,
                ItemId = item.Id
            });

            try
            {
                _dbContext.SaveChanges();
            } catch(DbUpdateException e)
            {
                throw new NotFoundException("You cant add same item in backpack.");
            }
        }

        public void RemoveItemFromBackpack(int itemId)
        {
            var user = _userContextService.GetUserId;
            var backpack = _dbContext.Backpacks.FirstOrDefault(b => b.UserId == user);

            if (backpack is null)
                throw new NotFoundException("Nie powinno się nigdy pojawić ale jak już się pojawi to wiedz, że coś jest nie tak z plecakiem.");

            var item = _dbContext.BackpackItems.FirstOrDefault(i => i.BackpackId == backpack.Id && i.ItemId == itemId);   

            if (item is null)
                throw new NotFoundException("Item not found.");

            _dbContext.BackpackItems.Remove(item);
            _dbContext.SaveChanges();

        }

        public IEnumerable<MoveDto> getAllMoves()
        {
            var user = _userContextService.GetUserId;
            var backpack = _dbContext.Backpacks.FirstOrDefault(b => b.UserId == user);

            if (backpack is null)
                throw new NotFoundException("Nie powinno się nigdy pojawić ale jak już się pojawi to wiedz, że coś jest nie tak z plecakiem.");

            var backpackMove = _dbContext.BackpackMoves.Where(bi => bi.BackpackId == backpack.Id);
            List<MoveDto> moves = new List<MoveDto>();

            foreach (var count in backpackMove)
            {
                var move = _dbContext.Moves.FirstOrDefault(i => i.Id == count.MoveId);

                MoveDto moveDto = new MoveDto()
                {
                    Id = move.Id,
                    Accuracy = move.Accuracy,
                    Category = move.Category,
                    Description = move.Description,
                    Name = move.Name,
                    PP = move.PP,
                    Type = move.Type,
                    TM = move.TM
                };
                moves.Add(moveDto);
            }
            return moves;
        }

        public void AddMoveToBackpack(int moveId)
        {
            var user = _userContextService.GetUserId;
            var backpack = _dbContext.Backpacks.FirstOrDefault(b => b.UserId == user);
            var move = _dbContext.Moves.FirstOrDefault(i => i.Id == moveId);

            if (backpack is null)
                throw new NotFoundException("Nie powinno się nigdy pojawić ale jak już się pojawi to wiedz, że coś jest nie tak z plecakiem.");

            _dbContext.BackpackMoves.Add(new BackpackMove
            {
                BackpackId = backpack.Id,
                MoveId = move.Id
            });

            try
            {
                _dbContext.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                throw new NotFoundException("You cant add same move in backpack.");
            }
        }

        public void RemoveMoveFromBackpack(int moveId)
        {
            var user = _userContextService.GetUserId;
            var backpack = _dbContext.Backpacks.FirstOrDefault(b => b.UserId == user);

            if (backpack is null)
                throw new NotFoundException("Nie powinno się nigdy pojawić ale jak już się pojawi to wiedz, że coś jest nie tak z plecakiem.");

            var move = _dbContext.BackpackMoves.FirstOrDefault(i => i.BackpackId == backpack.Id && i.MoveId == moveId);

            if (move is null)
                throw new NotFoundException("Move not found.");

            _dbContext.BackpackMoves.Remove(move);
            _dbContext.SaveChanges();
        }

        public IEnumerable<PokemonDto> getAllPokemons()
        {
            var user = _userContextService.GetUserId;
            var backpack = _dbContext.Backpacks.FirstOrDefault(b => b.UserId == user);

            if (backpack is null)
                throw new NotFoundException("Nie powinno się nigdy pojawić ale jak już się pojawi to wiedz, że coś jest nie tak z plecakiem.");

            var backpackPokemon = _dbContext.BackpackPokemons.Where(bp => bp.BackpackId == backpack.Id);
            List<PokemonDto> pokemons = new List<PokemonDto>();
            List<string> types = new List<string>();

            foreach (var count in backpackPokemon)
            {
                var pokemon = _dbContext
                    .Pokemons
                    .Include(p => p.Base)
                    .Include(p => p.Type)
                    .Include("Type")
                    .FirstOrDefault(p => p.Id == count.PokemonId);

                types.Clear();
                foreach (var type in pokemon.Type)
                {
                    types.Add(_dbContext.Types.FirstOrDefault(t => t.Id == type.TypeId).Name);
                }


                PokemonDto pokemonDto = new PokemonDto()
                {
                    Id = pokemon.Id,
                    Name = pokemon.Name,
                    Description = pokemon.Description,
                    HP = pokemon.Base.HP,
                    Attack = pokemon.Base.Attack,
                    Defense = pokemon.Base.Defense,
                    SpAttack = pokemon.Base.SpAttack,
                    SpDefense = pokemon.Base.SpDefense,
                    Speed = pokemon.Base.Speed,
                    Types = types.ToArray()

                };
                pokemons.Add(pokemonDto);
            }

            return pokemons;
        }

        public void AddPokemonToBackpack(int pokemonId)
        {
            var user = _userContextService.GetUserId;
            var backpack = _dbContext.Backpacks.FirstOrDefault(b => b.UserId == user);
            var pokemon = _dbContext.Pokemons.FirstOrDefault(i => i.Id == pokemonId);

            if (backpack is null)
                throw new NotFoundException("Nie powinno się nigdy pojawić ale jak już się pojawi to wiedz, że coś jest nie tak z plecakiem.");

            _dbContext.BackpackPokemons.Add(new BackpackPokemon
            {
                BackpackId = backpack.Id,
                PokemonId = pokemon.Id
            });

            try
            {
                _dbContext.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                throw new NotFoundException("You cant duplicate pokemon in backpack.");
            }
        }

        public void RemovePokemonFromBackpack(int pokemonId)
        {
            var user = _userContextService.GetUserId;
            var backpack = _dbContext.Backpacks.FirstOrDefault(b => b.UserId == user);

            if (backpack is null)
                throw new NotFoundException("Nie powinno się nigdy pojawić ale jak już się pojawi to wiedz, że coś jest nie tak z plecakiem.");

            var pokemon = _dbContext.BackpackPokemons.FirstOrDefault(p => p.BackpackId == backpack.Id && p.PokemonId == pokemonId);

            if (pokemon is null)
                throw new NotFoundException("Move not found.");

            _dbContext.BackpackPokemons.Remove(pokemon);
            _dbContext.SaveChanges();
        }
    }
}
