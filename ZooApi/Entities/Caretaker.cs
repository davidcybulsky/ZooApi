namespace Zoo.Entities
{
    public class Caretaker : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Address Address { get; set; }
        public List<Animal> Animals { get; set; } = new List<Animal>();
    }
}
