using Microsoft.AspNetCore.Mvc;

namespace NZWalksUI.Controllers;

public class RegionsController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public RegionsController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    // GET
    public async Task<IActionResult> Index()
    {
        try
        {
            //Get All Regions from Web API
            var client = _httpClientFactory.CreateClient();
            var httpResponseMessage = await client.GetAsync("https://localhost:7163/api/regions"); 

            httpResponseMessage.EnsureSuccessStatusCode();

            var stringResponseBody = await httpResponseMessage.Content.ReadAsStreamAsync();
            ViewBag.Response = stringResponseBody;
        }
        catch (Exception e)
        {
            //Log the exception
        }

        return View();
    }
}