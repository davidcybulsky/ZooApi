namespace Zoo.Entities
{
    public class Animal : BaseEntity
    {
        public string Name { get; set; }
        public Species Species { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Caretaker Caretaker { get; set; }
        public Guid CaretakerId { get; set; }
    }
}
