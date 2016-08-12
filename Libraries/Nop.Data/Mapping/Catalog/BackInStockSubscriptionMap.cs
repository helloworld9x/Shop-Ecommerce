using Nop.Core.Domain.Catalog;

namespace Nop.Data.Mapping.Catalog
{
    public class BackInStockSubscriptionMap : GoqEntityTypeConfiguration<BackInStockSubscription>
    {
        public BackInStockSubscriptionMap()
        {
            ToTable("BackInStockSubscription");
            HasKey(x => x.Id);

            HasRequired(x => x.Product)
                .WithMany()
                .HasForeignKey(x => x.ProductId)
                .WillCascadeOnDelete(true);
            
            HasRequired(x => x.Customer)
                .WithMany()
                .HasForeignKey(x => x.CustomerId)
                .WillCascadeOnDelete(true);
        }
    }
}