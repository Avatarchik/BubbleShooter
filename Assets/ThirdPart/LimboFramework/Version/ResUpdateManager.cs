using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using LimboFramework.Bundle;
using LimboFramework.IO;
using LimboFramework.Net;
using LimboFramework.Singleton;
using LitJson;
using UnityEngine;
using UnityEngine.Networking;

namespace Core.Manager
{
    public class ResUpdateManager : Singleton<ResUpdateManager>
    {
        private const string ResourceName = "resourceInfo.bytes";

        public async Task Start()
        {
            string path = $"{Application.persistentDataPath}/{ResourceName}";
            if (Directory.Exists(path))
            {
                await AsyncWebRequest.Load(path, HandleLoadLocalResourceMainfest);
            }
            else
            {
                TextAsset resourceAsset = Resources.Load<TextAsset>(Path.GetFileNameWithoutExtension(ResourceName));
                GeneratAssetManifest(resourceAsset.bytes);
            }
        }

        private void HandleLoadLocalResourceMainfest(DownloadHandler downloadHandler)
        {
            if (null == downloadHandler)
            {
                throw new Exception(
                    "Load resourceInfo form persistentDataPath or streamingAssetsPath error: file not exist");
            }

            GeneratAssetManifest(downloadHandler.data);
        }

        private void GeneratAssetManifest(byte[] loadedBytes)
        {
            AssetVersionMainifest assetMainifest = JsonMapper.ToObject<AssetVersionMainifest>(Encoding.UTF8.GetString(loadedBytes));
            Debug.Log(JsonMapper.ToJson(assetMainifest));
        }
    }
}