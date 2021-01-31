using BackgroundShareManager;
using CloudsLibrary.Cloud.YandexDisk;
using CloudsLibrary.Cloud.YandexDisk.FilesSystem;
using CloudsLibrary.Cloud.YandexDisk.System;
using CloudsLibrary.REST;
using CloudsLibrary.System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using Windows.ApplicationModel.Core;
using Windows.Networking.BackgroundTransfer;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace CloudTest
{
    public sealed partial class MainPage : Page
   {
        YandexDiskWorker yandexDiskWorker = new YandexDiskWorker();
        Share share = new Share();
        public MainPage()
        {
            this.InitializeComponent();
            Auth();
        }
        private async void Auth()
        {
            var localPath = ApplicationData.Current.LocalFolder.Path;
            var tokenFilePath = localPath + "\\" + "token.txt";
            var localFolder = await StorageFolder.GetFolderFromPathAsync(localPath);
            if (File.Exists(tokenFilePath))
            {
                 var tokenFile = await StorageFile.GetFileFromPathAsync(tokenFilePath);
                 yandexDiskWorker.DiskInformation.SetToken(await FileIO.ReadTextAsync(tokenFile));
                 yandexDiskWorker.DiskInformation.SetInformation(await yandexDiskWorker.YandexClient.GetStringAsync("https://cloud-api.yandex.net/v1/disk"));
                 var test = JObject.Parse(await yandexDiskWorker.YandexClient.GetStringAsync("https://cloud-api.yandex.net/v1/disk/resources?path=disk%3A%2F&limit=1"));
                 var items = JsonConvert.DeserializeObject<YDJsonFile>((test["_embedded"])["items"][0].ToString());

                try
                {
                    FileOpenPicker picker = new FileOpenPicker();
                    picker.FileTypeFilter.Add("*");
                    StorageFile file = await picker.PickSingleFileAsync();

                    var uploadInformation = JObject.Parse(await yandexDiskWorker.YandexClient.GetStringAsync(
                        new Uri("https://cloud-api.yandex.net/v1/disk/resources/upload?path=" + 
                        yandexDiskWorker.DiskInformation.DownloadsFolderPath + 
                        file.Name)));
                    if (uploadInformation.HasValues && uploadInformation.ContainsKey("href") && uploadInformation.ContainsKey("method"))
                    {
                        BackgroundUploader uploader = new BackgroundUploader();
                        uploader.Method = uploadInformation["method"].ToString();
                        uploader.SetRequestHeader("Authorization", "OAuth " + yandexDiskWorker.DiskInformation.Token);
                        share.UploadFile(uploader, file, new Uri(uploadInformation["href"].ToString()));
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }


                //share.UploadFile("disk://", null);
            }
            else
            {
                CoreApplicationView newView = CoreApplication.CreateNewView();
                int newViewId = 0;
                await newView.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => {
                    Frame frame = new Frame();
                    var page = new Page();
                    var webView = new WebView();
                    webView.DefaultBackgroundColor = Windows.UI.Color.FromArgb(255, 255, 255, 255);
                    page.Content = webView;
                    webView.Navigate(new Uri(YDInformation.TokenRequestString));
                    frame.Content = page;
                    Window.Current.Content = frame;
                    Window.Current.Activate();
                    newViewId = ApplicationView.GetForCurrentView().Id;
                    webView.LoadCompleted += async (s, e) => {
                    var path = webView.Source.ToString();
                        if (path.Contains("#access_token="))
                        {
                            page.Content = new TextBlock() { Text = "Авторизация завершена можете закрыть данное окно.",
                                HorizontalAlignment = HorizontalAlignment.Center,
                                VerticalAlignment = VerticalAlignment.Center,
                                TextWrapping = TextWrapping.Wrap
                            };
                            var tokenFile = await localFolder.CreateFileAsync("token.txt");
                            var result = yandexDiskWorker.DiskInformation.SetTokenAsUri(path);
                            await FileIO.WriteTextAsync(tokenFile, yandexDiskWorker.DiskInformation.Token);
                        }
                    };
                });
                bool viewShown = await ApplicationViewSwitcher.TryShowAsViewModeAsync(newViewId, ApplicationViewMode.CompactOverlay);
            }
        }
        private async void UploadMonitoring(UploadOperation operation)
        {
            
        }
   }
}
