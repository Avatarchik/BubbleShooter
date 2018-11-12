using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using LimboFramework.Bundle;
using LimboFramework.Net;
using LimboFramework.Singleton;
using LimboFramework.Utils;
using LimboFramework.Version;
using LitJson;
using UnityEngine;

namespace Core.Manager
{
    public class ResUpdateManager : Singleton<ResUpdateManager>
    {
        private const string ResourceName = "resourceInfo.bytes";

        private AssetMainifest _localAssetMainifest;
        private AssetMainifest _remoteAssetMainifest;

        public async Task Start()
        {
            await InitManifest();
            await GenerateDownloadMenifest();
        }

        private void Dispose()
        {
            _localAssetMainifest = null;
            _remoteAssetMainifest = null;
        }

        private async Task InitManifest()
        {
            string path = $"{Application.persistentDataPath}/{ResourceName}";
            if (Directory.Exists(path))
            {
                _localAssetMainifest = await LoadManifest(path);
            }
            else
            {
                TextAsset resourceAsset = Resources.Load<TextAsset>(Path.GetFileNameWithoutExtension(ResourceName));
                _localAssetMainifest = ParseFromJson(resourceAsset.bytes);
            }

            string remoteUrl = $"{GameConfig.Instance.RemoteSettingUrl}/{PlatformHelper.GetPlatformString()}/{ResourceName}";
            _remoteAssetMainifest = await LoadManifest(remoteUrl);
        }

        private async Task<AssetMainifest> LoadManifest(string url)
        {
            AssetMainifest mainifest = null;
            await AsyncWebRequest.Load(url, downloadHandler =>
            {
                Debug.Log(url);
                mainifest = ParseFromJson(downloadHandler.data);
            });
            return mainifest;
        }

        private AssetMainifest ParseFromJson(byte[] loadedBytes)
        {
            string jsonStr = Encoding.UTF8.GetString(loadedBytes);
            return JsonMapper.ToObject<AssetMainifest>(jsonStr);
        }

        private async Task GenerateDownloadMenifest()
        {
            //Dictionary<string, DownloadAssetDescriptor> assets = new Dictionary<string, DownloadAssetDescriptor>(32);
            //Dictionary<string, DownloadAssetDescriptor> oldAssets = new Dictionary<string, DownloadAssetDescriptor>(32);
            //Dictionary<string, DownloadAssetDescriptor> dirtyAssets = new Dictionary<string, DownloadAssetDescriptor>(32);

            //if (null != _remoteAssetMainifest)
            //{
            //    foreach (var bundle in _remoteAssetMainifest.AssetList)
            //    {
            //        assets[bundle.Name] = bundle;
            //    }
            //}

            //foreach (var bundle in _localAssetMainifest.AssetList)
            //{
            //    bool isFileExist = File.Exists(Application.persistentDataPath + "/" + bundle.Name);
            //    AssetDescriptor newBundle = assets.ContainsKey(bundle.Name) ? assets[bundle.Name] : null;
            //    if (!isFileExist || null != newBundle && bundle.HashCode != newBundle.HashCode)
            //    {
            //        dirtyAssets.Add(bundle.Name, bundle);
            //    }
            //}

            //foreach (var bundle in _localAssetMainifest.AssetList)
            //{
            //    oldAssets[bundle.Name] = bundle;
            //}
        }
    }
}