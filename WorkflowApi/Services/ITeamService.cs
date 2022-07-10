using System.Security.Claims;
using WorkflowApi.DataTransferObject;

namespace WorkflowApi.Services
{
    public interface ITeamService
    {
        TeamDbo GetOne(int id, List<Claim> ClaimsList);
        TeamDbo CreateTeam(TeamDbo teamDbo, List<Claim> claimList);
        void AddMember(string email,int teamId);
        List<TeamDbo> GetAll(List<Claim> claimList);
    }
}
