using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkflowApi.Models
{
    public class PTaskDependencies
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        //[ForeignKey("PTaskStart")]
        public int PTaskOneId { get; set; }

        //[ForeignKey("PTaskEnd")]
        public int PTaskTwoId { get; set; }


        public virtual PTask PTaskStart { get; set; }
        public virtual PTask PTaskEnd { get; set; }
    }
}
