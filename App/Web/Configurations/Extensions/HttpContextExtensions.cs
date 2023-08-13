namespace Web.Configurations.Extensions;

public static class HttpContextExtensions
{
    public static string GetBaseUrl(this HttpContext httpContext)
    {
        var request = httpContext.Request;
        return $"{request.Scheme}://{request.Host}{request.PathBase}".TrimEnd('/');
    }

    public static bool IsAjaxRequest(this HttpContext httpContext)
    {
        return httpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
    }
}
