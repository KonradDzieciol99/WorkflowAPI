using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkflowApi.Models
{
    public class TeamMember
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId {get; set; }
        [ForeignKey("Team")]
        public int TeamId {get; set; }

        public User User { get; set; }
        public Team Team { get; set; }
    }
}
