using Nop.Core.Domain.Messages;

namespace Nop.Data.Mapping.Messages
{
    public class NewsLetterSubscriptionMap : GoqEntityTypeConfiguration<NewsLetterSubscription>
    {
        public NewsLetterSubscriptionMap()
        {
            this.ToTable("NewsLetterSubscription");
            this.HasKey(nls => nls.Id);

            this.Property(nls => nls.Email).IsRequired().HasMaxLength(255);
        }
    }
}