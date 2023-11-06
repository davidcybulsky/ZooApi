namespace ZooApi.Dtos
{
    public class CaretakerDto
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public AddressDto Address { get; set; }
    }
}
