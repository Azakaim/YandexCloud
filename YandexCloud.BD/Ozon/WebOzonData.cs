﻿using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using YandexCloud.BD.Models;
using YandexCloud.CORE.DTOs;
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

        public async Task<OzonResponseModel> GetDeliveryDataAsync()
        {
            var httpClient = _httpClientFactory.CreateClient();
            var url = "https://api-seller.ozon.ru/v3/finance/transaction/list";

            var date = new Date
            {
                from = DateTime.SpecifyKind(new DateTime(2023, 6, 1), DateTimeKind.Utc),
                to = DateTime.SpecifyKind(new DateTime(2023, 6, 30), DateTimeKind.Utc),
            };

            var filter = new Filter
            {
                date = date,
                operation_type = new List<string> { "OperationAgentDeliveredToCustomer" },
                posting_number = "",
                transaction_type = "all"
            };

            var transactionListModel = new TransactionListModel
            {
                filter = filter,
                page = 1,
                page_size = 1000,
            };

            httpClient.DefaultRequestHeaders.Add("Client-Id", "129047");
            httpClient.DefaultRequestHeaders.Add("Api-Key", "06ef9d66-b383-4498-b75d-b5df7df4ce19");

            using var content = new StringContent(JsonSerializer.Serialize(transactionListModel), Encoding.UTF8, "application/json");
            var responce = await httpClient.PostAsync(url, content);

            if (!responce.IsSuccessStatusCode)
                throw new HttpRequestException($"Получен код {responce.StatusCode}");

            var ozonResponceModel = await responce.Content.ReadFromJsonAsync<OzonResponseModel>();

            return ozonResponceModel;
        }
    }
}