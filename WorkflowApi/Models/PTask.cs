using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkflowApi.Models
{
    public class PTask
    {
        [Key]
        public int Id { get; set; }
        
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime? EndDate { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }

        //[ForeignKey("Team")]
        //public int TeamId { get; set; }
        //public virtual Team Team { get; set; }

        [ForeignKey("Priority")]
        public int PriorityId { get; set; }
        public virtual Priority Priority { get; set; }

        [ForeignKey("State")]
        public int StateId { get; set; }
        public virtual State State { get; set; }

        //[InverseProperty("PTaskStart")]
        public ICollection<PTaskDependencies> PTaskDependenciesStart { get; set; }
        //[InverseProperty("PTaskEnd")]
        public ICollection<PTaskDependencies> PTaskDependenciesEnd { get; set; }
        public ICollection<TeamPTask> TeamPTask { get; set; }


    }
}
