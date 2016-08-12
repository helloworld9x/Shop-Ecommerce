using Nop.Core.Domain.Catalog;

namespace Nop.Data.Mapping.Catalog
{
    public class ProductPictureMap : GoqEntityTypeConfiguration<ProductPicture>
    {
        public ProductPictureMap()
        {
            ToTable("Product_Picture_Mapping");
            HasKey(pp => pp.Id);
            
            HasRequired(pp => pp.Picture)
                .WithMany()
                .HasForeignKey(pp => pp.PictureId);


            HasRequired(pp => pp.Product)
                .WithMany(p => p.ProductPictures)
                .HasForeignKey(pp => pp.ProductId);
        }
    }
}