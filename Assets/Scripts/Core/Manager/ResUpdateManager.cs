using System;
using System.Threading.Tasks;
using LimboFramework.Singleton;

namespace Core.Manager
{
    public class ResUpdateManager : Singleton<ResUpdateManager>
    {
        public async Task<bool> Start()
        {
            return false;
        }

    }
}