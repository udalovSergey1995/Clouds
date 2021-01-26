using System;
using System.Collections.Generic;
using System.Text;

namespace CloudsLibrary.Cloud.YandexDisk.FilesSystem
{
    public class YDJsonFile
    {
        public string public_key { get; set; }
        public string name { get; set; }
        public Exif exif { get; set; }
        public string resource_id { get; set; }
        public string public_url { get; set; }
        public DateTime modified { get; set; }
        public DateTime created { get; set; }
        public string path { get; set; }
        public Comment_Ids comment_ids { get; set; }
        public string type { get; set; }
        public long revision { get; set; }
    }
    public class Comment_Ids
    {
        public string private_resource { get; set; }
        public string public_resource { get; set; }
    }
    public class Exif
    {    }

}
