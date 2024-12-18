using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestProgrammasy.Models;

namespace TestProgrammasy.Data.Configuration
{
    public class QuestionConf: IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.QuestionText).IsRequired();
            builder.Property(p => p.QuestionType);
            builder.Property(p => p.Order);
            builder.Property(p => p.CreatedAt);
            builder.Property(p => p.CorrectAnswerIndex);
            builder.HasMany(p => p.Answers).WithOne(p => p.Question).HasForeignKey(p => p.QuestionId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(p => p.Test).WithMany(p => p.Questions).HasForeignKey(p => p.TestId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
