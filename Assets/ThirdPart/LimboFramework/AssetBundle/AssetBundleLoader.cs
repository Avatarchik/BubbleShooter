using System;
using UnityEngine;
using LimboFramework.IO.Loader;

namespace LimboFramework.AssetBundles
{
    public class AssetBundleLoader : ILoader
    {
        private readonly string _assetName;
        private readonly int _hashCode;
        private AssetBundleCreateRequest _assetBundleRequest;
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
            _assetBundleRequest = AssetBundle.LoadFromFileAsync(_assetName);
        }

        public void Update()
        {
            if (null == _assetBundleRequest)
            {
                return;
            }

            if (_assetBundleRequest.isDone)
            {
                _loadedBundleAsset = _assetBundleRequest.assetBundle;
                OnComplete?.Invoke(this);
                Stop();
                return;
            }

            OnProgress?.Invoke(_assetBundleRequest.progress);
        }

        public void Stop()
        {
            _assetBundleRequest = null;
        }
    }
}