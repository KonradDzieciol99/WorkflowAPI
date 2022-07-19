using System.ComponentModel.DataAnnotations;

namespace WorkflowApi.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<TeamMember> TeamMembers { get; set; }
        public ICollection<AppTask> AppTasks { get; set; }

    }
}
