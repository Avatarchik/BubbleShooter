namespace Core.Descriptor
{
    public sealed class ServerDescriptor
    {
        public int Id { get; set; }
        public int Port { get; set; }
        public int Status { get; set; }
        public int Platform { get; set; }
        public string Name { get; set; }
        public string Ip { get; set; }
    }
}