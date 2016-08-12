using Nop.Core.Domain.Catalog;

namespace Nop.Data.Mapping.Catalog
{
    public class ProductWarehouseInventoryMap : GoqEntityTypeConfiguration<ProductWarehouseInventory>
    {
        public ProductWarehouseInventoryMap()
        {
            ToTable("ProductWarehouseInventory");
            HasKey(x => x.Id);

            HasRequired(x => x.Product)
                .WithMany(p => p.ProductWarehouseInventory)
                .HasForeignKey(x => x.ProductId)
                .WillCascadeOnDelete(true);

            HasRequired(x => x.Warehouse)
                .WithMany()
                .HasForeignKey(x => x.WarehouseId)
                .WillCascadeOnDelete(true);
        }
    }
}