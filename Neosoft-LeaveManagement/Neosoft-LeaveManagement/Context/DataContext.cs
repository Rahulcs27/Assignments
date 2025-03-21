// DataContext.cs
using Microsoft.EntityFrameworkCore;
using Neosoft_LeaveManagement.Constants;
using Neosoft_LeaveManagement.Models;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<LeaveRequest> LeaveRequests { get; set; }
    public DbSet<LeaveApproval> LeaveApprovals { get; set; }
    public DbSet<LeaveBalance> LeaveBalances { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Define Primary Keys
        modelBuilder.Entity<User>().HasKey(u => u.UserId);
        modelBuilder.Entity<LeaveRequest>().HasKey(lr => lr.LeaveRequestId);
        modelBuilder.Entity<LeaveApproval>().HasKey(la => la.ApprovalId);

        // User Relationships
        modelBuilder.Entity<User>()
            .HasMany(u => u.LeaveRequests)
            .WithOne(lr => lr.Employee)
            .HasForeignKey(lr => lr.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<User>()
            .HasMany(u => u.LeaveApprovals)
            .WithOne(la => la.Manager)
            .HasForeignKey(la => la.ManagerId)
            .OnDelete(DeleteBehavior.Restrict);

        // Leave Request - Approval Relationship
        modelBuilder.Entity<LeaveRequest>()
            .HasOne(lr => lr.LeaveApproval)
            .WithOne(la => la.LeaveRequest)
            .HasForeignKey<LeaveApproval>(la => la.LeaveRequestId)
            .OnDelete(DeleteBehavior.Cascade);

        // Seed Default Admin User
        modelBuilder.Entity<User>().HasData(new User
        {
            UserId = 1,
            Name = "Admin",
            Email = "admin@neosoftmail.com",
            Password = "admin123",
            Role = UserRole.Admin
        });
    }
}
