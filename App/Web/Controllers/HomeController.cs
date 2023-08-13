namespace Web.Controllers;

public class HomeController : AuthenticatedController
{
    public HomeController()
    {

    }

    public IActionResult Index() => View();

    public IActionResult Privacy() => View();
}
