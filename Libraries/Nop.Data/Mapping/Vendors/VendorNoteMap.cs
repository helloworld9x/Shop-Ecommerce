using Nop.Core.Domain.Vendors;

namespace Nop.Data.Mapping.Vendors
{
    public class VendorNoteMap : GoqEntityTypeConfiguration<VendorNote>
    {
        public VendorNoteMap()
        {
            ToTable("VendorNote");
            HasKey(vn => vn.Id);
            Property(vn => vn.Note).IsRequired();

            HasRequired(vn => vn.Vendor)
                .WithMany(v => v.VendorNotes)
                .HasForeignKey(vn => vn.VendorId);
        }
    }
}