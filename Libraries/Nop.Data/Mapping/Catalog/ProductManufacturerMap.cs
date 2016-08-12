using Nop.Core.Domain.Catalog;

namespace Nop.Data.Mapping.Catalog
{
    public class ProductManufacturerMap : GoqEntityTypeConfiguration<ProductManufacturer>
    {
        public ProductManufacturerMap()
        {
            ToTable("Product_Manufacturer_Mapping");
            HasKey(pm => pm.Id);
            
            HasRequired(pm => pm.Manufacturer)
                .WithMany()
                .HasForeignKey(pm => pm.ManufacturerId);


            HasRequired(pm => pm.Product)
                .WithMany(p => p.ProductManufacturers)
                .HasForeignKey(pm => pm.ProductId);
        }
    }
}