//using Nop.Core.Domain.Catalog;

//namespace Nop.Data.Mapping.Catalog
//{
//    public class ManufacturerTemplateMap : GoqEntityTypeConfiguration<ManufacturerTemplate>
//    {
//        public ManufacturerTemplateMap()
//        {
//            ToTable("ManufacturerTemplate");
//            HasKey(p => p.Id);
//            Property(p => p.Name).IsRequired().HasMaxLength(400);
//            Property(p => p.ViewPath).IsRequired().HasMaxLength(400);
//        }
//    }
//}