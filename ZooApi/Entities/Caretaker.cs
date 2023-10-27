namespace Zoo.Entities
{
    public class Caretaker : BaseEntity
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required virtual Address Address { get; set; }
        public virtual List<Animal> Animals { get; set; } = new List<Animal>();
    }
}