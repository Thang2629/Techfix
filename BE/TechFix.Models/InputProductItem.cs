
using System;

namespace TechFix.EntityModels
{
    /// <summary>
    /// Matrix nhập kho sản phẩm
    /// </summary>
    public class InputProductItem : BaseModel
    {
        #region FK

        public Product Product { get; set; }
        public Guid? ProductId { get; set; }

        public InputProduct InputProduct { get; set; }
        public Guid? InputProductId { get; set; }

        #endregion

        public int Quantity { get; set; }
        public int OriginalPrice { get; set; }
    }
}
