using DliibApi.Data;
using DliibApi.Repositories;

namespace DliibApi.Services;

public class UserService(UserRepository userRepository)
{
    public async Task<DliibUser?> GetUserById(string userId)
    {
        return await userRepository.GetUserById(userId);
    }

    public async Task<DliibUser?> GetUserByName(string userName)
    {
        return await userRepository.GetUserByName(userName);
    }
}