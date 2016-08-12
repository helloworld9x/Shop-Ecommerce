namespace Nop.Core.Domain.Catalog
{
    /// <summary>
    /// Represents a price range
    /// </summary>
    public class PriceRange
    {
        /// <summary>
        /// From
        /// </summary>
        public decimal? From { get; set; }
        /// <summary>
        /// To
        /// </summary>
        public decimal? To { get; set; }
    }
}
