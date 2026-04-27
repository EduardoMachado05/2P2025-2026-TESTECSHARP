using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyCoreApp.Models;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyCoreApp.Pages;

public class InfopaisModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public InfopaisModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public string? CodigoPais { get; set; }
    public Pais? InfoPais { get; set; }

    public async Task OnGetAsync(string cod)
    {
        CodigoPais = cod ?? string.Empty;

        if (string.IsNullOrEmpty(cod))
        {
            return;
        }

        var client = _httpClientFactory.CreateClient("RestCountries");
        var response = await client.GetAsync($"v3.1/alpha/{cod}?fields=name,cca2,flags");

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            CountryApiResponse? artigoResponse = null;

            var trimmedJson = json.TrimStart();
            if (trimmedJson.StartsWith("["))
            {
                artigoResponse = JsonSerializer.Deserialize<List<CountryApiResponse>>(json, options)?.FirstOrDefault();
            }
            else
            {
                artigoResponse = JsonSerializer.Deserialize<CountryApiResponse>(json, options);
            }

            if (artigoResponse != null)
            {
                InfoPais = new Pais
                {
                    OfficialName = artigoResponse.name?.official,
                    Cca2 = artigoResponse.cca2,
                    FlagUrl = artigoResponse.flags?.png
                };
            }
        }
    }
}
