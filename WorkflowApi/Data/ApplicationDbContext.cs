using Microsoft.EntityFrameworkCore;
using WorkflowApi.Models;

namespace WorkflowApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        //public DbSet<ModelName> nazwajaką chcemy{get;set}
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<AppUser> Users { get; set; }
        public DbSet<AppRole> Roles { get; set; }
        public DbSet<Priority> Priorityies { get; set; }
        public DbSet<PTaskDependencies> PTaskDependencies { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<PTask> PTasks { get; set; }
        public DbSet<UserInvited> Invitations { get; set; }
        public DbSet<Message> Messages { get; set; }

        //Fluent API
        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<UserInvited>()
                .Property(u =>u.Confirmed).HasDefaultValue(false);

            builder.Entity<AppUser>()
                .HasIndex(u => u.Email)
                .IsUnique();

            builder.Entity<AppUser>()
                .Property(s => s.RoleId)
                .HasDefaultValue(1);

            builder.Entity<AppRole>().HasData(
                new {Id=1 ,Name = "User" },
                new { Id = 2 ,Name = "Moder" },
                new { Id = 3 ,Name = "Admin" });

            //builder.Entity<Role>().Property(s => 
            //s.Name).HasDefaultValue("User");

            builder.Entity<Priority>().HasData(
                new { Id = 1, Name = "Low" },
                new { Id = 2, Name = "Medium" },
                new { Id = 3, Name = "High" });

            builder.Entity<Priority>().Property(s =>
                    s.Name).HasDefaultValue("Low");

            builder.Entity<State>().HasData(
                new { Id = 1, Name = "ToDo" },
                new { Id = 2, Name = "In Progress" },
                new { Id = 3, Name = "Done" });

            builder.Entity<PTask>(e =>
            {
                e.Property(s => s.PriorityId).HasDefaultValue(1);
                e.Property(s => s.StateId).HasDefaultValue(1);
            });

            builder.Entity<PTaskDependencies>()
                .HasOne(m => m.PTaskStart)
                .WithMany(t => t.PTaskDependenciesStart)
                .HasForeignKey(m => m.PTaskOneId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.Entity<PTaskDependencies>()
                        .HasOne(m => m.PTaskEnd)
                        .WithMany(t => t.PTaskDependenciesEnd)
                        .HasForeignKey(m => m.PTaskTwoId)
                        .OnDelete(DeleteBehavior.Restrict);


            builder.Entity<UserInvited>()
    .HasKey(k => new { k.SourceUserId, k.InvitedUserId });

            builder.Entity<UserInvited>()
                .HasOne(s => s.SourceUser)
                .WithMany(l => l.UsersWhoInvite)
                .HasForeignKey(s => s.SourceUserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<UserInvited>()
                .HasOne(s => s.InvitedUser)
                .WithMany(l => l.InvitedByUsers)
                .HasForeignKey(s => s.InvitedUserId)
                .OnDelete(DeleteBehavior.NoAction);


            //new { BlogId = 1, PostId = 2, Title = "Second post", Content = "Test 2" });

        }


    }
}
