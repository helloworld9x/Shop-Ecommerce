using Nop.Core.Domain.Common;

namespace Nop.Data.Mapping.Common
{
    public class SearchTermMap : GoqEntityTypeConfiguration<SearchTerm>
    {
        public SearchTermMap()
        {
            this.ToTable("SearchTerm");
            this.HasKey(st => st.Id);
        }
    }
}
