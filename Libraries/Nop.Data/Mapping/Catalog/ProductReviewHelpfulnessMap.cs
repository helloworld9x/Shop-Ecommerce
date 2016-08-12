using Nop.Core.Domain.Catalog;

namespace Nop.Data.Mapping.Catalog
{
    public class ProductReviewHelpfulnessMap : GoqEntityTypeConfiguration<ProductReviewHelpfulness>
    {
        public ProductReviewHelpfulnessMap()
        {
            ToTable("ProductReviewHelpfulness");
            HasKey(pr => pr.Id);

            HasRequired(prh => prh.ProductReview)
                .WithMany(pr => pr.ProductReviewHelpfulnessEntries)
                .HasForeignKey(prh => prh.ProductReviewId).WillCascadeOnDelete(true);
        }
    }
}