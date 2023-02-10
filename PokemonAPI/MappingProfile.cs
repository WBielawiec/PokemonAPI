
using AutoMapper;
using PokemonAPI.Entities;
using PokemonAPI.Models;
using System.Linq;

namespace PokemonAPI
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Item, ItemDto>();
/*                .ForMember(m => m.Id, c => c.MapFrom(s => s.Id))
                .ForMember(m => m.Name, c => c.MapFrom(s => s.Name))
                .ForMember(m => m.Description, c => c.MapFrom(s => s.Description))
                .ForMember(m => m.Type, c => c.MapFrom(s => s.Type));*/

            CreateMap<Move, MoveDto>();
            /*  .ForMember(m => m.Id, c => c.MapFrom(s => s.Id))
                .ForMember(m => m.Accuracy, c => c.MapFrom(s => s.Accuracy))
                .ForMember(m => m.Category, c => c.MapFrom(s => s.Category))
                .ForMember(m => m.Description, c => c.MapFrom(s => s.Description))
                .ForMember(m => m.Name, c => c.MapFrom(s => s.Name))
                .ForMember(m => m.PP, c => c.MapFrom(s => s.PP))
                .ForMember(m => m.Type, c => c.MapFrom(s => s.Type))
                .ForMember(m => m.TM, c => c.MapFrom(s => s.TM));*/

            CreateMap<Pokemon, PokemonDto>()
                .ForMember(m => m.HP, c => c.MapFrom(s => s.Base.HP))
                .ForMember(m => m.Attack, c => c.MapFrom(s => s.Base.Attack))
                .ForMember(m => m.Defense, c => c.MapFrom(s => s.Base.Defense))
                .ForMember(m => m.SpAttack, c => c.MapFrom(s => s.Base.SpAttack))
                .ForMember(m => m.SpDefense, c => c.MapFrom(s => s.Base.SpDefense))
                .ForMember(m => m.Speed, c => c.MapFrom(s => s.Base.Speed));
                //.ForMember(m => m.Types, c => c.MapFrom(s => s.Type.Select(x => x.Type.Name)));


            CreateMap<ItemDto, Item>();

            CreateMap<string, Type>()
                .ForMember(t => t.Name, t => t.MapFrom(s => s.ToString()));
        }
    }
}
