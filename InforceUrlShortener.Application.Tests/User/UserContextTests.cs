using System.Security.Claims;
using FluentAssertions;
using InforceUrlShortener.Domain.Constants;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace InforceUrlShortener.Application.User.Tests
{
    public class UserContextTests
    {
        [Fact()]
        public void GetCurrentUser_WithAuthenticatedUser_ShouldReturnCurrentUser()
        {
            //arrange
            var userEmail = "admin@test.com";
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, "1"),
                new(ClaimTypes.Email, userEmail),
                new(ClaimTypes.Role, UserRoles.Admin),
            };
            var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "Test"));

            httpContextAccessorMock.Setup(x => x.HttpContext)
                .Returns(new DefaultHttpContext()
                {
                    User = user
                });

            var userContext = new UserContext(httpContextAccessorMock.Object);


            //act
            var currentUser = userContext.GetCurrentUser();

            //assert
            currentUser.Should().NotBeNull();
            currentUser.Id.Should().Be("1");
            currentUser.Email.Should().Be(userEmail);
        }

        [Fact]
        public void GetCurrentUser_WithUserContextNotPresent_ThrowsInvalidOperationException()
        {
            //arrange
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            httpContextAccessorMock.Setup(x => x.HttpContext)
                .Returns((HttpContext)null);

            var userContext = new UserContext(httpContextAccessorMock.Object);

            //act
            Action act = () => userContext.GetCurrentUser();

            //assert
            act.Should().Throw<InvalidOperationException>()
                .WithMessage("User context is null");
        }
    }
}