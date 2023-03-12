using System;

namespace TechFix.EntityModels.Configs
{
    public class SystemConfig
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public DateTime? ExpiredDate { get; set; }
    }
}
