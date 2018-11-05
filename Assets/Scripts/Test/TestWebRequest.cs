using System.Collections.Generic;
using LitJson;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.Test
{
    public class TestWebRequest : MonoBehaviour
    {
        async void Start()
        {
            await LimboFramework.Net.AsyncWebRequest.Load("http://limbostudio.bj.bcebos.com/bubble/GameConfig.json", Callback);
            await LimboFramework.Net.AsyncWebRequest.Load($"{Application.streamingAssetsPath}/VersionInfo.json", Callback);
        }

        private void Callback(DownloadHandler obj)
        {
            Debug.Log(JsonUtility.ToJson(obj.text));
            //RemoteConfig config = JsonMapper.ToObject<RemoteConfig>(obj.text);
            //Debug.Log(JsonMapper.ToJson(config));
        }
    }

    public class RemoteConfig
    {
        public List<Server> GameServres { get; set; }
        public List<Server> ResServres { get; set; }
        public string GameVersion { get; set; }
        public string ResVersion { get; set; }
    }

    public class Server
    {
        public int Id { get; set; }
        public int Port { get; set; }
        public int Status { get; set; }
        public int Platform { get; set; }
        public string Name { get; set; }
        public string Ip { get; set; }
    }
}