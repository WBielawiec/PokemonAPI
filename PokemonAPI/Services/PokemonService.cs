using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PokemonAPI.Entities;
using PokemonAPI.Exceptions;
using PokemonAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace PokemonAPI.Services
{

    public interface IPokemonService
    {
        IEnumerable<PokemonDto> GetAllPokemons(PokemonQuery pokemonQuery);
        PokemonDto GetPokemon(int pokemonId);
        void RemovePokemon(int pokemonId);
        int CreatePokemon(PokemonDto pokemon);
        void UpdatePokemon(int pokemonId, PokemonDto pokemon);
    }

    public class PokemonService : IPokemonService
    {
        private readonly IUserContextService _userContextService;
        public readonly PokeDbContext _dbContext;
        private readonly IMapper _mapper;

        public PokemonService(IUserContextService userContextService, PokeDbContext dbContext, IMapper mapper)
        {
            _userContextService = userContextService;
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public IEnumerable<PokemonDto> GetAllPokemons(PokemonQuery pokemonQuery)
        {
            var pokemons = _dbContext.
                Pokemons
                .Include(p => p.Base)
                .Include(p => p.Type)
                .Skip(pokemonQuery.PageSize * (pokemonQuery.PageNumber - 1))
                .Take(pokemonQuery.PageSize)
                .ToList();

            if (pokemons is null)
                throw new NotFoundException("No pokemons in base.");

            var pokemonDtos = _mapper.Map<List<PokemonDto>>(pokemons);

            return pokemonDtos;
        }

        public PokemonDto GetPokemon(int pokemonId)
        {
            var pokemon = _dbContext.Pokemons.FirstOrDefault(p => p.Id == pokemonId);

            if(pokemon is null)
                throw new NotFoundException("Pokemon not found");

            var pokemonDto = _mapper.Map<PokemonDto>(pokemon);

            return pokemonDto;
        }

        public void RemovePokemon(int pokemonId)
        {
            var pokemon = _dbContext.Pokemons.FirstOrDefault(p => p.Id == pokemonId);

            if (pokemon is null)
                throw new NotFoundException("Pokemon not found");

            _dbContext.Pokemons.Remove(pokemon);
            _dbContext.SaveChanges();
        }

        public int CreatePokemon(PokemonDto pokemon)
        {
            if(!(_dbContext.Pokemons.FirstOrDefault(p => p.Id == pokemon.Id) is null))
                throw new BadRequestException("Pokemon ID already exist.");

            var pokemonToAdd = _mapper.Map<Pokemon>(pokemon);

            _dbContext.Pokemons.Add(pokemonToAdd);
            _dbContext.SaveChanges();

            return pokemonToAdd.Id;
        }

        public void UpdatePokemon(int pokemonId, PokemonDto pokemon)
        {
            var pokemonToChange = _dbContext.Pokemons.FirstOrDefault(p => p.Id == pokemonId);

            if (pokemonToChange is null)
                throw new BadRequestException("Pokemon ID already exist.");

            var types = pokemon.Types;

            pokemonToChange.Name = pokemon.Name;
            pokemonToChange.Base.HP = pokemon.HP;
            pokemonToChange.Base.Attack = pokemon.Attack;
            pokemonToChange.Base.Defense = pokemon.Defense;
            pokemonToChange.Base.SpAttack = pokemon.SpAttack;
            pokemonToChange.Base.SpDefense = pokemon.SpDefense;
            pokemonToChange.Base.Speed = pokemon.Speed;
            //pokemonToChange.Type = pokemon.Types.

            _dbContext.SaveChanges();

        }
    }
}
