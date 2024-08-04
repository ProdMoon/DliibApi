using DliibApi.Data;
using DliibApi.Dtos;
using DliibApi.Repositories;

namespace DliibApi.Services;

public class DliibService(DliibRepository dliibRepository, UserRepository userRepository)
{
    public async Task<IEnumerable<DliibDto>> GetAllDliibDtos()
    {
        return await dliibRepository.GetAllDliibDtos();
    }

    public async Task<IEnumerable<DliibDto>> GetUserDliibDtos(string userName)
    {
        var userId = (await userRepository.GetUserByName(userName))?.Id;
        
        return await dliibRepository.GetUserDliibDtos(userId);
    }

    public async Task<DliibDto> GetDliibDto(int id)
    {
        return await dliibRepository.GetDliibDto(id);
    }

    public async Task<Dliib?> GetDliib(int id)
    {
        return await dliibRepository.GetDliib(id);
    }

    public async Task<DliibDto> CreateDliib(DliibDto dliibDto)
    {
        return await dliibRepository.Create(dliibDto);
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