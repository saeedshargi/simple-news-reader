using FluentValidation;

namespace SimpleNewsReader.Application.News;

public class NewsValidator: AbstractValidator<NewsDto>
{
    public NewsValidator()
    {
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x.NewsId).NotEmpty();
    }
}