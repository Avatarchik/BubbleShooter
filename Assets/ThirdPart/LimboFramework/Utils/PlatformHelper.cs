using UnityEngine;

namespace LimboFramework.Utils
{
    public static class PlatformHelper
    {
        public static string GetPlatformString()
        {
            switch (Application.platform)
            {
                case RuntimePlatform.WindowsEditor:
                case RuntimePlatform.WindowsPlayer:
                    return "StandaloneWindows";
                case RuntimePlatform.OSXPlayer:
                case RuntimePlatform.OSXEditor:
                    return "StandaloneOSX";
                case RuntimePlatform.IPhonePlayer:
                    return "iOS";
                default:
                    return Application.platform.ToString();
            }
        }

        public static string GetPlatformStreamingAssetPath()
        {
#if UNITY_IPHONE
            return $"file://{Application.dataPath}/Raw/iOS/";
#elif UNITY_STANDALONE_OSX
            return $"file://{Application.streamingAssetsPath}/{GetPlatformString()}/";
#endif
            return $"{Application.streamingAssetsPath}/{GetPlatformString()}/";
        }
    }
}