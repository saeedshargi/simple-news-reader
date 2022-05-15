namespace SimpleNewsReader.Application.News;

public interface INewsService
{
    Task<IReadOnlyList<NewsDto>> AddNewsAsync(int pageNumber);
    Task<IReadOnlyList<NewsDto>> GetNewsAsync();
}