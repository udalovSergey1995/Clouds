using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CloudsLibrary.REST
{
    public class RestClient : HttpClient
    {
        public static RestClient Client { get; set; } = new RestClient();
        public async Task<string> Send(string command)
        {
            string result = String.Empty;
            result = await GetStringAsync(command);
            return result;
        }
        public static async Task<string> SendCommand(string command)
        {
            ///Добавить сюда возможность передавать хэдеры
            return await Client.Send(command);
        }
    }
}
