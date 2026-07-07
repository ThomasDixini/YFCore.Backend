namespace YFCore.Application.Users.DTOs
{
    public sealed record AuthResultDto(Guid UserId, string Token);
}
