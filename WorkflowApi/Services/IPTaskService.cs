using System.Security.Claims;
using WorkflowApi.Models;

namespace WorkflowApi.Services
{
    public interface IPTaskService
    {
        List<PTaskDto> GetAllPtaskByTeamId(int teamId,List<Claim> ClaimList);
    }
}
