using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkflowApi.Models
{
    public class AppUser
    {
        [Key]
        public int Id { get; set; }
        [Required,EmailAddress]
        public string Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string PasswordHash { get; set; }

        [ForeignKey("Role")]
        public int RoleId { get; set; } = 1; 
        public virtual AppRole Role { get; set; }

        //public ICollection<PTask> PTasks { get; set; }
        public ICollection<TeamMember> TeamMember { get; set; }

        public ICollection<UserInvited> UsersWhoInvite { get; set; }
        public ICollection<UserInvited> InvitedByUsers { get; set; }
        public ICollection<Message> MessagesSent { get; set; }
        public ICollection<Message> MessagesReceived { get; set; }

    }
}