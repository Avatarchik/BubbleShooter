using Core.Manager;
using LimboFramework.Loader;
using LimboFramework.Utils;
using LimboFramework.Version;
using UnityEngine;

namespace Game
{
    public class GameEntry : MonoBehaviour
    {
        private WebLoader webLoader;
        async void Start()
        {
            await ResUpdateManager.Instance.Start();
        }

        void Update()
        {
            webLoader?.Update();
        }
    }
}