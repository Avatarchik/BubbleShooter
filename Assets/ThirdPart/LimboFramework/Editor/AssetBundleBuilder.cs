﻿using System.Collections.Generic;
using System.IO;
using LimboFramework.Bundle;
using LimboFramework.IO;
using LitJson;
using UnityEditor;
using UnityEngine;

namespace LimboFramework.Editor
{
    public static class AssetBundleBuilder
    {
        public static AssetBundleManifest BuildAssetBubdle(AssetBundleBuildDescriptor assetBundleBuildDescriptor)
        {
            string outputPath = assetBundleBuildDescriptor.OutputPath;

            AssetDatabase.RemoveUnusedAssetBundleNames();
            EditorUserBuildSettings.SwitchActiveBuildTarget(assetBundleBuildDescriptor.Group, assetBundleBuildDescriptor.Target);
            DirectoryHelper.EnsureExist(outputPath);
            return BuildPipeline.BuildAssetBundles(outputPath, assetBundleBuildDescriptor.Options, assetBundleBuildDescriptor.Target);
        }

        public static string BuildAssetsManifest(AssetBundleManifest manifest, FileDescriptor fileDescriptor)
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
