using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using proggame.Entities;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace proggame.Data;

public class proggameDbContext : AbpDbContext<proggameDbContext>
{
    public DbSet<TaskFile> Tasks { get; set; }
    public DbSet<TestFile> Tests { get; set; }
    public DbSet<SolutionFile> Solution { get; set; }
    public proggameDbContext(DbContextOptions<proggameDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureAuditLogging();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureFeatureManagement();
        builder.ConfigureTenantManagement();

        /* Configure your own entities here */

        builder.Entity<TaskFile>(tf =>
        {
            tf.ToTable("AppTasks", "dbo");
            tf.ConfigureByConvention();
        });
        builder.Entity<TestFile>(tf =>
        {
            tf.ToTable("AppTests", "dbo");
            tf.ConfigureByConvention();
            tf.HasOne<TaskFile>()
                .WithMany()
                .HasForeignKey(x => x.TaskId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();
        });
        builder.Entity<SolutionFile>(tf =>
        {
            tf.ToTable("AppTests", "dbo");
            tf.ConfigureByConvention();
            tf.HasOne<TaskFile>()
                .WithMany()
                .HasForeignKey(x => x.TaskId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();
            tf.HasOne<IdentityUser<Guid>>()
                .WithMany()
                .HasForeignKey(x => x.CreatorId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();
        });
    }
}
