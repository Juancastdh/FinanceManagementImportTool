using FinanceManagementImportTool.Models;
using System.Net;
using System.Text.Json;

namespace FinanceManagementImportTool.Services.Implementations
{
    public class APIPeriodsService : IPeriodsService
    {
        private readonly string APIBaseUrl;

        public APIPeriodsService(string apiBaseUrl)
        {
            APIBaseUrl = apiBaseUrl;
        }

        public async Task<IEnumerable<Period>> GetPeriods()
        {
            string endpointUrl = $"{APIBaseUrl}/Periods";
            HttpClient client = new HttpClient();

            HttpResponseMessage response = await client.GetAsync(endpointUrl);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"{endpointUrl} GET Request failed. Reason: {response.ReasonPhrase}");
            }

            string responseBody = await response.Content.ReadAsStringAsync();

            IEnumerable<Period> periods = JsonSerializer.Deserialize<List<Period>>(responseBody, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? new List<Period>();
            return periods;
        }
    }
}
