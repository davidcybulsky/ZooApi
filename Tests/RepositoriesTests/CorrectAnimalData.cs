using System.Collections;
using Zoo.Entities;

namespace Tests.RepositoriesTests
{
    public class CorrectAnimalData : IEnumerable<object[]>
    {
        private readonly List<object[]> _animals = new List<object[]>
        {
            new object[]
            {
                "John", Species.PARROT, DateTime.Now, Guid.NewGuid()
            },
            new object[]
            {
                "Lili", Species.PANDA, DateTime.Now, Guid.NewGuid()
            },
            new object[]
            {
                "Ricky", Species.DOLPHIN, DateTime.Now, Guid.NewGuid()
            },
            new object[]
            {
                "Carol", Species.LION, DateTime.Now, Guid.NewGuid()
            }
    };

        public IEnumerator<object[]> GetEnumerator() => _animals.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
