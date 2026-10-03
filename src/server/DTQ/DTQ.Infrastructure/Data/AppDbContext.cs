using DTQ.Domain;
using DTQ.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace DTQ.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<TaskItem> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskItem>(entity =>
            {
                entity.ToTable("tasks");
                entity.HasKey(t => t.Id);

                entity.Property(t => t.Id).HasColumnName("id").ValueGeneratedOnAdd();

                entity.Property(t => t.Name)
                    .HasColumnName("name")
                    .HasMaxLength(20)
                    .IsRequired();

                entity.Property(t => t.TaskType)
                    .HasColumnName("task_type")
                    .HasMaxLength(20)
                    .IsRequired();

                entity.Property(t => t.Status)
                    .HasColumnName("status")
                    .HasMaxLength(20)
                    .HasConversion<string>()
                    .IsRequired();

                entity.Property(t => t.Payload)
                    .HasColumnName("payload")
                    .HasColumnType("json");

                entity.Property(t => t.MaxRetries).HasColumnName("max_retries").IsRequired();

                entity.Property(t => t.RetriesCount)
                    .HasColumnName("retries_count")
                    .HasDefaultValue(0);

                entity.Property(t => t.FencingToken)
                    .HasColumnName("fencing_token")
                    .HasDefaultValue(0);

                entity.Property(t => t.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp without time zone")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(t => t.ErrorMessage)
                    .HasColumnName("error_message")
                    .HasMaxLength(30);
            });
        }
    }
}
