using System;
using UnityEngine.Networking;

namespace LimboFramework.Loader
{
    public class WebLoader : ILoader
    {
        private readonly string _requestUrl;
        private UnityWebRequest _request;

        public string Name { get; }
        /// <summary>
        /// The 'LoadedData' for AssetBundleLoader is 'DownloadHandler'
        /// </summary>
        public object LoadedData { get; private set; }
        public event Action<float> OnProgress;
        public event Action<ILoader> OnComplete;
        public event Action<ILoader> OnError;

        public WebLoader(string requestUrl)
        {
            _requestUrl = requestUrl;
        }

        public void Start()
        {
            _request = UnityWebRequest.Get(_requestUrl);
            _request.SendWebRequest();
        }

        public void Update()
        {
            if (null == _request)
            {
                return;
            }

            if (!string.IsNullOrEmpty(_request.error))
            {
                OnError?.Invoke(this);
                Stop();
                return;
            }

            if (_request.isDone)
            {
                LoadedData = _request.downloadHandler;
                OnComplete?.Invoke(this);
                Stop();
                return;
            }

            OnProgress?.Invoke(_request.downloadProgress);
        }

        public void Stop()
        {
            _request?.Dispose();
            _request = null;
        }
    }
}