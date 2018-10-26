using System;
using System.Collections.Generic;

namespace LimboFramework.IO.Loader
{
    public class QueueLoader : ILoader
    {
        private float _loadedCount;
        private float _totalLoadCount;

        private ILoader _curLoader;
        private readonly Queue<ILoader> _loaderQueue = new Queue<ILoader>();
        private readonly Dictionary<string, ILoader> _assetDic = new Dictionary<string, ILoader>();

        public event Action<float> OnProgress;
        public event Action<ILoader> OnComplete;
        public event Action<ILoader> OnError;

        public string Name { get; }

        /// <summary>
        /// The 'LoadedData' for QueueLoader is a 'Dictionary' whit 'string' key and 'Iloader' value
        /// </summary>
        public object LoadedData => _assetDic;

        public QueueLoader()
        {
        }

        public QueueLoader(string name)
        {
            Name = name;
        }

        public void Add(ILoader loader)
        {
            _loaderQueue.Enqueue(loader);
        }

        public void Add(List<ILoader> loaders)
        {
            if (null == loaders)
            {
                return;
            }

            for (int i = 0; i < loaders.Count; i++)
            {
                _loaderQueue.Enqueue(loaders[i]);
            }
        }

        public void Start()
        {
            _totalLoadCount = _loaderQueue.Count;
            StartLoad();
        }

        public void Update()
        {
            _curLoader?.Update();
        }

        public void Stop()
        {
            _curLoader?.Stop();
            _curLoader = null;
        }

        private void OnLoadComplete(ILoader loader)
        {
            if (null == loader)
            {
                HandleComplete();
                return;
            }

            loader.OnComplete -= OnLoadComplete;
            loader.OnError -= OnLoadError;
            loader.OnProgress -= OnLoadingProgress;

            CacheAsset(loader);
            StartLoad();
        }

        private void OnLoadError(ILoader loader)
        {
            OnError?.Invoke(this);
        }

        private void OnLoadingProgress(float progress)
        {
            OnProgress?.Invoke(++_loadedCount / _totalLoadCount);
        }

        private void StartLoad()
        {
            if (_loaderQueue.Count <= 0)
            {
                HandleComplete();
                return;
            }

            _curLoader = _loaderQueue.Dequeue();
            _curLoader.OnComplete += OnLoadComplete;
            _curLoader.OnError += OnLoadError;
            _curLoader.OnProgress += OnLoadingProgress;

            _curLoader.Start();
        }

        private void HandleComplete()
        {
            OnProgress?.Invoke(1.0f);
            OnComplete?.Invoke(this);
        }

        private void CacheAsset(ILoader loader)
        {
            if (_assetDic.ContainsKey(loader.Name))
            {
                _assetDic[loader.Name] = loader;
            }
            else
            {
                _assetDic.Add(loader.Name, loader);
            }
        }
    }
}