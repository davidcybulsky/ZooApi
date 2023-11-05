namespace Zoo.Entities
{
    public class Address : BaseEntity
    {
        public required string Street { get; set; }
        public required string City { get; set; }
        public required string ZipCode { get; set; }
    }
}
