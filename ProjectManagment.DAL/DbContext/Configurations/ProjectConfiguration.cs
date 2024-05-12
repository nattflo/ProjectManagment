using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ProjectManagment.DAL.Entities;

namespace ProjectManagment.DAL.Context.Configurations;

public class ProjectConfiguration : BaseEntityConfiguration<Project>
{
    public override void Configure(EntityTypeBuilder<Project> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.StartTime)
            .IsRequired();

        builder.Property(e => e.EndTime)
            .IsRequired();

        builder.Property(e => e.Priority)
            .IsRequired();

        builder.HasOne<Employee>()
            .WithMany()
            .HasForeignKey(p => p.ProjectManagerId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        builder.HasMany<Employee>()
            .WithMany()
            .UsingEntity(j => j.ToTable("EmployeeProject"));

        builder.HasOne<Company>()
            .WithMany()
            .HasForeignKey(p => p.ClientId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        builder.HasOne<Company>()
            .WithMany()
            .HasForeignKey(p => p.ContractorId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();
    }
}