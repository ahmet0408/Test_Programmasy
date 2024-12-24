using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestProgrammasy.Models;

namespace TestProgrammasy.Data.Configuration
{
    public class TestConf : IEntityTypeConfiguration<Test>
    {
        public void Configure(EntityTypeBuilder<Test> builder) 
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name);
            builder.Property(p => p.Description);
            builder.Property(p => p.UpdatetAt);
            builder.Property(p => p.CreatedAt);
            builder.Property(p => p.Level);
            builder.Property(p => p.Type);
            builder.HasOne(p => p.User).WithMany(p => p.Tests).HasForeignKey(p => p.UserId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(p => p.Questions).WithOne(p => p.Test).HasForeignKey(p => p.TestId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
