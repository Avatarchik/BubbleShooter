using System.Text;
using Const;
using LimboFramework.Editor;
using LimboFramework.IO;
using UnityEditor;
using UnityEngine;

namespace Editor.Bundle
{
    /// <summary>
    /// 功能说明：
    /// </summary>
	public static class BundleHelper
    {
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

        private static void Build(BuildTargetGroup group, BuildTarget target)
        {
            string bundleOutputPath = $"{BaseStringConst.AssetBundlePath}{target.ToString()}";

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
                Name = BaseStringConst.ResourceManifestFileName
            };

            AssetBundleManifest manifest = AssetBundleBuilder.BuildAssetBubdle(buildDescriptor);
            string resourceInfo = AssetBundleBuilder.BuildAssetsManifest(manifest, fileDescriptor);
            byte[] encryptBytes = ByteHelper.DeOrEncrypt(Encoding.UTF8.GetBytes(resourceInfo));
            FileHelper.WriteBytes(fileDescriptor.Path, fileDescriptor.Name, encryptBytes);
        }
    }
}
