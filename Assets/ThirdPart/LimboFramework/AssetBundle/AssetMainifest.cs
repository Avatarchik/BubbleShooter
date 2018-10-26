﻿using System.Collections.Generic;

namespace LimboFramework.AssetBundle
{
    public class AssetMainifest
    {
        public string Version { get; set; }
        public List<AssetDescriptor> AssetList { get; } = new List<AssetDescriptor>();
        
        public void Reset()
        {
            AssetList.Clear();
        }
    }
}