using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using NZWalksUI.Models;
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
    [HttpGet]
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

    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        var client = _httpClientFactory.CreateClient();
        var response = await client.GetFromJsonAsync<RegionDto>($"https://localhost:7163/api/regions/{id.ToString()}");

        if (response is not null)
            return View(response);


        return View(null);
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddRegionViewModel regionViewModel)
    {
        var client = _httpClientFactory.CreateClient();

        var httpRequestMessage = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri("https://localhost:7163/api/regions"),
            Content = new StringContent(JsonSerializer.Serialize(regionViewModel), Encoding.UTF8, "application/json")
        };
        
        var httpResponseMessage = await client.SendAsync(httpRequestMessage);
        httpResponseMessage.EnsureSuccessStatusCode();

        var response = await httpResponseMessage.Content.ReadFromJsonAsync<RegionDto>();

        if (response is not null)
            return RedirectToAction("Index", "Regions");

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Edit(RegionDto request)
    {
        var client = _httpClientFactory.CreateClient();
        
        var httpRequestMessage = new HttpRequestMessage
        {
            Method = HttpMethod.Put,
            RequestUri = new Uri($"https://localhost:7163/api/regions/{request.Id}"),
            Content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json")
        };
        
        var httpResponseMessage = await client.SendAsync(httpRequestMessage);
        httpResponseMessage.EnsureSuccessStatusCode();

        var response = await httpResponseMessage.Content.ReadFromJsonAsync<RegionDto>();

        if (response is not null)
            return RedirectToAction("Edit", "Regions");

        return View();
    }
    
    
    
    [HttpPost]
    public async Task<IActionResult> Delete(RegionDto request)
    {
        try
        {
            var client = _httpClientFactory.CreateClient();

            var httpResponseMessage = await client.DeleteAsync($"https://localhost:7163/api/regions/{request.Id}");
            httpResponseMessage.EnsureSuccessStatusCode();

            return RedirectToAction("Index", "Regions");
        }
        catch (Exception ex)
        {
            //Console
        }

        return View("Edit");
    }
}