namespace Web.Configurations.Middlewares;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly string _environment;
    private readonly ILogWriter _logWriter;

    public ExceptionHandlerMiddleware(RequestDelegate next, AppSetting appSetting, ILogWriter logWriter)
    {
        _next = next;
        _environment = appSetting.EnvironmentName;
        _logWriter = logWriter;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (Exception ex)
        {
            if (context.IsAjaxRequest())
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync("Ocorreu um erro inesperado. Contate o Administrador!");
            }
            else
            {
                context.Response.Redirect("/error");
            }

            var route = context.Request.Path.ToString().ToLower().Trim('/');
            if (_environment.Equals("DEVELOPMENT", StringComparison.OrdinalIgnoreCase))
            {
                _logWriter.ConsoleWrite($"ROUTE....: {route}", ConsoleColor.Yellow);
                _logWriter.ConsoleWrite($"EXCEPTION: {ex}", ConsoleColor.White);
            }
            else
                _logWriter.Write("ExceptionHandler.txt", $"ROUTE....: {route}", $"EXCEPTION: {ex}");
        }
    }
}
