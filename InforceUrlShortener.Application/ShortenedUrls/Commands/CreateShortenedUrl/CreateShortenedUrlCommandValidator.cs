using FluentValidation;

namespace InforceUrlShortener.Application.ShortenedUrls.Commands.CreateShortenedUrl
{
    public class CreateShortenedUrlCommandValidator : AbstractValidator<CreateShortenedUrlCommand>
    {
        public CreateShortenedUrlCommandValidator()
        {
            RuleFor(c => c.OriginalUrl)
                .NotEmpty().WithMessage("Original url is required.")
                .Must(IsAValidHttpsUrl).WithMessage("Original url must be a valid HTTPS URL.");
        }
        private bool IsAValidHttpsUrl(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out var uriResult) &&
                   uriResult.Scheme == Uri.UriSchemeHttps;
        }
    }
}
