using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestProgrammasy.Models;

namespace TestProgrammasy.Data.Configuration
{
    public class AnswerConf : IEntityTypeConfiguration<Answer>
    {
        public void Configure(EntityTypeBuilder<Answer> builder) {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.AnswerText).IsRequired();
            builder.Property(p => p.IsCorrect).IsRequired();
            builder.Property(p => p.Order);
            builder.HasOne(p => p.Question).WithMany(p => p.Answers).HasForeignKey(p => p.QuestionId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
