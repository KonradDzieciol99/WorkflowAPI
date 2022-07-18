using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WorkflowApi.Data;
using WorkflowApi.DataTransferObject;
using WorkflowApi.Exceptions;
using WorkflowApi.Models;

namespace WorkflowApi.Services
{
    public class TeamService : ITeamService
    {
        private readonly ApplicationDbContext dbContext;

        public TeamService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public List<TeamDto> GetAll(List<Claim> claimList)
        {
            int userId = int.Parse(claimList.Find(e => e.Type == ClaimTypes.NameIdentifier).Value);
            var belongingsTeams = dbContext.TeamMembers.Include(t => t.Team).Where(t => t.UserId == userId).ToList();
            List<TeamDto> teamDboList = new List<TeamDto>();

            foreach (var item in belongingsTeams)
            {
                TeamDto teamDbo = new TeamDto()
                {
                    Id = item.Team.Id,
                    Name = item.Team.Name,
                };
                teamDboList.Add(teamDbo);
            }


            return teamDboList;
        }
        public void AddMember(string email, int teamId)
        {
            var user = this.dbContext.Users.FirstOrDefault(x => (x.Email == email));

            if (user == null)
            {
                throw new BadRequestException("Podano błędny Email użytkownika");
            }
            //todo// check if user belong team that id send
            TeamMember teamMember = new TeamMember()
            {
                UserId = user.Id,
                TeamId = teamId,
            };

            this.dbContext.TeamMembers.Add(teamMember);
            this.dbContext.SaveChanges();

        }
        //todo// remove user function
        public TeamDto CreateTeam(TeamDto teamDbo, List<Claim> claimList)
        {
            Team team = new Team()
            {
                Name = teamDbo.Name
            };
            this.dbContext.Teams.Add(team);
            this.dbContext.SaveChanges();//generuje id dla obiektu team

            int userId = int.Parse(claimList.Find(e => e.Type == ClaimTypes.NameIdentifier).Value);

            TeamMember teamMember = new TeamMember()
            {
                UserId = userId,
                TeamId = team.Id,
            };
            this.dbContext.TeamMembers.Add(teamMember);
            this.dbContext.SaveChanges();

            TeamDto teamDboRet = new TeamDto()
            {
                Id = team.Id,
                Name = team.Name
            };

            return teamDboRet;
        }

        public TeamDto GetOne(int id, List<Claim> claimList)
        {

            int userId = int.Parse(claimList.Find(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var teamMember = this.dbContext.TeamMembers.Include(t => t.Team).FirstOrDefault(x => (x.TeamId == id && x.UserId == userId));

            if (teamMember is null)
            {
                throw new BadRequestException("Podano błędne Id & Nie masz prawa do tego zasobu");
            }

            TeamDto teamDbo = new TeamDto()
            {
                Id = teamMember.TeamId,
                Name = teamMember.Team.Name
            };
            

            return teamDbo;
        }
    }
}
