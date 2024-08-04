using DliibApi.Repositories;

namespace DliibApi.Services;

public class DliibLikeService(DliibLikeRepository dliibLikeRepository, UserRepository userRepository)
{
    public async Task ToggleLike(int dliibId, string userName)
    {
        var userId = (await userRepository.GetUserByName(userName))?.Id;
        var likeId = await dliibLikeRepository.GetLikeId(dliibId, userId);
        if (likeId > 0)
        {
            await dliibLikeRepository.CancelLike(likeId.Value);
            return;
        }

        var dislikeId = await dliibLikeRepository.GetDislikeId(dliibId, userId);
        if (dislikeId > 0)
        {
            await dliibLikeRepository.CancelDislike(dislikeId.Value);
        }

        await dliibLikeRepository.Like(dliibId, userId);

        return;
    }

    public async Task ToggleDislike(int dliibId, string userName)
    {
        var userId = (await userRepository.GetUserByName(userName))?.Id;
        var dislikeId = await dliibLikeRepository.GetDislikeId(dliibId, userId);
        if (dislikeId > 0)
        {
            await dliibLikeRepository.CancelDislike(dislikeId.Value);
            return;
        }

        var likeId = await dliibLikeRepository.GetLikeId(dliibId, userId);
        if (likeId > 0)
        {
            await dliibLikeRepository.CancelLike(likeId.Value);
        }

        await dliibLikeRepository.Dislike(dliibId, userId);

        return;
    }
}