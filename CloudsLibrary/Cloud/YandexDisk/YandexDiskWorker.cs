using CloudsLibrary.Cloud.YandexDisk.System;
using CloudsLibrary.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudsLibrary.Cloud.YandexDisk
{
    /// <summary>
    /// Класс для работы с диском
    /// </summary>
    public class YandexDiskWorker
    {
        /// <summary>
        /// Информация о диске.
        /// </summary>
        public YDInformation DiskInformation { get; set; } = new YDInformation();
        /// <summary>
        /// Объект для обмена информацией с диском
        /// </summary>
        public YandexClient YandexClient {
            get {
                if (_yandexClient == null)
                {
                    throw new Exception("Клиент еще не создан. Необходимо получить токен для работы с облаком");
                }
                return _yandexClient;
            }
            set {
                _yandexClient = value;
            }
        }

        private YandexClient _yandexClient = new YandexClient();

        public YandexDiskWorker()
        {
            //Подписываемся на событие получения токена что бы в нужное время создать объект клиента 
            //и разрешить взаимодействие с облаком
            DiskInformation.TokenReceived += () => {
                YandexClient.DefaultRequestHeaders.Add("Authorization", "OAuth " + YDInformation.Token);
            };
        }
    }
}
