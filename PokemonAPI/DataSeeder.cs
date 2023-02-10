using AutoMapper;
using Newtonsoft.Json;
using PokemonAPI.Deserializers;
using PokemonAPI.Entities;
using PokemonAPI.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PokemonAPI
{
    public class DataSeeder
    {
        private readonly PokeDbContext _dbContext;
        private readonly IMapper _mapper;

        public DataSeeder(PokeDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.Roles.Any())
                {
                    var roles = GetRoles();
                    _dbContext.Roles.AddRange(roles);
                    _dbContext.SaveChanges();
                }
                if (!_dbContext.Types.Any())
                {
                    var types = GetTypes();
                    _dbContext.Types.AddRange(types);
                    _dbContext.SaveChanges();
                }
                if (!_dbContext.Moves.Any())
                {
                    var moves = GetMoves();
                    _dbContext.Moves.AddRange(moves);
                    _dbContext.SaveChanges();
                }
                if (!_dbContext.Items.Any())
                {
                    var items = GetItems();
                    _dbContext.Items.AddRange(items);
                    _dbContext.SaveChanges();
                }
                if (!_dbContext.Pokemons.Any())
                {
                    var pokemons = GetPokemons();
                    _dbContext.Pokemons.AddRange(pokemons);
                    _dbContext.SaveChanges();
                }
            }
        }

        private IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>()
            {
                new Role()
                {
                    Name = "User"
                },
                new Role()
                {
                    Name="Admin"
                },
            };

            return roles;
        }

        private IEnumerable<Entities.Type> GetTypes()
        {
            var types = new List<Entities.Type>()
            {
                new Entities.Type()
                {
                    Name = "Normal"
                },
                new Entities.Type()
                {
                    Name = "Fire"
                },
                new Entities.Type()
                {
                    Name = "Water"
                },
                new Entities.Type()
                {
                    Name = "Grass"
                },
                new Entities.Type()
                {
                    Name = "Flying"
                },
                new Entities.Type()
                {
                    Name = "Poison"
                },
                new Entities.Type()
                {
                    Name = "Electric"
                },
                new Entities.Type()
                {
                    Name = "Ground"
                },
                new Entities.Type()
                {
                    Name = "Rock"
                },
                new Entities.Type()
                {
                    Name = "Psychic"
                },
                new Entities.Type()
                {
                    Name = "Ice"
                },
                new Entities.Type()
                {
                    Name = "Bug"
                },
                new Entities.Type()
                {
                    Name = "Ghost"
                },
                new Entities.Type()
                {
                    Name = "Steel"
                },
                new Entities.Type()
                {
                    Name = "Dragon"
                },
                new Entities.Type()
                {
                    Name = "Dark"
                },
                new Entities.Type()
                {
                    Name = "Fairy"
                },
                new Entities.Type()
                {
                    Name = "Fighting"
                },
            };

            return types;
        }

        private IEnumerable<Item> GetItems()
        {
            string startupPath = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).FullName);
            string json = File.ReadAllText(startupPath + "\\data\\items.json");
            var items = JsonConvert.DeserializeObject<List<Item>>(json);

            return items;
        }

        private IEnumerable<Move> GetMoves()
        {
            string startupPath = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).FullName);
            string json = File.ReadAllText(startupPath + "\\data\\moves.json");
            var moves = JsonConvert.DeserializeObject<List<Move>>(json);

            return moves;
        }
        private IEnumerable<Pokemon> GetPokemons()
        {
            string startupPath = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).FullName);
            string json = File.ReadAllText(startupPath + "\\data\\pokemons.json");
            var pokemons = JsonConvert.DeserializeObject<List<PokemonDeserializer>>(json);

            var pokemonList = new List<Pokemon>();

            foreach (var pokemon in pokemons)
            {
                ICollection<Entities.Type> typeList = new List<Entities.Type>();

                Pokemon pokeObject = new Pokemon()
                {
                    Id = pokemon.id,
                    Name = pokemon.name.english,
                    Base = new Entities.Base()
                    {
                        Id = pokemon.id,
                        HP = pokemon.@base.HP,
                        Attack = pokemon.@base.Attack,
                        Defense = pokemon.@base.Defense,
                        SpAttack = pokemon.@base.SpAttack,
                        SpDefense = pokemon.@base.SpDefense,
                        Speed = pokemon.@base.Speed
                    },
                    Description = pokemon.description,
                };

                foreach (string pokemonType in pokemon.type)
                {
                    var type = _mapper.Map<Entities.Type>(pokemonType);
                    var dbType = _dbContext.Types.FirstOrDefault(t => t.Name == pokemonType);
                    /*typeList.Add(type);*/

                    if ( dbType is null )
                    {
                        throw new NotFoundException($"There is no type like: {type.Name}");
                    } 
                    else
                    {
                        _dbContext.PokemonTypes.Add(new PokemonType
                        {
                            PokemonId = pokeObject.Id,
                            TypeId = dbType.Id,
                            Type = dbType,
                        });
                    }
                }
                pokemonList.Add(pokeObject);
            }

            return pokemonList;
        }
    }
}