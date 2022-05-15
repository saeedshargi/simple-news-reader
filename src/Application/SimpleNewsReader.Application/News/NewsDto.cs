using ValidationException = SimpleNewsReader.Application.Exceptions.ValidationException;

namespace SimpleNewsReader.Application.News;

public class NewsDto
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public long NewsId { get; set; }
    public string? Category { get; set; }
    public string? Description { get; set; }
    public string? Author { get; set; }
    public string? PublishDate { get; set; }
    public string? ImageUrl { get; set; }
    public string? CreatedDate { get; set; }
    public string? ModifiedDate { get; set; }

    public void ValidateNews()
    {
        var newsValidator = new NewsValidator();
        var validationResult = newsValidator.Validate(this);
        if (!validationResult.IsValid)
        {
            var validationErrorMessage =string.Join("\n",
                validationResult.Errors.SelectMany(x =>
                    $"{x.PropertyName} failed validation.Error is : {x.ErrorMessage}"));
            throw new ValidationException(validationErrorMessage);
        }
    }
}