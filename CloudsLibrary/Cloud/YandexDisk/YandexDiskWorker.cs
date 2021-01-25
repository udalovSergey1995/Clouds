using CloudsLibrary.Cloud.YandexDisk.System;
using CloudsLibrary.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudsLibrary.Cloud.YandexDisk
{
    public class YandexDiskWorker
    {
        public YandexClient YandexClient { get; set; } = new YandexClient();
        public YandexDiskWorker()
        {
            YandexClient.DefaultRequestHeaders.Add("Authorization", "OAuth " + YDInformation.Token);
        }
    }
}
