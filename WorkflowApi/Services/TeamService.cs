using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WorkflowApi.Data;
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
        public List<TeamDbo> GetAll(List<Claim> claimList)
        {
            int userId = int.Parse(claimList.Find(e => e.Type == ClaimTypes.NameIdentifier).Value);
            var belongingsTeams = dbContext.TeamMembers.Include(t => t.Team).Where(t => t.UserId == userId).ToList();
            List<TeamDbo> teamDboList = new List<TeamDbo>();

            foreach (var item in belongingsTeams)
            {
                TeamDbo teamDbo = new TeamDbo()
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
        public TeamDbo CreateTeam(TeamDbo teamDbo, List<Claim> claimList)
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

            TeamDbo teamDboRet = new TeamDbo()
            {
                Id = team.Id,
                Name = team.Name
            };

            return teamDboRet;
        }

        public TeamDbo GetOne(int id, List<Claim> claimList)
        {

            int userId = int.Parse(claimList.Find(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var teamMember = this.dbContext.TeamMembers.Include(t => t.Team).FirstOrDefault(x => (x.TeamId == id && x.UserId == userId));

            if (teamMember == null)
            {
                throw new BadRequestException("Podano błędne Id & Nie masz prawa do tego zasobu");
            }

            TeamDbo teamDbo = new TeamDbo()
            {
                Id = teamMember.TeamId,
                Name = teamMember.Team.Name
            };

            return teamDbo;
        }
    }
}
