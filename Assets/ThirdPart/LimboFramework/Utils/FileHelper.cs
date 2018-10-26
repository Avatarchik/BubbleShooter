using System;
using System.IO;
using LimboFramework.AssetBundle;
using UnityEditor;

namespace LimboFramework.Utils
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
