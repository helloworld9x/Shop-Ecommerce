using Nop.Core.Domain.Catalog;

namespace Nop.Data.Mapping.Catalog
{
    public class ProductMap : GoqEntityTypeConfiguration<Product>
    {
        public ProductMap()
        {
            ToTable("Product");
            HasKey(p => p.Id);
            Property(p => p.Name).IsRequired().HasMaxLength(400);
            Property(p => p.Sku).HasMaxLength(400);
            Property(p => p.ManufacturerPartNumber).HasMaxLength(400);
            Property(p => p.AdditionalShippingCharge).HasPrecision(18, 4);
            Property(p => p.Price).HasPrecision(18, 4);
            Property(p => p.OldPrice).HasPrecision(18, 4);
            Property(p => p.ProductCost).HasPrecision(18, 4);
            Property(p => p.SpecialPrice).HasPrecision(18, 4);
            Property(p => p.MinimumCustomerEnteredPrice).HasPrecision(18, 4);
            Property(p => p.MaximumCustomerEnteredPrice).HasPrecision(18, 4);
            Property(p => p.Weight).HasPrecision(18, 4);
            Property(p => p.Length).HasPrecision(18, 4);
            Property(p => p.Width).HasPrecision(18, 4);
            Property(p => p.Height).HasPrecision(18, 4);
            Property(p => p.RequiredProductIds).HasMaxLength(1000);
            Property(p => p.AllowedQuantities).HasMaxLength(1000);
            Property(p => p.BasepriceAmount).HasPrecision(18, 4);
            Property(p => p.BasepriceBaseAmount).HasPrecision(18, 4);
            Ignore(p => p.ProductType);
            Ignore(p => p.BackorderMode);
            Ignore(p => p.LowStockActivity);
            Ignore(p => p.ManageInventoryMethod);

            HasMany(p => p.ProductTags)
                .WithMany(pt => pt.Products)
                .Map(m => m.ToTable("Product_ProductTag_Mapping"));
        }
    }
}