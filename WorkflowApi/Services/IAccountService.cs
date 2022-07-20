using WorkflowApi.DataTransferObject;
using WorkflowApi.Models;

namespace WorkflowApi.Services
{
    public interface IAccountService
    {
        Task<AppUser> RegisterUserAsync(RegisterUserDto dto);
    }

}
