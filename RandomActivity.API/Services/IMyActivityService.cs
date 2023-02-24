using RandomActivity.API.Models;

namespace RandomActivity.API.Services
{
    public interface IMyActivityService
    {
        Task<MyActivityDto?> GetNewActity(CancellationToken cancellationToken = default);
    }
}
