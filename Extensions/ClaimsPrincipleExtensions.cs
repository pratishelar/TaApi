using System.Security.Claims;

namespace Extensions
{
    public static class ClaimsPrincipleExtensions
    {
        public static string Getusername(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}