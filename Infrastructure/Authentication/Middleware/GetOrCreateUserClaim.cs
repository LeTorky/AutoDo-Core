using System.Security.Claims;

public class GetOrCreateUserClaim
{
    private readonly RequestDelegate _next;

    public GetOrCreateUserClaim(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IUserRepository userRepository)
    {
        var User = context.Items["User"] as ClaimsPrincipal;
        var userEmail = User?.FindFirst("Email")?.Value;
        var userExists = userRepository.UserExistsByEmail(userEmail);
        if(!userExists)
            userRepository.CreateUserWithEmailAsync(userEmail);
        await _next(context);
    }
}
