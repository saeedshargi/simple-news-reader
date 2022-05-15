using SimpleNewsReader.Application.News;

namespace SimpleNewsReader.Infrastructure.PageScraping;

public interface IPageScrapingService
{
    Task<IReadOnlyList<NewsDto>> GetNewsOfPageAsync(int pageNumber);
}