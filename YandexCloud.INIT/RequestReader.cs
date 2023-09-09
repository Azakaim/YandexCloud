using YandexCloud.CORE.DTOs;
using YandexCloud.INIT.Infrastructure;

namespace YandexCloud.INIT
{
    public class RequestReader : IRequestReader
    {
        public RequestDataDto Read()
        {
            var model = new RequestDataDto();
            string initString = string.Empty;

            model.From = initString.ReadDateTime("Введите дату начала");
            model.To = initString.ReadDateTime("Введите дату конца");
            model.ClientId = model.ClientId.ReadString("Введите id клиента");
            model.ApiKey = model.ApiKey.ReadString("Введите ключ api");

            return model;
        }
    }
}
