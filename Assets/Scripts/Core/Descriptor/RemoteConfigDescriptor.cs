using System.Collections.Generic;

namespace Core.Descriptor
{
    public sealed class RemoteConfigDescriptor
    {
        public List<ServerDescriptor> GameServres { get; set; }
        public List<ServerDescriptor> ResServres { get; set; }
        public string GameVersion { get; set; }
        public string ResVersion { get; set; }
        public string Platform { get; set; }
        public string ForceUpdateServer { get; set; }
    }
}