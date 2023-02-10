using AutoMapper;
using PokemonAPI.Entities;
using PokemonAPI.Exceptions;
using PokemonAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace PokemonAPI.Services
{
    public interface IMoveService
    {
        IEnumerable<MoveDto> GetAllMoves(PokemonQuery pokemonQuery);
        MoveDto GetMove(int moveId);
        void RemoveMove(int moveId);
        int CreateMove(MoveDto move);
        void UpdateMove(int moveId, MoveDto move);
    }

    public class MoveService : IMoveService
    {
        private readonly IUserContextService _userContextService;
        public readonly PokeDbContext _dbContext;
        private readonly IMapper _mapper;

        public MoveService(IUserContextService userContextService, PokeDbContext dbContext, IMapper mapper)
        {
            _userContextService = userContextService;
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public IEnumerable<MoveDto> GetAllMoves(PokemonQuery pokemonQuery)
        {
            var moves = _dbContext.Moves
                .Skip(pokemonQuery.PageSize * (pokemonQuery.PageNumber - 1))
                .Take(pokemonQuery.PageSize)
                .ToList();

            if (moves is null)
                throw new NotFoundException("No moves in base.");

            var movesDtos = _mapper.Map<List<MoveDto>>(moves);

            return movesDtos;
        }

        public MoveDto GetMove(int moveId)
        {
            var move = _dbContext.Moves.FirstOrDefault(i => i.Id == moveId);

            if (move is null)
                throw new NotFoundException("Move not found");

            var moveDto = _mapper.Map<MoveDto>(move);

            return moveDto;
        }

        public void RemoveMove(int moveId)
        {
            var move = _dbContext.Moves.FirstOrDefault(i => i.Id == moveId);

            if (move is null)
                throw new NotFoundException("Move not found");

            _dbContext.Moves.Remove(move);
            _dbContext.SaveChanges();
        }

        public int CreateMove(MoveDto move)
        {
            if (!(_dbContext.Moves.FirstOrDefault(i => i.Id == move.Id) is null))
                throw new BadRequestException("Move ID already exist.");

            var moveToAdd = _mapper.Map<Move>(move);

            _dbContext.Moves.Add(moveToAdd);
            _dbContext.SaveChanges();

            return move.Id;
        }

        public void UpdateMove(int moveId, MoveDto move)
        {
            var moveToChange = _dbContext.Moves.FirstOrDefault(i => i.Id == moveId);

            if (moveToChange is null)
                throw new NotFoundException("Move not found.");

            moveToChange.Name = move.Name;
            moveToChange.Description = move.Description;
            moveToChange.Type = move.Type;

            _dbContext.SaveChanges();

        }
    }
}
