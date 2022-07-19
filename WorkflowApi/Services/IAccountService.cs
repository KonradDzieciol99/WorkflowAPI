using WorkflowApi.DataTransferObject;

namespace WorkflowApi.Services
{
    public interface IAccountService
    {
        Task RegisterUserAsync(RegisterUserDto dto);
        Task<Tuple<string, DateTime>> GenerateJwt(UserDto dto);
    }

}
