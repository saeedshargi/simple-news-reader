using Microsoft.Extensions.DependencyInjection;
using SimpleNewsReader.Application.News;
using SimpleNewsReader.Infrastructure.News;
using SimpleNewsReader.Infrastructure.PageScraping;

namespace SimpleNewsReader.Infrastructure;

public static class IocResolveServices
{
    public static void ResolveServices(this IServiceCollection service)
    {
        service.AddScoped<INewsService, NewsService>();
        service.AddScoped<IPageScrapingService, PageScrapingService>();
    }
}