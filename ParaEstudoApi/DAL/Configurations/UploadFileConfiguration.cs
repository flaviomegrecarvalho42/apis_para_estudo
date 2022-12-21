using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParaEstudoApi.Models;

namespace ParaEstudoApi.DAL.Configurations
{
    public class UploadFileConfiguration : IEntityTypeConfiguration<UploadFile>
    {
        public void Configure(EntityTypeBuilder<UploadFile> builder)
        {
            builder
               .ToTable("UploadFile")
               .HasKey(r => r.Id);

            builder
                .Property(r => r.CreatedAt)
                .IsRequired();

            builder
                .Property(r => r.GeneratedAt)
                .IsRequired();

            builder
                .Property(r => r.OriginName)
                .HasMaxLength(300)
                .IsRequired();

            builder
                .Property(r => r.Name)
                .HasMaxLength(300)
                .IsRequired();

            builder
                .Property(r => r.HashCode)
                .HasMaxLength(300)
                .IsRequired();

            builder.HasIndex(r => r.GeneratedAt);
            builder.HasIndex(r => r.HashCode);
        }
    }
}
