using System;
using Newtonsoft.Json;

namespace TechFix.EntityModels
{
    public class BaseModel
    {
        public Guid? Id { get; set; }

        [JsonIgnore]
        public Guid? CreatedUser { get; set; }

        [JsonIgnore]
        public DateTime? CreatedDate { get; set; }

        [JsonIgnore]
        public Guid? ModifiedUser { get; set; }

        [JsonIgnore]
        public DateTime? ModifiedDate { get; set; }

        [JsonIgnore]
        public string SearchData { get; set; }

        [JsonIgnore]
        public bool IsDeleted { get; set; }
    }
}
