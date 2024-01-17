using Microsoft.AspNetCore.Mvc;

namespace NZWalksUI.Controllers;

public class RegionsController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}