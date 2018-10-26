using System.IO;

namespace LimboFramework.IO
{
    public static class FileHelper
    {
        public static void WriteBytes(string outputPath, string name, byte[] bytes)
        {
            DirectoryHelper.EnsureExist(outputPath);
            File.WriteAllBytes($"{outputPath}/{name}", bytes);
        }
    }
}
