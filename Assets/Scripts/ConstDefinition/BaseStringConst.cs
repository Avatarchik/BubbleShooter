using UnityEngine;

namespace Const
{
    public static class BaseStringConst
    {
        public static readonly string ResourceManifestFileName = "resourceInfo.bytes";
        public static readonly string AssetBundlePath = $"{Application.dataPath}/../AssetBundles/";
        public static readonly string VersionInfoFileName = $"{Application.streamingAssetsPath}/VersionInfo.json";
    }
}
