using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CloudsLibrary.REST
{
    public class RestClient : HttpClient
    {
        /// <summary>
        /// Базовый класс для REST клиентов облаков
        /// </summary>
        public static RestClient Client { get; set; } = new RestClient();

        /// <summary>
        /// Отправить комманду в облако
        /// </summary>
        /// <param name="command">REST запрос к серверу</param>
        /// <returns>JSON ответ сервера</returns>
        public async Task<string> Send(string command)
        {
            string result = String.Empty;
            result = await GetStringAsync(command);
            return result;
        }

        /// <summary>
        /// Отправить комманду в облако. Статичная модификация метода Send(string command)
        /// </summary>
        /// <param name="command">REST запрос к серверу</param>
        /// <returns>JSON ответ сервера</returns>
        public static async Task<string> SendCommand(string command)
        {
            ///Добавить сюда возможность передавать хэдеры
            return await Client.Send(command);
        }
    }
}
