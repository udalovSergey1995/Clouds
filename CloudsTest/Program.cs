using CloudsLibrary.REST;
using CloudsLibrary.System;
using System;

namespace CloudsTest
{
    //https://oauth.yandex.ru/authorize?response_type=token&client_id=0d41a78f77084835b7d32ef535121cac
    class Program
    {
        static void Main(string[] args)
        {
            Ivoke();
            Console.ReadLine();
        }

        private static async void Ivoke()
        {
            await Information.SendTokenRequest();
            //var t = await RestClient.Send("");
        }
    }
}
