using FluentAssertions;
using InforceUrlShortener.Domain.RepositoriesInterfaces;
using Moq;
using Xunit;

namespace InforceUrlShortener.Infrastructure.Services.Tests
{
    public class UrlShortenerServiceTests
    {
        [Fact]
        public async Task Service_ShouldReturn8CharacterUniqueCode()
        {
            //arrange
            var repositoryMock = new Mock<IShortenedUrlRepository>();
            repositoryMock.Setup(r => r.HasShortCodeDuplicateAsync(It.IsAny<string>()))
                          .ReturnsAsync(false);

            var service = new UrlShortenerService(repositoryMock.Object);

            //act
            var code = await service.GenerateShortCodeAsync();

            //assert
            code.Should().NotBeNullOrEmpty();
            code.Length.Should().Be(8);
            repositoryMock.Verify(r => r.HasShortCodeDuplicateAsync(code), Times.Once);
        }

        [Fact]
        public async Task Service_ShouldRetryIfCodeIsDuplicate()
        {
            //arrange
            var repositoryMock = new Mock<IShortenedUrlRepository>();
            
            repositoryMock.SetupSequence(r => r.HasShortCodeDuplicateAsync(It.IsAny<string>()))
                          .ReturnsAsync(true)
                          .ReturnsAsync(false);

            var service = new UrlShortenerService(repositoryMock.Object);

            //act
            var code = await service.GenerateShortCodeAsync();

            //assert
            code.Should().NotBeNullOrEmpty();
            code.Length.Should().Be(8);
            repositoryMock.Verify(r => r.HasShortCodeDuplicateAsync(It.IsAny<string>()), Times.Exactly(2));
        }
    }
}