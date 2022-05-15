namespace SimpleNewsReader.Application.News;

public interface INewsService
{
    Task<IReadOnlyList<NewsDto>> AddNewsAsync(int pageNumber);
    public Task<IReadOnlyList<Domain.Entities.News>> GetNewsByIdsAsync(IEnumerable<long> newsIds);
    Task<IReadOnlyList<NewsDto>> GetNewsAsync();
}