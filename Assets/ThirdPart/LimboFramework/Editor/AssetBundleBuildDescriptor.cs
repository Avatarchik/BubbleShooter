using LimboFramework.AssetBundle;
using UnityEditor;

namespace LimboFramework.Editor
{
    public class AssetBundleBuildDescriptor
    {
        public BuildTargetGroup Group { get; set; }
        public BuildTarget Target { get; set; }
        public BuildAssetBundleOptions Options { get; set; }
        public string OutputPath { get; set; }
    }
}
