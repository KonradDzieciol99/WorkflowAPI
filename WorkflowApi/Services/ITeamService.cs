using System.Security.Claims;
using WorkflowApi.DataTransferObject;

namespace WorkflowApi.Services
{
    public interface ITeamService
    {
        TeamDto GetOne(int id, List<Claim> ClaimsList);
        TeamDto CreateTeam(TeamDto teamDbo, List<Claim> claimList);
        void AddMember(string email,int teamId);
        List<TeamDto> GetAll(List<Claim> claimList);
    }
}
