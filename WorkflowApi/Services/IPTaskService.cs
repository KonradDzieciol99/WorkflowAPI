using System.Security.Claims;
using WorkflowApi.DataTransferObject;

namespace WorkflowApi.Services
{
    public interface IPTaskService
    {
        List<PTaskDto> GetAllPtaskByTeamId(int teamId,List<Claim> ClaimList);
        PTaskDto CreatePTask(int teamId);
        void UpdatePTask(PTaskUpdateDto pTaskDto);

    }
}
