using System.IO;

namespace MVCCore
{
    public static class Folder
    {
        public static void CreateFolder(string nameFolder, string dir)
        {
            string folder = dir + "\\" + nameFolder;
            if (Directory.Exists(folder))
            {
                return;
            }
            Directory.CreateDirectory(folder);
        }
    }
}
