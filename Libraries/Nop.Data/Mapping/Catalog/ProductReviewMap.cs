using Nop.Core.Domain.Catalog;

namespace Nop.Data.Mapping.Catalog
{
    public class ProductReviewMap : GoqEntityTypeConfiguration<ProductReview>
    {
        public ProductReviewMap()
        {
            ToTable("ProductReview");
            HasKey(pr => pr.Id);

            HasRequired(pr => pr.Product)
                .WithMany(p => p.ProductReviews)
                .HasForeignKey(pr => pr.ProductId);

            HasRequired(pr => pr.Customer)
                .WithMany()
                .HasForeignKey(pr => pr.CustomerId);
        }
    }
}