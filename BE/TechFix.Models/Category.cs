using TechFix.EntityModels.Abstracts;

namespace TechFix.EntityModels
{
    public class Category : ProductAssociated 
    {
        public string Path { get; set; }
    }
}
