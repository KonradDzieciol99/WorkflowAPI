using WorkflowApi.Models;

namespace WorkflowApi.Services
{
    public interface IJwtTokenService
    {
        Task<Tuple<string, DateTime>> GenerateJwt(AppUser User);
    }
}
