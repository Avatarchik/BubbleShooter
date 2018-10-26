using System.IO;

namespace LimboFramework.Utils
{
    public class DirectoryHelper
    {
        public static void Delete(string path, bool recursive)
        {
            if (Directory.Exists(path))
            {
                Directory.Delete(path, recursive);
            }
        }

        public static void EnsureExist(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}