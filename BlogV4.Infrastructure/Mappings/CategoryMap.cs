using BlogV4.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogV4.Data.Mappings
{
    public class CategoryMap : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            // Tabela
            builder.ToTable("Category");

            // Chave Primária
            builder.HasKey(x => x.Id);

            builder
              .Property(x => x.CreatedAt)
              .HasDefaultValueSql("GETDATE()")
              .Metadata
              .SetAfterSaveBehavior(Microsoft.EntityFrameworkCore.Metadata
              .PropertySaveBehavior.Ignore);

            builder
              .Property(x => x.UpdatedAt)
              .HasDefaultValueSql("GETDATE()")
              .Metadata
              .SetAfterSaveBehavior(Microsoft.EntityFrameworkCore.Metadata
              .PropertySaveBehavior.Ignore);


            // Propriedades
            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnName("Name")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(80);

            builder.Property(x => x.Slug)
                .IsRequired()
                .HasColumnName("Slug")
                .HasColumnType("VARCHAR")
                .HasMaxLength(80);

            // Índices
            builder
                .HasIndex(x => x.Slug, "IX_Category_Slug")
                .IsUnique();
        }
    }
}