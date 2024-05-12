using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectManagment.DAL.Entities;

namespace ProjectManagment.DAL.Context.Configurations;

public class CompanyConfiguration : BaseEntityConfiguration<Company>
{
    public override void Configure(EntityTypeBuilder<Company> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(100);
    }
}
