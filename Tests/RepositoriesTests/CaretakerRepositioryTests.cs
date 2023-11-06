using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Zoo.Entities;
using ZooApi.Data;
using ZooApi.Repositories;

namespace Tests.RepositoriesTests
{
    public class CaretakerRepositioryTests
    {
        private readonly ZooContext _context;
        private readonly CaretakerRepository _caretakerRepository;

        public CaretakerRepositioryTests()
        {
            var options = new DbContextOptionsBuilder<ZooContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _context = new ZooContext(options);
            _caretakerRepository = new CaretakerRepository(_context);
        }

        [Fact]
        public void Create_ShouldAddCaretakerToDatabase()
        {
            // Arrange
            var caretaker = new Caretaker { FirstName = "John", LastName = "Doe", Address = new Address() { City = "Warsaw", Street = "Nowowiejska", ZipCode = "00-000" } };

            // Act
            _caretakerRepository.Create(caretaker);

            // Assert
            var savedCaretaker = _context.Caretakers.FirstOrDefault(a => a.Id == caretaker.Id);
            savedCaretaker.Should().NotBeNull();
            savedCaretaker.Should().Be(caretaker);
        }

        [Fact]
        public void Read_ShouldReturnCaretakerFromDatabase()
        {
            // Arrange
            var caretaker = new Caretaker { FirstName = "John", LastName = "Doe", Address = new Address() { City = "Warsaw", Street = "Nowowiejska", ZipCode = "00-000" } };
            _context.Caretakers.Add(caretaker);
            _context.SaveChanges();

            // Act
            var retrievedCaretaker = _caretakerRepository.Read(caretaker.Id);

            // Assert
            retrievedCaretaker.Should().Be(caretaker);
        }

        [Fact]
        public void Update_ShouldUpdateCaretakerInDatabase()
        {
            // Arrange
            var caretaker = new Caretaker { FirstName = "John", LastName = "Doe", Address = new Address() { City = "Warsaw", Street = "Nowowiejska", ZipCode = "00-000" } };
            _context.Caretakers.Add(caretaker);
            _context.SaveChanges();
            var updatedCaretaker = new Caretaker { FirstName = "James", LastName = "Cook", Address = new Address() { City = "Krakow", Street = "Zamkowa", ZipCode = "43-450" } };

            // Act
            _caretakerRepository.Update(caretaker.Id, updatedCaretaker);

            // Assert
            var retrievedCaretaker = _context.Caretakers.FirstOrDefault(a => a.Id == caretaker.Id);
            retrievedCaretaker.Should().Be(caretaker);
        }

        [Fact]
        public void Delete_ShouldRemoveCaretakerFromDatabase()
        {

            // Arrange
            var caretaker = new Caretaker { FirstName = "John", LastName = "Doe", Address = new Address() { City = "Warsaw", Street = "Nowowiejska", ZipCode = "00-000" } };
            _context.Caretakers.Add(caretaker);
            _context.SaveChanges();

            // Act
            _caretakerRepository.Delete(caretaker.Id);

            // Assert
            var deletedCaretaker = _context.Caretakers.FirstOrDefault(a => a.Id == caretaker.Id);
            deletedCaretaker.Should().BeNull();
        }
    }
}
