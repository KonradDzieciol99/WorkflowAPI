using System.Security.Claims;
using WorkflowApi.DataTransferObject;

namespace WorkflowApi.Services
{
    public interface IAppTaskService
    {
        List<AppTaskDto> GetAllPtaskByTeamId(int teamId,List<Claim> ClaimList);
        AppTaskDto CreatePTask(int teamId);
        void UpdatePTask(AppTaskUpdateDto pTaskDto);

    }
}
