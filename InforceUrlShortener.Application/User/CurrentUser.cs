namespace InforceUrlShortener.Application.User
{
    public record CurrentUser (string Id, string Email, string Role)
    {
        public bool IsInRole(string RoleName) => RoleName == Role;
    }
}
