using Nop.Core.Domain.Catalog;

namespace Nop.Data.Mapping.Catalog
{
    public class ProductCategoryMap : GoqEntityTypeConfiguration<ProductCategory>
    {
        public ProductCategoryMap()
        {
            ToTable("Product_Category_Mapping");
            HasKey(pc => pc.Id);
            
            HasRequired(pc => pc.Category)
                .WithMany()
                .HasForeignKey(pc => pc.CategoryId);


            HasRequired(pc => pc.Product)
                .WithMany(p => p.ProductCategories)
                .HasForeignKey(pc => pc.ProductId);
        }
    }
}