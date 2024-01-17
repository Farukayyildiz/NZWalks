using Microsoft.AspNetCore.Mvc;
using NZWalksUI.Models.DTO;

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
        List<RegionDto> response = new List<RegionDto>();
        try
        {
            //Get All Regions from Web API
            var client = _httpClientFactory.CreateClient();
            var httpResponseMessage = await client.GetAsync("https://localhost:7163/api/regions");

            httpResponseMessage.EnsureSuccessStatusCode();

            response.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<RegionDto>>());
        }
        catch (Exception e)
        {
            //Log the exception
        }

        return View(response);
    }
}