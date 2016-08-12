using Nop.Core.Domain.Catalog;

namespace Nop.Data.Mapping.Catalog
{
    public class ProductAttributeCombinationMap : GoqEntityTypeConfiguration<ProductAttributeCombination>
    {
        public ProductAttributeCombinationMap()
        {
            ToTable("ProductAttributeCombination");
            HasKey(pac => pac.Id);

            Property(pac => pac.Sku).HasMaxLength(400);
            Property(pac => pac.ManufacturerPartNumber).HasMaxLength(400);
            Property(pac => pac.Gtin).HasMaxLength(400);
            Property(pac => pac.OverriddenPrice).HasPrecision(18, 4);

            HasRequired(pac => pac.Product)
                .WithMany(p => p.ProductAttributeCombinations)
                .HasForeignKey(pac => pac.ProductId);
        }
    }
}