using System;
using UnityEngine;

namespace LimboFramework.IO.Loader
{
    public class AssetBundleLoader : ILoader
    {
        private readonly string _assetName;
        private readonly int _hashCode;
        private AssetBundleCreateRequest _assetBundleCreateRequest;
        private AssetBundle _loadedBundleAsset;

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
            _assetBundleCreateRequest = AssetBundle.LoadFromFileAsync(_assetName);
        }

        public void Update()
        {
            if (null == _assetBundleCreateRequest)
            {
                return;
            }

            if (_assetBundleCreateRequest.isDone)
            {
                _loadedBundleAsset = _assetBundleCreateRequest.assetBundle;
                OnComplete?.Invoke(this);
                Stop();
                return;
            }

            OnProgress?.Invoke(_assetBundleCreateRequest.progress);
        }

        public void Stop()
        {
            _assetBundleCreateRequest = null;
        }
    }
}