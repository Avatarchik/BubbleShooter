using UnityEngine;

namespace LimboFramework.Utils
{
    public static class PathHelper
    {
        public static string GetLocalVersionFilePath()
        {
#if UNITY_STANDALONE || UNITY_STANDALONE_OSX
            return $"file://{Application.streamingAssetsPath}/{PlatformHelper.GetPlatformString()}/VersionInfo.json";
#endif
        }
    }
}