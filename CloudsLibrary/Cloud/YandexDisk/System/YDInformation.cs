using Newtonsoft.Json.Linq;
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
        public double MaxFileSize { get; set; } = 0;
        public double TotalSpace { get; set; } = 0;
        public double UsedSpace { get; set; } = 0;
        public double TrashSize { get; set; } = 0;
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
            var nodes = JObject.Parse(jsonInformation);
            MaxFileSize = double.Parse(nodes["max_file_size"].ToString());
            TotalSpace = double.Parse(nodes["total_space"].ToString());
            UsedSpace = double.Parse(nodes["used_space"].ToString());
            TrashSize = double.Parse(nodes["trash_size"].ToString());
            UnlimitedAutouploadEnabled = bool.Parse(nodes["unlimited_autoupload_enabled"].ToString());
            IsPaid = bool.Parse(nodes["is_paid"].ToString());
            
            var folders = JObject.Parse(nodes["system_folders"].ToString());

            OdnoklasnikiFolderPath = folders["odnoklassniki"].ToString();
            GoogleFolderPath = folders["google"].ToString();
            InstagramFolderPath = folders["instagram"].ToString();
            VkFolderPath = folders["vkontakte"].ToString();
            MailRuFolderPath = folders["mailru"].ToString();
            DownloadsFolderPath = folders["downloads"].ToString();
            ApplicationsFolderPath = folders["applications"].ToString();
            FacebookFolderPath = folders["facebook"].ToString();
            SocialFolderPath = folders["social"].ToString();
            ScreenshotsFolderPath = folders["screenshots"].ToString();
            PhotosFolderPath = folders["photostream"].ToString();
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
