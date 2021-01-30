using System;
using System.Collections.Generic;
using System.Text;

namespace CloudsLibrary.REST
{
    public class CloudClient : RestClient
    {
        #region events
        /// <summary>
        /// Делегат для отправки или получения файлов
        /// </summary>
        /// <param name="serverPath">Путь к файлу в облаке</param>
        /// <param name="localPath">Путь к локальному файлу</param>
        public delegate void FileSharingD(string serverPath, string localPath);

        /// <summary>
        /// Событие которое будет инициировать выгрузку файла в вашей программе. 
        /// </summary>
        public event FileSharingD FileUpload;

        /// <summary>
        /// Событие которое будет инициировать загрузку файла в вашей программе. 
        /// </summary>
        public event FileSharingD FileDownload;

        /// <summary>
        /// Отправить файл на диск. 
        /// Отправляет комманду в нативный код программы для инициации выгрузки файла. 
        /// Для работы необходимо подписаться на событие FileUpload этого же класса.
        /// </summary>
        /// <param name="Path"></param>
        /// <param name="LocalFilePath"></param>
        #endregion

        #region EventsInvokers
        public void OnUpload(string serverPath, string localPath)
        {
            FileUpload?.Invoke(serverPath, localPath);
        }
        public void OnDownload(string serverPath, string localPath)
        {
            FileDownload?.Invoke(serverPath, localPath);
        }
        #endregion
    }
}
