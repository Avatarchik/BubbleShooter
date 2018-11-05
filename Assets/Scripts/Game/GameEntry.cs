using Core.Manager;
using UnityEngine;

namespace Game
{
    public class GameEntry : MonoBehaviour
    {
        async void Start()
        {
            await VersionManager.Instance.Init();

            bool needForceUpdate = VersionManager.Instance.NeedUpdatePackage();
            if (needForceUpdate)
            {
                //TODO 强制更新
                return;
            }

            await ResUpdateManager.Instance.Start();
        }
    }
}