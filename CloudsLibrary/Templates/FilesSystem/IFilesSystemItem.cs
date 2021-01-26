using System;
using System.Collections.Generic;
using System.Text;

namespace CloudsLibrary.Templates.FilesSystem
{
    public interface IFilesSystemItem
    {
        string Name { get; set; }
        string Path { get; set; }
    }
}
