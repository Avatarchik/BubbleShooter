namespace Core.Descriptor
{
    public sealed class VersionDescriptor
    {
        public int BuildVersionCode { get; set; }
        public string ConfigUrl { get; set; }
        public string GameVersion { get; set; }
        public string ResVersion { get; set; }
        public string BundleIdentifer { get; set; }
    }
}