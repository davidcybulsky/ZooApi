using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Zoo.Entities;
using ZooApi.Data;
using ZooApi.Repositories;

namespace Tests.RepositoriesTests;
public class AnimalRepositoryTests
{
    private readonly ZooContext _context;
    private readonly AnimalRepository _animalRepository;

    public AnimalRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<ZooContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _context = new ZooContext(options);
        _animalRepository = new AnimalRepository(_context);
    }

    [Fact]
    public void Create_ShouldAddAnimalToDatabase()
    {
        // Arrange
        var animal = new Animal { Name = "TestAnimal", Species = Species.TIGER, DateOfBirth = DateTime.Now, CaretakerId = Guid.NewGuid() };

        // Act
        _animalRepository.Create(animal);

        // Assert
        var savedAnimal = _context.Animals.FirstOrDefault(a => a.Id == animal.Id);
        savedAnimal.Should().NotBeNull();
        savedAnimal.Name.Should().Be(animal.Name);
        savedAnimal.Species.Should().Be(animal.Species);
    }

    [Fact]
    public void Read_ShouldReturnAnimalFromDatabase()
    {
        // Arrange
        var animal = new Animal { Name = "TestAnimal", Species = Species.LION, DateOfBirth = DateTime.Now, CaretakerId = Guid.NewGuid() };
        _context.Animals.Add(animal);
        _context.SaveChanges();

        // Act
        var retrievedAnimal = _animalRepository.Read(animal.Id);

        // Assert
        retrievedAnimal.Should().NotBeNull();
        retrievedAnimal.Id.Should().Be(animal.Id);
        retrievedAnimal.Name.Should().Be(animal.Name);
        retrievedAnimal.Species.Should().Be(animal.Species);
    }

    [Fact]
    public void Read_ShouldThrowException_WhenAnimalNotInDb()
    {
        // Arrange
        var animal = new Animal { Name = "TestAnimal", Species = Species.LION, DateOfBirth = DateTime.Now, CaretakerId = Guid.NewGuid() };

        // Act and Assert
        _animalRepository.Invoking(repo => repo.Read(animal.Id))
            .Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void Update_ShouldUpdateAnimalInDatabase()
    {
        // Arrange
        var animal = new Animal { Name = "TestAnimal", Species = Species.PARROT, DateOfBirth = DateTime.Now, CaretakerId = Guid.NewGuid() };
        _context.Animals.Add(animal);
        _context.SaveChanges();
        var updatedAnimal = new Animal { Name = "UpdatedAnimal", Species = Species.PANDA, DateOfBirth = DateTime.Now, CaretakerId = Guid.NewGuid() };

        // Act
        _animalRepository.Update(animal.Id, updatedAnimal);

        // Assert
        var retrievedAnimal = _context.Animals.FirstOrDefault(a => a.Id == animal.Id);
        retrievedAnimal.Should().NotBeNull();
        retrievedAnimal.Name.Should().Be(updatedAnimal.Name);
        retrievedAnimal.Species.Should().Be(updatedAnimal.Species);
    }

    [Fact]
    public void Update_ShouldThrowException_WhenAnimalNotInDb()
    {
        // Arrange
        var animal = new Animal { Name = "TestAnimal", Species = Species.LION, DateOfBirth = DateTime.Now, CaretakerId = Guid.NewGuid() };
        _context.SaveChanges();

        // Act and Assert
        _animalRepository.Invoking(repo => repo.Update(animal.Id, animal))
            .Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void Delete_ShouldRemoveAnimalFromDatabase()
    {
        // Arrange
        var animal = new Animal { Name = "TestAnimal", Species = Species.DOLPHIN, DateOfBirth = DateTime.Now, CaretakerId = Guid.NewGuid() };
        _context.Animals.Add(animal);
        _context.SaveChanges();

        // Act
        _animalRepository.Delete(animal.Id);

        // Assert
        var deletedAnimal = _context.Animals.FirstOrDefault(a => a.Id == animal.Id);
        deletedAnimal.Should().BeNull();
    }
}