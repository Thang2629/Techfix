using TechFix.EntityModels.Abstracts;

namespace TechFix.EntityModels
{
    public class Store : ProductAssociated
    {
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
