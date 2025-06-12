using FluentAssertions;
using InforceUrlShortener.Domain.Constants;
using Xunit;

namespace InforceUrlShortener.Application.User.Tests
{
    public class CurrentUserTests
    {
        [Theory()]
        [InlineData(UserRoles.Admin)]
        public void IsInRole_WithMatchingRole_ShouldReturnTrue(string roleName)
        {
            //arrange
            var currentUser = new CurrentUser(string.Empty, string.Empty, UserRoles.Admin);

            //act
            var isInRole = currentUser.IsInRole(roleName);

            //assert
            isInRole.Should().BeTrue();
        }

        [Theory()]
        [InlineData(UserRoles.Admin)]
        public void IsInRole_WithNoMatchingRole_ShouldReturnFalse(string roleName)
        {
            //arrange
            var currentUser = new CurrentUser(string.Empty, string.Empty, UserRoles.User);

            //act
            var isInRole = currentUser.IsInRole(roleName);

            //assert
            isInRole.Should().BeFalse();
        }
    }
}