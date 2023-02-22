using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TechFix.EntityModels
{
    public class Template
    {
        public Guid? Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Path { get; set; }

        [JsonIgnore]
        public Guid? CreatedUser { get; set; }

        [JsonIgnore]
        public DateTime? CreatedDate { get; set; }

        [JsonIgnore]
        public Guid? ModifiedUser { get; set; }

        [JsonIgnore]
        public DateTime? ModifiedDate { get; set; }

        [JsonIgnore]
        public bool IsDeleted { get; set; }
    }
}
