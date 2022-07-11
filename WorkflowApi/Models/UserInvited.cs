namespace WorkflowApi.Models
{
    public class UserInvited
    {
        public AppUser SourceUser { get; set; }
        public int SourceUserId { get; set; }

        public AppUser InvitedUser { get; set; }
        public int InvitedUserId { get; set; }
    }
}
