using Xunit;
using InforceUrlShortener.Application.ShortenedUrls.Commands.CreateShortenedUrl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.TestHelper;

namespace InforceUrlShortener.Application.ShortenedUrls.Commands.CreateShortenedUrl.Tests
{
    public class CreateShortenedUrlCommandValidatorTests
    {
        private readonly CreateShortenedUrlCommandValidator _validator;

        public CreateShortenedUrlCommandValidatorTests()
        {
            _validator = new CreateShortenedUrlCommandValidator();
        }

        [Fact]
        public void Valiadtor_ForInvalidCommand_ShouldHaveValidationErrorIsRequired()
        {
            // arrange
            var command = new CreateShortenedUrlCommand { OriginalUrl = "" };

            // act
            var result = _validator.TestValidate(command);

            // assert
            result.ShouldHaveValidationErrorFor(c => c.OriginalUrl)
                  .WithErrorMessage("Original url is required.");
        }

        [Theory]
        [InlineData("http://youtube.com")]
        [InlineData("https:/youtube.com")]
        [InlineData("youtube.com")]
        public void Valiadtor_ForInvalidCommand_ShouldHaveValidationErrorInvalidHttps(string invalidUrl)
        {
            // arrange
            var command = new CreateShortenedUrlCommand { OriginalUrl = invalidUrl };

            // act
            var result = _validator.TestValidate(command);

            // assert
            result.ShouldHaveValidationErrorFor(c => c.OriginalUrl)
                  .WithErrorMessage("Original url must be a valid HTTPS URL.");
        }

        [Theory]
        [InlineData("https://youtube.com")]
        [InlineData("https://www.youtube.com/watch?v=dQw4w9WgXcQ")]
        public void Valiadtor_ForValidCommand_ShouldNotHaveValidationError(string validUrl)
        {
            // Arrange
            var command = new CreateShortenedUrlCommand { OriginalUrl = validUrl };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveValidationErrorFor(c => c.OriginalUrl);
        }
    }
}