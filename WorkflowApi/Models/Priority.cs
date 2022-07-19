using System.ComponentModel.DataAnnotations;

namespace WorkflowApi.Models
{
    public class Priority
    {
        public int Id { get; set; }
        public string Name{ get; set; }

        public ICollection<AppTask> AppTasks { get; set; }
    }
}
