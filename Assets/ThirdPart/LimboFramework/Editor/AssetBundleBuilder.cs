using System.Collections.Generic;
using System.IO;
using System.Text;
using LimboFramework.Bundle;
using LimboFramework.IO;
using LitJson;
using UnityEditor;
using UnityEngine;

namespace LimboFramework.Editor
{
    public static class AssetBundleBuilder
    {
        private class AssetBundleBuildDescriptor
        {
            public BuildTargetGroup Group { get; set; }
            public BuildTarget Target { get; set; }
            public BuildAssetBundleOptions Options { get; set; }
            public string OutputPath { get; set; }
        }

        private static readonly string ResourceManifestFileName = "resourceInfo.bytes";
        private static readonly string AssetBundlePath = $"{Application.dataPath}/../AssetBundles/";

        [MenuItem("Tools/Bundle/PC")]
        public static void BuildForWindows()
        {
            Build(BuildTargetGroup.Standalone, BuildTarget.StandaloneWindows);
        }

        [MenuItem("Tools/Bundle/Mac")]
        public static void BuildForOSX()
        {
            Build(BuildTargetGroup.Standalone, BuildTarget.StandaloneOSX);
        }

        [MenuItem("Tools/Bundle/Android")]
        public static void BuildForAndroid()
        {
            Build(BuildTargetGroup.Android, BuildTarget.Android);
        }

        [MenuItem("Tools/Bundle/Mac")]
        public static void BuildForIOS()
        {
            Build(BuildTargetGroup.iOS, BuildTarget.iOS);
        }

        private static void Build(BuildTargetGroup group, BuildTarget target)
        {
            string bundleOutputPath = $"{AssetBundlePath}{target.ToString()}";

            AssetBundleBuildDescriptor buildDescriptor = new AssetBundleBuildDescriptor
            {
                Group = group,
                Options = BuildAssetBundleOptions.ChunkBasedCompression,
                Target = target,
                OutputPath = bundleOutputPath
            };

            FileDescriptor fileDescriptor = new FileDescriptor
            {
                Path = bundleOutputPath,
                Name = ResourceManifestFileName
            };

            AssetBundleManifest manifest = AssetBundleBuilder.BuildAssetBubdle(buildDescriptor);
            string resourceInfo = AssetBundleBuilder.BuildAssetsManifest(manifest, fileDescriptor);
            byte[] encryptBytes = ByteHelper.DeOrEncrypt(Encoding.UTF8.GetBytes(resourceInfo));
            FileHelper.WriteBytes(fileDescriptor.Path, fileDescriptor.Name, encryptBytes);
        }

        private static AssetBundleManifest BuildAssetBubdle(AssetBundleBuildDescriptor assetBundleBuildDescriptor)
        {
            string outputPath = assetBundleBuildDescriptor.OutputPath;

            AssetDatabase.RemoveUnusedAssetBundleNames();
            EditorUserBuildSettings.SwitchActiveBuildTarget(assetBundleBuildDescriptor.Group, assetBundleBuildDescriptor.Target);
            DirectoryHelper.EnsureExist(outputPath);
            return BuildPipeline.BuildAssetBundles(outputPath, assetBundleBuildDescriptor.Options, assetBundleBuildDescriptor.Target);
        }

        private static string BuildAssetsManifest(AssetBundleManifest manifest, FileDescriptor fileDescriptor)
        {
            AssetVersionMainifest assetMainifest = new AssetVersionMainifest ();

            Dictionary<string, AssetDescriptor> bundleList = new Dictionary<string, AssetDescriptor>();
            DirectoryInfo directoryInfo = new DirectoryInfo(fileDescriptor.Path);
            foreach (var info in directoryInfo.GetFiles())
            {
                if (Path.GetExtension(info.Name) == $".{Path.GetExtension(fileDescriptor.Path)}")
                {
                    AssetDescriptor data = new AssetDescriptor
                    {
                        Name = info.Name,
                        HashCode = manifest.GetAssetBundleHash(info.Name).GetHashCode(),
                        Size = Mathf.CeilToInt(info.Length / 1024f)
                    };
                    bundleList.Add(info.Name, data);
                }
            }

            foreach (var key in bundleList.Keys)
            {
                assetMainifest.AssetList.Add(bundleList[key]);
            }

            return JsonMapper.ToJson(assetMainifest);
        }
    }
}
