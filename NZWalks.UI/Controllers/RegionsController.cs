using Microsoft.AspNetCore.Mvc;
using NZWalks.UI.Models.DTO;

namespace NZWalks.UI.Controllers
{
    public class RegionsController(IHttpClientFactory httpClientFactory) : Controller
    {
        public async Task <IActionResult> Index()
        {
            List<RegionDTO> response = new List<RegionDTO>();
            try
            {
                //Get all regions from web api
                var client = httpClientFactory.CreateClient();
                var httpResponseMessage = await client.GetAsync("https://localhost:7260/api/regions");
                httpResponseMessage.EnsureSuccessStatusCode();
                response.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<RegionDTO>>());
            }
            catch (Exception ex)
            {
            }
            return View(response);
        }
    }
}
