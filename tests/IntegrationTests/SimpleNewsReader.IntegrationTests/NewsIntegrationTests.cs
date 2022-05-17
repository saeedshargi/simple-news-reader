using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleNewsReader.Application.Mapper;
using SimpleNewsReader.Application.News;
using SimpleNewsReader.Domain.Common;
using SimpleNewsReader.Infrastructure.Data;
using SimpleNewsReader.Infrastructure.News;
using SimpleNewsReader.Infrastructure.PageScraping;

namespace SimpleNewsReader.IntegrationTests;

public class NewsIntegrationTests
{
    private readonly INewsService _newsService;
    private readonly SimpleNewsReaderContext _context;
    private readonly IMapper _mapper;
    private readonly IPageScrapingService _pageScrapingService;

    public NewsIntegrationTests()
    {
        var options = GetDbContextOptions();
        _context = new SimpleNewsReaderContext(options);
        _mapper = SimpleNewsReaderMapper.Mapper;
        _pageScrapingService = new PageScrapingService();
        _newsService = new NewsService(_context,_mapper,_pageScrapingService);
    }

    private DbContextOptions<SimpleNewsReaderContext> GetDbContextOptions()
    {
        var service = new ServiceCollection().AddEntityFrameworkSqlServer().BuildServiceProvider();

        var builder = new DbContextOptionsBuilder<SimpleNewsReaderContext>();
        builder.UseSqlServer("Data Source=localhost,4433;Initial Catalog=SimpleNewsReader;User ID=sa;Password=Test@dmin123;");

        return builder.Options;
    }

    [Fact]
    public async Task ShouldAddNewsWithCorrectNumberOfNews()
    {
        var addNewsList = await _newsService.AddNewsAsync(1);

        var expectedNewsAddedCount = addNewsList.Count;

        var actualAddedNewsCount = (await _newsService.GetNewsByIdsAsync(addNewsList.Select(x => x.NewsId))).Count;

        Assert.Equal(expectedNewsAddedCount,actualAddedNewsCount);
    }
}