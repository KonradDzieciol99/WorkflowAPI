using System.ComponentModel.DataAnnotations;

namespace WorkflowApi.Models
{
    public class Team
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<TeamMember> TeamMember { get; set; }
        public ICollection<TeamPTask> TeamPTask { get; set; }

    }
}
