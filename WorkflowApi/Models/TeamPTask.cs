using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkflowApi.Models
{
    public class TeamPTask
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("PTask")]
        public int PTaskId { get; set; }

        [ForeignKey("Team")]
        public int TeamId { get; set; }

        public PTask PTask { get; set; }
        public Team Team { get; set; }
    }
}
