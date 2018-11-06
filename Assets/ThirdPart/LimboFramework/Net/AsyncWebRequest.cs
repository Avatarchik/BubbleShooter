using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace LimboFramework.Net
{
    public static class AsyncWebRequest
    {
        public static async Task Load(string url, Action<DownloadHandler> completeAction)
        {
            using (UnityWebRequest www = UnityWebRequest.Get(url))
            {
                await www.SendWebRequest();

                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.LogError($"Load error {url}");
                    completeAction?.Invoke(null);
                    return;
                }

                completeAction?.Invoke(www.downloadHandler);
            }
        }
    }
}