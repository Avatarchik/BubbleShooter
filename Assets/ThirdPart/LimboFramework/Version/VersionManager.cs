using System.Threading.Tasks;
using LimboFramework.Net;
using LimboFramework.Singleton;
using LimboFramework.Utils;
using LitJson;
using UnityEngine;
using UnityEngine.Networking;

namespace LimboFramework.Version
{
    public class VersionManager : Singleton<VersionManager>
    {
        private RemoteConfigDescriptor _remoteConfigDescriptor;

        public string ForceUpdateUrl => _remoteConfigDescriptor.ForceUpdateServer;

        public async Task Init()
        {
            string path = $"{GameConfig.Instance.RemoteSettingUrl}/{PlatformHelper.GetPlatformString()}/GameConfig.json";
            await AsyncWebRequest.Load(path, InitRemoteConfig);
        }

        private void InitRemoteConfig(DownloadHandler handler)
        {
            _remoteConfigDescriptor = JsonMapper.ToObject<RemoteConfigDescriptor>(handler.text);
        }

        public bool NeedUpdatePackage()
        {
            return !string.Equals(Application.version, _remoteConfigDescriptor.GameVersion);
        }
    }
}