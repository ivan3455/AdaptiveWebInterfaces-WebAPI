namespace AdaptiveWebInterfaces_WebAPI.Services.Jwt
{
    public interface IJwtService
    {
        string GenerateToken(string email);
    }
}
