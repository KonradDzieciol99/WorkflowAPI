using System.ComponentModel.DataAnnotations;

namespace WorkflowApi.Models
{
    public class Team
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<TeamMember> TeamMembers { get; set; }
        public ICollection<PTask> PTasks { get; set; }

    }
}
