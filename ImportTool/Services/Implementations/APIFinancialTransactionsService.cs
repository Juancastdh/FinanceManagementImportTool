using FinanceManagementImportTool.Models;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;

namespace FinanceManagementImportTool.Services.Implementations
{
    public class APIFinancialTransactionsService : IFinancialTransactionsService
    {

        private readonly string APIBaseUrl;

        public APIFinancialTransactionsService(string apiBaseUrl)
        {
            APIBaseUrl = apiBaseUrl;
        }

        public async Task AddFinancialTransaction(FinancialTransaction financialTransaction)
        {
            string endpointUrl = $"{APIBaseUrl}/FinancialTransactions";
            HttpClient client = new HttpClient();

            string jsonPayload = JsonSerializer.Serialize(financialTransaction);
            HttpContent httpContent = new StringContent(jsonPayload);

            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = await client.PostAsync(endpointUrl, httpContent);

            if (response.StatusCode != HttpStatusCode.Created)
            {
                throw new Exception($"{endpointUrl} POST Request failed. Reason: {response.ReasonPhrase}");
            }
        }

    }
}
