namespace Nop.Core.Domain.Discounts
{
    /// <summary>
    /// Represents a discount type
    /// </summary>
    public enum DiscountType
    {
        /// <summary>
        /// Assigned to order total 
        /// </summary>
        AssignedToOrderTotal = 1,

        /// <summary>
        /// Assigned to products (SKUs)
        /// </summary>
        AssignedToSkus = 2,

        /// <summary>
        /// Assigned to categories (all products in a category)
        /// </summary>
        AssignedToCategories = 5,
    }
}
