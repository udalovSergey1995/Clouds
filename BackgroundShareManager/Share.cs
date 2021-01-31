using CloudsLibrary.System;
using System;
using System.Threading.Tasks;
using Windows.Networking.BackgroundTransfer;
using Windows.Storage;

namespace BackgroundShareManager
{
    public sealed class Share
    {
        public void UploadFile(BackgroundUploader uploader, StorageFile file, Uri url)
        {
            try
            {
                UploadOperation upload = uploader.CreateUpload(url, file);
                upload.StartAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DownloadFile()
        {

        }
    }
}
