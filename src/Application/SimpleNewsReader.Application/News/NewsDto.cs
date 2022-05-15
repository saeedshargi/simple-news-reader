namespace SimpleNewsReader.Application.News;

public class NewsDto
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public long NewsId { get; set; }
    public string? Category { get; set; }
    public string? Description { get; set; }
    public string? Author { get; set; }
    public string? PublishDate { get; set; }
    public string? ImageUrl { get; set; }
    public string? CreatedDate { get; set; }
    public string? ModifiedDate { get; set; }
}