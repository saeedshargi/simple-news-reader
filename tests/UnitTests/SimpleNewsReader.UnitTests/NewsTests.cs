using FluentValidation.TestHelper;
using SimpleNewsReader.Application.Exceptions;
using SimpleNewsReader.Application.News;

namespace SimpleNewsReader.UnitTests;

public class NewsTests
{
    private NewsDto _newsDto;
    private readonly NewsValidator _newsValidator;

    public NewsTests()
    {
        _newsDto = new NewsDto
        {
            Id = 1,
            NewsId = 1,
            ModifiedDate = "1401/02/25",
            Author = "Saeid",
            Category = "Programming",
            CreatedDate = "1401/02/25",
            Description = "Simple News Reader Test App",
            ImageUrl = "c:\\test.jpg",
            PublishDate = "1401/02/25",
            Title = "Test News"
        };
        _newsValidator = new NewsValidator();
    }

    [Fact]
    public async Task ShouldGetErrorWhenTitleIsNull()
    {
        _newsDto.Title = null;

        var result = await _newsValidator.TestValidateAsync(_newsDto);

        result.ShouldHaveValidationErrorFor(news => news.Title);
    }

    [Fact]
    public async Task ShouldGetErrorWhenTitleIsEmpty()
    {
        _newsDto.Title = "";

        var result = await _newsValidator.TestValidateAsync(_newsDto);

        result.ShouldHaveValidationErrorFor(news => news.Title);
    }

    [Fact]
    public async Task ShouldSuccessWhenTitleIsCorrect()
    {
        var result = await _newsValidator.TestValidateAsync(_newsDto);

        result.ShouldNotHaveValidationErrorFor(news => news.Title);
    }

    [Fact]
    public async Task ShouldGetErrorWhenNewsIdIsZero()
    {
        _newsDto.NewsId = 0;

        var result = await _newsValidator.TestValidateAsync(_newsDto);

        result.ShouldHaveValidationErrorFor(news => news.NewsId);
    }

    [Fact]
    public async Task ShouldSuccessWhenNewsIdCorrect()
    {
        var result = await _newsValidator.TestValidateAsync(_newsDto);

        result.ShouldNotHaveValidationErrorFor(news => news.NewsId);
    }

    [Fact]
    public void ShouldGetErrorWhenValidateWithNullTitle()
    {
        _newsDto.Title = null;

        Assert.Throws<ValidationException>(() => _newsDto.ValidateNews());
    }

    [Fact]
    public void ShouldGetErrorWhenValidateWithZeroNewsId()
    {
        _newsDto.NewsId = 0;

        Assert.Throws<ValidationException>(() => _newsDto.ValidateNews());
    }
}