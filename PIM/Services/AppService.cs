using System.Text.Json;
namespace PIM.Services
{
    public class AppService
    {
        private readonly HttpClient _httpClient;

        public AppService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /*  public async Task<ViaCepResponse?> ConsultarCepAsync(string cep)
          {
              var url = $"https://viacep.com.br/ws/{cep}/json/";

              var response = await _httpClient.GetAsync(url);

              if (!response.IsSuccessStatusCode)
                  return null;

              var json = await response.Content.ReadAsStringAsync();

              return JsonSerializer.Deserialize<ViaCepResponse>(json,
                  new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
          }*/
    }   
}
