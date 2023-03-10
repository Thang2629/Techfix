using System;
using TechFix.EntityModels.Abstracts;

namespace TechFix.EntityModels
{
    public class MoneyOutHistory : MoneyHistory
    {
        #region FK

        public Guid? SupplierId { get; set; }
        public Supplier Supplier { get; set; }

        public Guid? InputProductId { get; set; }
        public InputProduct InputProduct { get; set; }

        #endregion
    }
}
