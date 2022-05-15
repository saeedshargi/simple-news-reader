using SimpleNewsReader.Domain.Common;

namespace SimpleNewsReader.Domain.Entities;

public class News: BaseEntity
{
    public string? Title { get; set; }
    public long NewsId { get; set; }
    public string? Category { get; set; }
    public string? Description { get; set; }
    public string? Author { get; set; }
    public string? PublishDate { get; set; }
    public string? ImageUrl { get; set; }
}