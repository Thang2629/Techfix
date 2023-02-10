using System;
using Newtonsoft.Json;

namespace TechFix.EntityModels
{
    public class BaseModel
    {
        public Guid? CreatedUser { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? ModifiedUser { get; set; }
        public DateTime? ModifiedDate { get; set; }

        [JsonIgnore]
        public string SearchData { get; set; }
    }
}
