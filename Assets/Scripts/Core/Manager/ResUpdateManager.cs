using System;
using System.Threading.Tasks;
using LimboFramework.Singleton;

namespace Core.Manager
{
    public class ResUpdateManager : Singleton<ResUpdateManager>
    {
        public async Task Start()
        {
            bool needUpdate = VersionManager.Instance.NeedUpdateResource();
            if (!needUpdate)
            {
                return;
            }


        }
    }
}