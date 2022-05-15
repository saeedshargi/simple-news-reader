using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleNewsReader.Domain.Entities;

namespace SimpleNewsReader.Infrastructure.Data.Config;

public class NewsConfiguration: IEntityTypeConfiguration<Domain.Entities.News>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.News> builder)
    {
        builder.ToTable("Newses");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Property(x => x.Title).IsRequired().HasMaxLength(256);
        builder.Property(x => x.Author).HasMaxLength(50);
        builder.Property(x => x.Category).HasMaxLength(50);
        builder.Property(x => x.Description).HasMaxLength(1024);
        builder.Property(x => x.ImageUrl).HasMaxLength(256);
        builder.Property(x => x.NewsId).IsRequired().IsUnicode();
        builder.HasIndex(x => x.NewsId);
    }
}