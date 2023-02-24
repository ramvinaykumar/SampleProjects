using Newtonsoft.Json;
using RandomActivity.API.Models;

namespace RandomActivity.API.Services
{
    public class MyActivityService : IMyActivityService
    {
        private readonly HttpClient _httpClient;

        public MyActivityService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<MyActivityDto?> GetNewActity(CancellationToken cancellationToken = default)
        {
            _httpClient.BaseAddress = new Uri("https://www.boredapi.com");
            var responseMessage = await _httpClient.GetAsync("/api/activity", cancellationToken);
            if (responseMessage.IsSuccessStatusCode)
            {
                var stream = await responseMessage.Content.ReadAsStringAsync(cancellationToken);
                return JsonConvert.DeserializeObject<MyActivityDto>(stream);
            }
            return null;
        }
    }
}
