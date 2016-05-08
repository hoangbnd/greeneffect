using MVCCore.Configuration;

namespace MVCCore.IO
{
    public class FileSystemSettings : ISettings
    {
        public string DirectoryName { get; set; }
    }
}