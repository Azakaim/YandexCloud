using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using YandexCloud.BD.Models;
using YandexCloud.CORE.DTOs;
using YandexCloud.CORE.Models;
using YandexCloud.CORE.Repositories;

namespace YandexCloud.BD.Ozon
{
    public class WebOzonData : IOzonFullData
    {
        readonly IHttpClientFactory _httpClientFactory;

        public WebOzonData(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<OzonResponseModel> GetDeliveryDataAsync(RequestDataModel requestModel)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var url = "https://api-seller.ozon.ru/v3/finance/transaction/list";

            var date = new Date
            {
                from = requestModel.From,
                to = requestModel.To,
            };

            var filter = new Filter
            {
                date = date,
                operation_type = new List<string> { requestModel.OperationType },
                posting_number = "",
                transaction_type = "all"
            };

            var transactionListModel = new TransactionListModel
            {
                filter = filter,
                page = requestModel.Page,
                page_size = 1000,
            };

            httpClient.DefaultRequestHeaders.Add("Client-Id", requestModel.ClientId);
            httpClient.DefaultRequestHeaders.Add("Api-Key", requestModel.ApiKey);

            using var content = new StringContent(JsonSerializer.Serialize(transactionListModel), Encoding.UTF8, "application/json");
            var responce = await httpClient.PostAsync(url, content);

            if (!responce.IsSuccessStatusCode)
                throw new HttpRequestException($"Получен код {responce.StatusCode}");

            var ozonResponceModel = await responce.Content.ReadFromJsonAsync<OzonResponseModel>();

            return ozonResponceModel;
        }
    }
}
