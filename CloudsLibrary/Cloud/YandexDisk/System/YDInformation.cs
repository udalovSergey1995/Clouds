using System;
using System.Collections.Generic;
using System.Text;

namespace CloudsLibrary.Cloud.YandexDisk.System
{
    public class YDInformation
    {
        public static string TokenRequestString { get; set; } = "https://passport.yandex.ru/auth?retpath=https%3A%2F%2Foauth.yandex.ru%2Fauthorize%3Fresponse_type%3Dtoken%26client_id%3D0d41a78f77084835b7d32ef535121cac&noreturn=1&origin=oauth";
        public static string Token { get; set; } = "";

        public YDUser UserInformation { get; set; }
        public int MaxFileSize { get; set; } = 0;
        public int TotalSpace { get; set; } = 0;
        public int UsedSpace { get; set; } = 0;
        public int TrashSize { get; set; } = 0;
        public bool UnlimitedAutouploadEnabled { get; set; } = false;
        public bool IsPaid { get; set; } = false;

        public string VkFolderPath { get; set; }
        public string OdnoklasnikiFolderPath { get; set; }
        public string InstagramFolderPath { get; set; }
        public string GoogleFolderPath { get; set; }
        public string MailRuFolderPath { get; set; }
        public string DownloadsFolderPath { get; set; }
        public string ApplicationsFolderPath { get; set; }
        public string FacebookFolderPath { get; set; }
        public string SocialFolderPath { get; set; }
        public string ScreenshotsFolderPath { get; set; }
        public string PhotosFolderPath { get; set; }

        public void SetInformation(string jsonInformation)
        {
            var js = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonInformation);
            
        }
        public bool SetTokenAsUri(string uri)
        {
            try
            {
                YDInformation.Token = uri.Split('=')[1].Split('&')[0];
                return true;
            }
            catch
            {
                return false;
            }
        }
        public void SetToken(string token)
        {
            YDInformation.Token = token;
        }
    }
}
