using Zoo.Entities;

namespace ZooApi.Dtos
{
    public class AnimalDto
    {
        public required string Name { get; set; }
        public required Species Species { get; set; }
        public required DateTime DateOfBirth { get; set; }
        public required CaretakerDto Caretaker { get; set; }
        public Guid CaretakerId { get; set; }
    }
}
