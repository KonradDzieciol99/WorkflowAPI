using System.ComponentModel.DataAnnotations;

namespace WorkflowApi.Models
{
    public class AppRole
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<AppUser> Users { get; set; }
    }
}