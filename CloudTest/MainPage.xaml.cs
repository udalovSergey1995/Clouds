using CloudsLibrary.Cloud.YandexDisk;
using CloudsLibrary.Cloud.YandexDisk.System;
using CloudsLibrary.REST;
using CloudsLibrary.System;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Security.Authentication.Web;
using Windows.Storage;
using Windows.UI.ViewManagement;
using Windows.UI.WindowManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace CloudTest
{
   public sealed partial class MainPage : Page
   {
        YandexDiskWorker yandexDiskWorker;
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
                Information.YDInformation.SetToken(await FileIO.ReadTextAsync(tokenFile));
                yandexDiskWorker = new YandexDiskWorker();
                Information.YDInformation.SetInformation(await yandexDiskWorker.YandexClient.GetStringAsync("https://cloud-api.yandex.net/v1/disk"));
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
                           var result = Information.YDInformation.SetTokenAsUri(path);
                           await FileIO.WriteTextAsync(tokenFile, YDInformation.Token);
                       }
                   };
               });
               bool viewShown = await ApplicationViewSwitcher.TryShowAsViewModeAsync(newViewId, ApplicationViewMode.CompactOverlay);
           }
       }
   }
}
