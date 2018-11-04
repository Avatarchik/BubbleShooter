using System.Threading.Tasks;
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
                Debug.Log(VersionManager.Instance.ForceUpdateUrl);
                return;
            }

            await ResUpdateManager.Instance.Start();
        }
    }
}