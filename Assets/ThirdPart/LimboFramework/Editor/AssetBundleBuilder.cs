using System.Collections.Generic;
using System.IO;
using LimboFramework.AssetBundle;
using LimboFramework.IO;
using LimboFramework.Utils;
using LitJson;
using UnityEditor;
using UnityEngine;

namespace LimboFramework.Editor
{
    public class AssetBundleBuilder
    {
        public static AssetBundleManifest BuildAssetBubdle(AssetBundleBuildDescriptor assetBundleBuildDescriptor)
        {
            string outputPath = assetBundleBuildDescriptor.OutputPath;

            AssetDatabase.RemoveUnusedAssetBundleNames();
            EditorUserBuildSettings.SwitchActiveBuildTarget(assetBundleBuildDescriptor.Group, assetBundleBuildDescriptor.Target);
            DirectoryHelper.EnsureExist(outputPath);
            return BuildPipeline.BuildAssetBundles(outputPath, assetBundleBuildDescriptor.Options, assetBundleBuildDescriptor.Target);
        }

        public static string BuildAssetsManifest(AssetBundleManifest manifest, FileOutputDescriptor fileOutputDescriptor)
        {
            AssetMainifest assetMainifest = new AssetMainifest();

            Dictionary<string, AssetDescriptor> bundleList = new Dictionary<string, AssetDescriptor>();
            DirectoryInfo directoryInfo = new DirectoryInfo(fileOutputDescriptor.Path);
            foreach (var info in directoryInfo.GetFiles())
            {
                if (Path.GetExtension(info.Name) == $".{Path.GetExtension(fileOutputDescriptor.Path)}")
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
