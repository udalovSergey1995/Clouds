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
                //Если токен не доступен вызываем исключение
                if (DiskInformation.IsTokenRecivied)
                {
                    throw new Exception("Отсутствует токен");
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
                //Добавляем соответствующий заголовок в REST клиентдиска
                YandexClient.DefaultRequestHeaders.Add("Authorization", "OAuth " + DiskInformation.Token);
            };
        }
    }
}
