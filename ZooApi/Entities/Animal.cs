namespace Zoo.Entities
{
    public class Animal : BaseEntity
    {
        public required string Name { get; set; }
        public required Species Species { get; set; }
        public required DateTime DateOfBirth { get; set; }
        public virtual Caretaker Caretaker { get; set; }
        public required Guid CaretakerId { get; set; }
    }
}
