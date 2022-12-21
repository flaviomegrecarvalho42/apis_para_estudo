using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParaEstudoApi.Models;

namespace ParaEstudoApi.DAL.Configurations
{
    public class DownloadFileConfiguration : IEntityTypeConfiguration<DownloadFile>
    {
        public void Configure(EntityTypeBuilder<DownloadFile> builder)
        {
            builder
                .ToTable("DownloadFile")
                .HasKey(d => d.Id);

            builder
                .Property(d => d.Name)
                .HasMaxLength(300)
                .IsRequired();

            builder
                .Property(d => d.CreatedAt)
                .IsRequired();

            builder
                .Property(d => d.GeneratedAt)
                .IsRequired();

            builder.HasIndex(d => d.CreatedAt);

            builder.HasIndex(d => d.GeneratedAt);
        }
    }
}
