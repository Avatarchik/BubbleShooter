using System.Collections.Generic;

namespace LimboFramework.Bundle
{
    public sealed class AssetMainifest
    {
        public string Version { get; set; }

        //Make the set as public for JsonMapper to init it
        public List<AssetDescriptor> AssetList { get; set; } = new List<AssetDescriptor>();
    }
}