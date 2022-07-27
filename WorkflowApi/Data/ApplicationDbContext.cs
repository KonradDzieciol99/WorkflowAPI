using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WorkflowApi.Models;

namespace WorkflowApi.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Priority> Priorityies { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<AppTask> AppTasks { get; set; }
        public DbSet<UserInvited> Invitations { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<SignalRConnection> SignalRConnections { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);//because of identity


            builder.Entity<SignalRConnection>(ss =>
            {
                ss.HasKey(s => s.UserName);
            });

            builder.Entity<Team>(ts =>
            {
                ts.HasKey(t => t.Id);
            });

            builder.Entity<Priority>(ps =>
            {
                ps.HasKey(p => p.Id);

                ps.Property(s =>s.Name).HasDefaultValue("Low");

                ps.HasData(
                new { Id = 1, Name = "Low" },
                new { Id = 2, Name = "Medium" },
                new { Id = 3, Name = "High" });
            });

            builder.Entity<State>(ss =>
            {
                ss.HasKey(s => s.Id);

                ss.HasData(
                new { Id = 1, Name = "ToDo" },
                new { Id = 2, Name = "In Progress" },
                new { Id = 3, Name = "Done" });
            });


            builder.Entity<AppTask>(ts =>
            {
                ts.HasKey(t => t.Id);

                ts.HasOne(t => t.Team)
                .WithMany(team => team.AppTasks)
                .HasForeignKey(t => t.TeamId);

                ts.HasOne(t => t.State)
                .WithMany(s => s.AppTasks)
                .HasForeignKey(t => t.StateId);

                ts.HasOne(t => t.Priority)
                .WithMany(p => p.AppTasks)
                .HasForeignKey(t => t.PriorityId);

                ts.Property(s => s.PriorityId).HasDefaultValue(1);
                ts.Property(s => s.StateId).HasDefaultValue(1);
                ts.Property(t => t.TeamId).IsRequired();
            });

            builder.Entity<TeamMember>()
                .HasKey(t => new { t.UserId, t.TeamId});

            builder.Entity<TeamMember>()
                .HasOne<Team>(t => t.Team)
                .WithMany(s => s.TeamMembers)
                .HasForeignKey(t => t.TeamId);

            builder.Entity<TeamMember>()
                .HasOne<AppUser>(u => u.User)
                .WithMany(t => t.TeamMembers)
                .HasForeignKey(u => u.UserId);

            builder.Entity<UserInvited>()
                .HasKey(k => new { k.SourceUserId, k.InvitedUserId });

            builder.Entity<UserInvited>()
                .Property(u => u.Confirmed).HasDefaultValue(false);

            builder.Entity<UserInvited>()
                .HasOne(s => s.SourceUser)
                .WithMany(l => l.UsersWhoInvite)
                .HasForeignKey(s => s.SourceUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<UserInvited>()
                .HasOne(s => s.InvitedUser)
                .WithMany(l => l.InvitedByUsers)
                .HasForeignKey(s => s.InvitedUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Message>()
                .HasKey(m => m.Id);

            builder.Entity<Message>()
                .HasOne(m => m.Sender)
                .WithMany(u => u.MessagesSent)
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Message>()
                .HasOne(m => m.Recipient)
                .WithMany(u => u.MessagesReceived)
                .HasForeignKey(m => m.RecipientId)
                .OnDelete(DeleteBehavior.Restrict);
            //new { BlogId = 1, PostId = 2, Title = "Second post", Content = "Test 2" });
        }
    }
}
