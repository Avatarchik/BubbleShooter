using System.Threading.Tasks;
using Const;
using Core.Descriptor;
using Game.Utils;
using LimboFramework.Net;
using LimboFramework.Singleton;
using LimboFramework.Utils;
using LitJson;
using UnityEngine.Networking;

namespace Core.Manager
{
    public class VersionManager : Singleton<VersionManager>
    {
        private VersionDescriptor _versionDescriptor;
        private RemoteConfigDescriptor _remoteConfigDescriptor;

        public string ForceUpdateUrl => _remoteConfigDescriptor.ForceUpdateServer;

        public async Task Init()
        {
            await AsyncWebRequest.Load(PathHelper.GetLocalVersionFilePath(), InitLocalConfig);
            await AsyncWebRequest.Load(_versionDescriptor.ConfigUrl, InitRemoteConfig);
        }

        private void InitLocalConfig(DownloadHandler handler)
        {
            _versionDescriptor = JsonMapper.ToObject<VersionDescriptor>(handler.text);
        }

        private void InitRemoteConfig(DownloadHandler handler)
        {
            _remoteConfigDescriptor = JsonMapper.ToObject<RemoteConfigDescriptor>(handler.text);
        }

        public bool NeedUpdatePackage()
        {
            return !string.Equals(_versionDescriptor.GameVersion, _remoteConfigDescriptor.GameVersion);
        }
    }
}