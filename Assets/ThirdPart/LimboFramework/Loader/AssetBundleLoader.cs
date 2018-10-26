using System;
using UnityEngine;

namespace LimboFramework.Loader
{
    public class AssetBundleLoader : ILoader
    {
        private readonly string _assetName;
        private readonly int _hashCode;
        private WWW _donwloader;
        private UnityEngine.AssetBundle _loadedBundleAsset;

        public event Action<float> OnProgress;
        public event Action<ILoader> OnComplete;
        public event Action<ILoader> OnError;

        public string Name => _assetName;

        /// <summary>
        /// The 'LoadedData' for AssetBundleLoader is 'AssetBundle'
        /// </summary>
        public object LoadedData => _loadedBundleAsset;

        public AssetBundleLoader(string assetName)
        {
            _assetName = assetName;
        }

        public AssetBundleLoader(string assetName, int hashCode) : this(assetName)
        {
            _hashCode = hashCode;
        }

        public AssetBundleLoader(string assetName, Hash128 hashCode) : this(assetName, hashCode.GetHashCode())
        {

        }

        public void Start()
        {
            string url = _assetName.Contains("://") ? _assetName : $"file:///{_assetName}";
            _donwloader = WWW.LoadFromCacheOrDownload(url, _hashCode);
        }

        public void Update()
        {
            if (null == _donwloader)
            {
                return;
            }

            if (!string.IsNullOrEmpty(_donwloader.error))
            {
                Stop();
                OnError?.Invoke(this);
                return;
            }

            if (_donwloader.isDone)
            {
                _loadedBundleAsset = _donwloader.assetBundle;
                OnComplete?.Invoke(this);
                Stop();
            }
            else
            {
                OnProgress?.Invoke(_donwloader.progress);
            }
        }

        public void Stop()
        {
            _donwloader?.Dispose();
            _donwloader = null;
        }
    }
}