using System.Collections.Generic;

namespace LimboFramework.Bundle
{
    public sealed class AssetVersionMainifest
    {
        public string Version { get; set; }
        public List<AssetDescriptor> AssetList { get; } = new List<AssetDescriptor>();
        
        public void Reset()
        {
            AssetList.Clear();
        }
    }
}