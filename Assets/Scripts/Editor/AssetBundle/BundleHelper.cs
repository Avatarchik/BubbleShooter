using System.Text;
using LimboFramework.AssetBundle;
using LimboFramework.Editor;
using LimboFramework.Utils;
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
            string bundleOutputPath = $"{Application.dataPath}/../AssetBundles/{BuildTarget.StandaloneWindows.ToString()}";

            AssetBundleBuildDescriptor buildDescriptor = new AssetBundleBuildDescriptor
            {
                Group = BuildTargetGroup.Standalone,
                Options = BuildAssetBundleOptions.ChunkBasedCompression,
                Target = BuildTarget.StandaloneWindows,
                OutputPath = bundleOutputPath
            };

            OutputDescriptor outputDescriptor = new OutputDescriptor
            {
                Path = bundleOutputPath,
                Name = "resourceInfo.bytes"
            };

            AssetBundleManifest manifest = BundleBuilder.Build(buildDescriptor);
            string resourceInfo = BundleBuilder.BuildManifest(manifest, outputDescriptor);
            byte[] encryptBytes = ByteHelper.DeOrEncrypt(Encoding.UTF8.GetBytes(resourceInfo));
            FileHelper.WriteBytes(outputDescriptor.Path, outputDescriptor.Name, encryptBytes);
        }
    }
}
