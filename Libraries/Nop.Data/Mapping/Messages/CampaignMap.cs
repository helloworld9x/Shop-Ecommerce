using Nop.Core.Domain.Messages;

namespace Nop.Data.Mapping.Messages
{
    public class CampaignMap : GoqEntityTypeConfiguration<Campaign>
    {
        public CampaignMap()
        {
            ToTable("Campaign");
            HasKey(ea => ea.Id);

            Property(ea => ea.Name).IsRequired();
            Property(ea => ea.Subject).IsRequired();
            Property(ea => ea.Body).IsRequired();
        }
    }
}