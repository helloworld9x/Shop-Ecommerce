//using Nop.Core.Domain.Customers;

//namespace Nop.Data.Mapping.Customers
//{
//    public class CustomerAttributeValueMap : GoqEntityTypeConfiguration<CustomerAttributeValue>
//    {
//        public CustomerAttributeValueMap()
//        {
//            ToTable("CustomerAttributeValue");
//            HasKey(cav => cav.Id);
//            Property(cav => cav.Name).IsRequired().HasMaxLength(400);

//            HasRequired(cav => cav.CustomerAttribute)
//                .WithMany(ca => ca.CustomerAttributeValues)
//                .HasForeignKey(cav => cav.CustomerAttributeId);
//        }
//    }
//}