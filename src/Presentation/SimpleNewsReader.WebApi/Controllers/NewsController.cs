using Microsoft.AspNetCore.Mvc;
using SimpleNewsReader.Application.News;

namespace SimpleNewsReader.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;
        private readonly ILogger<NewsController> _logger;

        public NewsController(INewsService newsService, ILogger<NewsController> logger)
        {
            _newsService = newsService;
            _logger = logger;
        }

        [HttpGet("{pageId}")]
        public async Task<IActionResult> GetNews(int pageId)
        {
            try
            {
                var news = await _newsService.AddNewsAsync(pageId);
                return Ok(news);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
