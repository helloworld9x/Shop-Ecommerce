using Nop.Core.Domain.Topics;

namespace Nop.Data.Mapping.Topics
{
    public class TopicMap : GoqEntityTypeConfiguration<Topic>
    {
        public TopicMap()
        {
            this.ToTable("Topic");
            this.HasKey(t => t.Id);
        }
    }
}
