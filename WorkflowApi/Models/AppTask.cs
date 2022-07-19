using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkflowApi.Models
{
    public class AppTask
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.Now.AddDays(7);
        public string? Title { get; set; }
        public string? Description { get; set; }

        public int TeamId { get; set; }
        public virtual Team Team { get; set; }

        public int PriorityId { get; set; }
        public virtual Priority Priority { get; set; }

        public int StateId { get; set; }
        public virtual State State { get; set; }
    }
}
