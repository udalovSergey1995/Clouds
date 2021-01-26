using System;
using System.Collections.Generic;
using System.Text;

namespace CloudsLibrary.Templates.FilesSystem
{
    public class FilesSystemItem : IFilesSystemItem
    {
        public bool IsFolder { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
    }
}
