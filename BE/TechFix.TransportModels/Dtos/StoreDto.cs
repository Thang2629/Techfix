using System;
using TechFix.Common.Interfaces;
using TechFix.EntityModels;

namespace TechFix.TransportModels.Dtos
{
    public class StoreDto : IMapFrom<Store>
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string SearchData { get; set; }
    }
}
