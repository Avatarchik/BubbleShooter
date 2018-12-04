using Core.Manager;
using LimboFramework.Version;
using UnityEngine;

namespace Game
{
    public class GameEntry : MonoBehaviour
    {
        async void Start()
        {
            await VersionManager.Instance.Init();
            //GameConfig.Instance.Log();

            //bool needForceUpdate = VersionManager.Instance.NeedUpdatePackage();
            //if (needForceUpdate)
            //{
            //    //TODO 强制更新
            //    return;
            //}

            await ResUpdateManager.Instance.Start();
        }
    }
}