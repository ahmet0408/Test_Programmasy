using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestProgrammasy.Models;

namespace TestProgrammasy.Data.Configuration
{
    public class TestResultConf : IEntityTypeConfiguration<TestResult>
    {
        public void Configure(EntityTypeBuilder<TestResult> builder) {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.CompletedAt);
            builder.Property(p => p.Score);
            builder.Property(p => p.TotalPoints);
            builder.HasOne(p => p.User).WithMany(p => p.TestResults).HasForeignKey(p => p.Id).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(p => p.Test).WithMany(p => p.TestResults).HasForeignKey(p => p.TestId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
