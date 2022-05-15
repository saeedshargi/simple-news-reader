using SimpleNewsReader.Domain.Exceptions;
using SimpleNewsReader.Infrastructure.PageScraping;

namespace SimpleNewsReader.UnitTests;

public class PageScrapingTests
{
    private readonly IPageScrapingService _pageScrapingService;

    public PageScrapingTests()
    {
        _pageScrapingService = new PageScrapingService();
    }

    [Fact]
    public async Task ShouldGetErrorWhenPageNumberIsInvalid()
    {
        await Assert.ThrowsAsync<BadRequestException>(() => _pageScrapingService.GetNewsOfPageAsync(-1));
    }

    [Fact]
    public async Task ShouldSuccessWhenPageNumberIsValid()
    {
        var news = await _pageScrapingService.GetNewsOfPageAsync(1);

        var expected = news.Count != 0;

        Assert.True(expected);
    }
}