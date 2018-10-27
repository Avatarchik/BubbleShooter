using System.Text;
using LimboFramework.Editor;
using LimboFramework.IO;
using UnityEditor;
using UnityEngine;

namespace Editor.AssetBundle
{
    /// <summary>
    /// 功能说明：
    /// </summary>
	public class BundleHelper
    {
        [MenuItem("Test/Bundle/PC")]
        public static void BuildBundleForWindows()
        {
            string bundleOutputPath = $"{Application.dataPath}/../AssetBundles/{BuildTarget.StandaloneOSX.ToString()}";

            AssetBundleBuildDescriptor buildDescriptor = new AssetBundleBuildDescriptor
            {
                Group = BuildTargetGroup.Standalone,
                Options = BuildAssetBundleOptions.ChunkBasedCompression,
                Target = BuildTarget.StandaloneOSX,
                OutputPath = bundleOutputPath
            };

            FileOutputDescriptor fileOutputDescriptor = new FileOutputDescriptor
            {
                Path = bundleOutputPath,
                Name = "resourceInfo.bytes"
            };

            AssetBundleManifest manifest = AssetBundleBuilder.BuildAssetBubdle(buildDescriptor);
            string resourceInfo = AssetBundleBuilder.BuildAssetsManifest(manifest, fileOutputDescriptor);
            byte[] encryptBytes = ByteHelper.DeOrEncrypt(Encoding.UTF8.GetBytes(resourceInfo));
            FileHelper.WriteBytes(fileOutputDescriptor.Path, fileOutputDescriptor.Name, encryptBytes);
        }
    }
}
