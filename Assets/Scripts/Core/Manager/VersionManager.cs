using System.Threading.Tasks;
using Const;
using Core.Descriptor;
using LimboFramework.Net;
using LimboFramework.Singleton;
using LitJson;
using UnityEngine;
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
            await WebRequest.Load(BaseStringConst.VersionInfoFileName, InitLocalConfig);
            await WebRequest.Load(_versionDescriptor.ConfigUrl, InitRemoteConfig);
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

        public bool NeedUpdateResource()
        {
            return !string.Equals(_versionDescriptor.ResVersion, _remoteConfigDescriptor.ResVersion);
        }
    }
}