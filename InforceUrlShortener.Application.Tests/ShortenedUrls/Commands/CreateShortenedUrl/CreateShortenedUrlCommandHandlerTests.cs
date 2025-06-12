using AutoMapper;
using FluentAssertions;
using InforceUrlShortener.Application.ShortenedUrls.DTOs;
using InforceUrlShortener.Application.User;
using InforceUrlShortener.Domain.Constants;
using InforceUrlShortener.Domain.Entities;
using InforceUrlShortener.Domain.Exceptions;
using InforceUrlShortener.Domain.RepositoriesInterfaces;
using InforceUrlShortener.Domain.ServicesInterfaces;
using Moq;
using Xunit;

namespace InforceUrlShortener.Application.ShortenedUrls.Commands.CreateShortenedUrl.Tests
{
    public class CreateShortenedUrlCommandHandlerTests
    {
        [Fact()]
        public async void ForValidCommand_ReturnsCreatedShortenedUrlDto()
        {
            //arrange
            var shortenedUrlRepositoryMock = new Mock<IShortenedUrlRepository>();
            var urlShortenerServiceMock = new Mock<IUrlShortenerService>();
            var userContextMock = new Mock<IUserContext>();
            var mapperMock = new Mock<IMapper>();

            var currentUser = new CurrentUser("1", "user@test.com", UserRoles.User);
            userContextMock.Setup(u => u.GetCurrentUser()).Returns(currentUser);

            var command = new CreateShortenedUrlCommand
            {
                OriginalUrl = "https://example.com"
            };

            shortenedUrlRepositoryMock
                .Setup(r => r.HasOriginalUrlDuplicateAsync(command.OriginalUrl))
                .ReturnsAsync(false);

            var shortCode = "qwer1234";
            urlShortenerServiceMock
                .Setup(s => s.GenerateShortCodeAsync())
                .ReturnsAsync(shortCode);

            ShortenedUrl capturedEntity = null!;
            shortenedUrlRepositoryMock
                .Setup(r => r.CreateAsync(It.IsAny<ShortenedUrl>()))
                .Callback<ShortenedUrl>(entity => capturedEntity = entity)
                .Returns(Task.CompletedTask);

            var expectedDto = new ShortenedUrlListItemDto
            {
                Id = Guid.NewGuid(),
                OriginalUrl = command.OriginalUrl,
                ShortCode = shortCode,
                OwnerId = currentUser.Id
            };
            mapperMock
                .Setup(m => m.Map<ShortenedUrlListItemDto>(It.IsAny<ShortenedUrl>()))
                .Returns(expectedDto);

            var handler = new CreateShortenedUrlCommandHandler(
                shortenedUrlRepositoryMock.Object,
                urlShortenerServiceMock.Object,
                userContextMock.Object,
                mapperMock.Object
            );

            //act
            var result = await handler.Handle(command, CancellationToken.None);
            result.Should().BeEquivalentTo(expectedDto);
            capturedEntity.OriginalUrl.Should().Be(command.OriginalUrl);
            capturedEntity.ShortCode.Should().Be(shortCode);
            capturedEntity.OwnerId.Should().Be(currentUser.Id);
            shortenedUrlRepositoryMock.Verify(r => r.CreateAsync(It.IsAny<ShortenedUrl>()), Times.Once);
        }

        [Fact]
        public async Task Handle_WhenOriginalUrlIsDuplicate_ThrowsOriginalUrlDuplicateException()
        {
            // Arrange
            var shortenedUrlRepositoryMock = new Mock<IShortenedUrlRepository>();
            var urlShortenerServiceMock = new Mock<IUrlShortenerService>();
            var userContextMock = new Mock<IUserContext>();
            var mapperMock = new Mock<IMapper>();

            var currentUser = new CurrentUser("1", "user@test.com", UserRoles.User);

            var command = new CreateShortenedUrlCommand
            {
                OriginalUrl = "https://duplicate.com"
            };

            shortenedUrlRepositoryMock
                .Setup(r => r.HasOriginalUrlDuplicateAsync(command.OriginalUrl))
                .ReturnsAsync(true);

            userContextMock.Setup(x => x.GetCurrentUser())
                .Returns(currentUser);

            var handler = new CreateShortenedUrlCommandHandler(
                shortenedUrlRepositoryMock.Object,
                urlShortenerServiceMock.Object,
                userContextMock.Object,
                mapperMock.Object
            );

            //act
            Func<Task> act = () => handler.Handle(command, CancellationToken.None);

            //assert
            await act.Should()
                .ThrowAsync<OriginalUrlDuplicateException>()
                .WithMessage($"Url: {command.OriginalUrl} already has shortened url. You can find it by original url"); // optional: check specific message
        }
    }
}