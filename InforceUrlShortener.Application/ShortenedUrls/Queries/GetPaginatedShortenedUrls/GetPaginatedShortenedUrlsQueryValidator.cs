using FluentValidation;
using InforceUrlShortener.Application.ShortenedUrls.Queries.GetPaginatedShortenedUrls;

public class GetPaginatedShortenedUrlsQueryValidator : AbstractValidator<GetPaginatedShortenedUrlsQuery>
{
    private int[] allowedPageSizes = [5, 10, 20];
    public GetPaginatedShortenedUrlsQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1);

        RuleFor(x => x.PageSize)
               .Must(value => allowedPageSizes.Contains(value))
               .WithMessage($"Page size must be in [{string.Join(",", allowedPageSizes)}]");
    }
}