using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimpleNewsReader.Application.News;
using SimpleNewsReader.Domain.Exceptions;
using SimpleNewsReader.Infrastructure.Data;
using SimpleNewsReader.Infrastructure.PageScraping;

namespace SimpleNewsReader.Infrastructure.News;

public class NewsService: INewsService
{
    private readonly SimpleNewsReaderContext _context;
    private readonly IMapper _mapper;
    private readonly IPageScrapingService _pageScrapingService;

    public NewsService(SimpleNewsReaderContext context, IMapper mapper, IPageScrapingService pageScrapingService)
    {
        _context = context;
        _mapper = mapper;
        _pageScrapingService = pageScrapingService;
    }

    public async Task<IReadOnlyList<NewsDto>> AddNewsAsync(int pageNumber)
    {
        var newsInPage = await _pageScrapingService.GetNewsOfPageAsync(pageNumber);
        if (newsInPage == null)
            throw new NotFoundException("Could not find any news in this page");
        var newsInPageIds = newsInPage.Select(x => x.NewsId).ToList();
        var existNewsIds = await GetNewsByIdsAsync(newsInPageIds);
        var newAddedNews = newsInPage.Where(c => newsInPageIds.Except(existNewsIds.Select(x => x.NewsId)).Contains(c.NewsId));
        var newsToAdd = _mapper.Map<List<Domain.Entities.News>>(newAddedNews);
        await _context.Newses.AddRangeAsync(newsToAdd);
        await _context.SaveChangesAsync();
        return _mapper.Map<List<NewsDto>>(newsInPage);
    }

    public async Task<IReadOnlyList<Domain.Entities.News>> GetNewsByIdsAsync(IEnumerable<long> newsIds)
    {
        return await _context.Newses.Where(x => newsIds.Contains(x.NewsId)).Select(x => x).ToListAsync();
    }

    public async Task<IReadOnlyList<NewsDto>> GetNewsAsync()
    {
        var news = await _context.Newses.ToListAsync();
        return _mapper.Map<List<NewsDto>>(news);
    }
}