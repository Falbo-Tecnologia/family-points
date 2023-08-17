namespace Web.Controllers;

public class AuthenticatedController : Controller
{
    protected Usuario UsuarioLogado { get; set; }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        base.OnActionExecuting(context);

        var cookieName = "FamilyPointsAuthenticationToken";
        var cookie = Request.Cookies[cookieName];
        var result = new RedirectResult("/login");

        if (string.IsNullOrEmpty(cookie))
        {
            context.Result = result;
            return;
        }
        else
        {
            try
            {
                var jwtcookie = DecodeToken.Handler(cookie);
                var tokenExp = jwtcookie.Claims.First(claim => claim.Type.Equals("exp")).Value;
                var ticks = long.Parse(tokenExp);
                var tokenDate = DateTimeOffset.FromUnixTimeSeconds(ticks).DateTime;

                if (tokenDate <= DateTime.UtcNow)
                {
                    Response.Cookies.Delete(cookieName);
                    context.Result = result;
                    return;
                }
            }
            catch
            {
                Response.Cookies.Delete(cookieName);
                context.Result = result;
                return;
            }
        }

        UsuarioLogado = CookieExtension.SerializarToken(cookie);
        ViewBag.UsuarioLogado = UsuarioLogado;

        if (!HttpContext.Request.RouteValues.Values.LastOrDefault().ToString().Equals("Index"))
        {
            if (!UsuarioLogado.UsuarioOpcoes.Any(x => !string.IsNullOrEmpty(x.OpcaoSistema.Descricao) &&
                x.OpcaoSistema.Descricao.RemoveDiacritics().Replace(" ", "").ToLower().Equals(ControllerContext.ActionDescriptor.ControllerName.ToLower())))
                context.Result = new RedirectResult("/error/opcao-nao-encontrada");
        }

        if (context.HttpContext.Response.StatusCode == 200)
        {
            context.HttpContext.Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            context.HttpContext.Response.Headers["Pragma"] = "no-cache";
            context.HttpContext.Response.Headers["Expires"] = "0";
        }
    }
}
