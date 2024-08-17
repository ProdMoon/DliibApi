using AutoMapper;
using DliibApi.Data;
using DliibApi.Dtos;

namespace DliibApi.AutoMapperProfiles;

public class DliibProfile : Profile
{
    public DliibProfile()
    {
        CreateMap<Dliib, DliibDto>()
            .ForMember(dest => dest.Contents, opt => opt.MapFrom(src => src.Contents.OrderBy(x => x.Order).Select(x => x.Content)))
            .ForMember(dest => dest.Likes, opt => opt.MapFrom(src => src.Likes.Count()))
            .ForMember(dest => dest.Dislikes, opt => opt.MapFrom(src => src.Dislikes.Count()))
            .ForMember(dest => dest.AuthorNickName, opt => opt.MapFrom(src => src.Author != null ? src.Author.NickName : "익명"));

        CreateMap<DliibDto, Dliib>()
            .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Contents.FirstOrDefault()))
            .ForMember(dest => dest.Contents, opt => opt.MapFrom(src => src.Contents.Select((x, i) => new DliibContent { Content = x, Order = i })));
    }
}
