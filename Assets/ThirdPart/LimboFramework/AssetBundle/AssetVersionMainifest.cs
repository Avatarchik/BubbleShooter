using System.Collections.Generic;

namespace LimboFramework.AssetBundles
{
    public class AssetVersionMainifest
    {
        public string Version { get; set; }
        public List<AssetDescriptor> AssetList { get; } = new List<AssetDescriptor>();
        
        public void Reset()
        {
            AssetList.Clear();
        }
    }
}