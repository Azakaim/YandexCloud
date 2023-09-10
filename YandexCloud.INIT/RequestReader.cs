using YandexCloud.CORE.DTOs;
using YandexCloud.INIT.Infrastructure;

namespace YandexCloud.INIT
{
    public class RequestReader : IRequestReader
    {
        readonly IConsoleReader _consoleReader;

        public RequestReader(IConsoleReader consoleReader)
        {
            _consoleReader = consoleReader;
        }

        public RequestDataDto Read()
        {
            var model = new RequestDataDto();

            model.From = _consoleReader.ReadDateTime("Введите дату начала");
            model.To = _consoleReader.ReadDateTime("Введите дату конца");
            model.ClientId = _consoleReader.ReadString("Введите id клиента");
            model.ApiKey = _consoleReader.ReadString("Введите ключ api");

            return model;
        }
    }
}
