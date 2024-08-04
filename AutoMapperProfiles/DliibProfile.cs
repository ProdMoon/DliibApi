using AutoMapper;
using DliibApi.Data;
using DliibApi.Dtos;

namespace DliibApi.AutoMapperProfiles;

public class DliibProfile : Profile
{
    public DliibProfile()
    {
        CreateMap<Dliib, DliibDto>()
            .ForMember(dest => dest.Likes, opt => opt.MapFrom(src => src.Likes.Count()))
            .ForMember(dest => dest.Dislikes, opt => opt.MapFrom(src => src.Dislikes.Count()))
            .ForMember(dest => dest.AuthorNickName, opt => opt.MapFrom(src => src.Author != null ? src.Author.NickName : "익명"));

        CreateMap<DliibDto, Dliib>();
    }
}
