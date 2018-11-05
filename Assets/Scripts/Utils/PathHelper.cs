using LimboFramework.Utils;
using UnityEngine;

namespace Game.Utils
{
    public static class PathHelper
    {
        public static string GetLocalVersionFilePath()
        {
#if UNITY_STANDALONE || UNITY_O
            return $"file://{Application.streamingAssetsPath}/{PlatformHelper.GetPlatformString()}/VersionInfo.json";
#endif
        }
    }
}