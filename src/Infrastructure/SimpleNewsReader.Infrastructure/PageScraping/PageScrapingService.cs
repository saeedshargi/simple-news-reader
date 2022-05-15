using HtmlAgilityPack;
using SimpleNewsReader.Application.News;

namespace SimpleNewsReader.Infrastructure.PageScraping;

public class PageScrapingService: IPageScrapingService
{
    private const string PageUrl = "https://www.zoomit.ir/page/";
    private const string PageName = "https://www.zoomit.ir/";
    public async Task<IReadOnlyList<NewsDto>> GetNewsOfPageAsync(int pageNumber)
    {
        var newsList = new List<NewsDto>();
        var htmlWeb = new HtmlWeb();
        var htmlDoc = htmlWeb.Load($"{PageUrl}{pageNumber}/");
        var newNewsNode = htmlDoc.GetElementbyId("ArticleDetails").FirstChild;
        if (newNewsNode == null)
        {
            return newsList;
        }

        foreach (var node in newNewsNode.ChildNodes.Where(x => x.HasClass("item-list-row")))
        {
            var imageNode = node.FirstChild.ChildNodes.FirstOrDefault(x => x.HasClass("col-md-4"));
            var imageUrl = imageNode?.ChildNodes.FirstOrDefault(x => x.Name == "a")?.Attributes["href"].Value ?? "";
            var newsId = imageUrl.Remove(0, PageName.Length).Split("/")[1].Split("-")[0];
            var imageSrc = imageNode?.ChildNodes.FirstOrDefault(x => x.Name == "a")?.ChildNodes.FirstOrDefault(x => x.Name == "img")?.Attributes["src"].Value ?? "";
            var contentNode = node.FirstChild.ChildNodes.FirstOrDefault(x => x.HasClass("col-md-8"));
            if (contentNode == null) continue;
            var category = contentNode.ChildNodes.FirstOrDefault(x => x.HasClass("catgroup"))?.InnerText ?? "";
            var title = HtmlEntity.DeEntitize(contentNode.ChildNodes.FirstOrDefault(x => x.Name == "h3")?.InnerText ?? "");
            var description = HtmlEntity.DeEntitize(contentNode.ChildNodes.FirstOrDefault(x => x.Name == "p")?.InnerText ?? "");
            var authorNode = contentNode.ChildNodes.FirstOrDefault(x => x.HasClass("ListItemHeader"));
            var author = authorNode?.ChildNodes.FirstOrDefault(x => x.HasClass("authorlist"))?.InnerText ?? "";
            var createDate = authorNode?.ChildNodes.FirstOrDefault(x => x.HasClass("datelist"))?.InnerText ?? "";
            var news = new NewsDto
            {
                Description = description,
                Author = author,
                Category = category,
                ImageUrl = imageSrc,
                NewsId = long.Parse(newsId),
                PublishDate = createDate,
                Title = title
            };
            news.ValidateNews();
            newsList.Add(news);
        }
        
        return newsList;
    }
}