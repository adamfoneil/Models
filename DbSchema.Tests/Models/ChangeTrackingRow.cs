using System;

namespace DbSchema.Tests.Models
{
    public class ChangeTrackingRow
    {
        public string Key { get; set; }
        public long Version { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
