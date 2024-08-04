using AutoMapper;
using DliibApi.Data;
using DliibApi.Dtos;
using DliibApi.Repositories;

namespace DliibApi.Services;

public class DliibService(DliibRepository dliibRepository, UserRepository userRepository, IMapper mapper)
{
    public async Task<IEnumerable<DliibDto>> GetAllDliibDtos(string? userName)
    {
        var dliibs = await dliibRepository.GetAllDliibs();
        var dliibDtos = mapper.Map<List<DliibDto>>(dliibs);
        if (userName == null)
        {
            return dliibDtos;
        }

        var user = await userRepository.GetUserByName(userName);
        foreach (var dliibDto in dliibDtos)
        {
            dliibDto.IsLiked = dliibs.Where(x => x.Id == dliibDto.Id).Any(x => x.Likes.Any(y => y.User == user));
            dliibDto.IsDisliked = dliibs.Where(x => x.Id == dliibDto.Id).Any(x => x.Dislikes.Any(y => y.User == user));
        }

        return dliibDtos;
    }

    public async Task<IEnumerable<DliibDto>> GetUserDliibDtos(string userName)
    {
        var userId = (await userRepository.GetUserByName(userName))?.Id;
        
        return await dliibRepository.GetUserDliibDtos(userId);
    }

    public async Task<DliibDto> GetDliibDto(int id, string? userName)
    {
        var dliib = await dliibRepository.GetDliib(id);
        if (dliib == null)
        {
            return null;
        }

        var dliibDto = mapper.Map<DliibDto>(dliib);

        if (userName == null)
        {
            return dliibDto;
        }

        var user = await userRepository.GetUserByName(userName);
        dliibDto.IsLiked = dliib.Likes.Any(x => x.User == user);
        dliibDto.IsDisliked = dliib.Dislikes.Any(x => x.User == user);

        return dliibDto;
    }

    public async Task<Dliib?> GetDliib(int id)
    {
        return await dliibRepository.GetDliib(id);
    }

    public async Task<DliibDto> CreateDliib(DliibDto dliibDto, string userName)
    {
        var user = await userRepository.GetUserByName(userName);
        if (user == null)
        {
            return null;
        }

        var dliib = mapper.Map<Dliib>(dliibDto);
        dliib.Author = user;
        
        await dliibRepository.Create(dliib);

        return mapper.Map<DliibDto>(dliib);
    }

    public async Task<DliibDto> UpdateDliib(DliibDto dliibDto)
    {
        return await dliibRepository.Update(dliibDto);
    }

    public async Task<bool> DeleteDliib(int id)
    {
        return await dliibRepository.Delete(id);
    }
}