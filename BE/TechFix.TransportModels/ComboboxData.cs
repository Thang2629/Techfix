using System;
using TechFix.Common.Interfaces;
using TechFix.EntityModels.Abstracts;

namespace TechFix.TransportModels
{
    public class ComboboxData : IMapFrom<ProductAssociated>
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
    }
}
