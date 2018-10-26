using System;

namespace LimboFramework.IO.Loader
{
    public interface ILoader
    {
        string Name { get; }
        object LoadedData { get; }

        event Action<float> OnProgress;
        event Action<ILoader> OnComplete;
        event Action<ILoader> OnError;

        void Start();
        void Update();
        void Stop();
    }
}