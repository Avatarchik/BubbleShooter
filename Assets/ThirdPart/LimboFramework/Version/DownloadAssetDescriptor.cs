using LimboFramework.Bundle;

namespace LimboFramework.Version
{
    public enum AssetOption
    {
        None,
        Delete,
        Add
    }

    public class DownloadAssetDescriptor
    {
        public AssetDescriptor Asset { get; set; }
        public AssetOption Option { get; set; }
    }
}