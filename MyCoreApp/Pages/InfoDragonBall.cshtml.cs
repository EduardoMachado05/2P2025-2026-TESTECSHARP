using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyCoreApp.Models;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyCoreApp.Pages;

public class InfoDragonBallModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public InfoDragonBallModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

public string? CodigoDragonBall { get; set; }
    public string? name  { get; set; }
    public string? description { get; set; }
    public string? image { get; set; }
    public string? affiliation  { get; set; }
    public Personagem? InfoDragonBall { get; set; }

    public async Task OnGetAsync(string cod)
    {
        CodigoDragonBall = cod ?? string.Empty;
        name = cod ?? string.Empty;

        if (string.IsNullOrEmpty(cod))
        {
            return;
        }

        var client = _httpClientFactory.CreateClient("DragonBall");
        var response = await client.GetAsync($"{cod}");

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            InfoDragonBall = JsonSerializer.Deserialize<Personagem>(json, options);
        }
    }
}
