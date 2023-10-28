using FluentAssertions;
using NSubstitute;
using Zoo.Entities;
using Zoo.Services;
using ZooApi.Interface;

namespace Tests.ServicesTests;

public class AnimalServiceTests
{
    private readonly IRepository<Animal> _mockRepository;
    private readonly AnimalService _animalService;

    public AnimalServiceTests()
    {
        _mockRepository = Substitute.For<IRepository<Animal>>();
        _animalService = new AnimalService(_mockRepository);
    }

    [Fact]
    public void Create_ShouldCallRepositoryCreate()
    {
        // Arrange
        var animal = new Animal { Name = "TestAnimal", Species = Species.TIGER, DateOfBirth = DateTime.Now, CaretakerId = Guid.NewGuid() };

        // Act
        _animalService.Create(animal);

        // Assert
        _mockRepository.Received(1).Create(animal);
    }

    [Fact]
    public void Create_NullAnimal_ShouldThrowException()
    {
        // Act and Assert
        _animalService.Invoking(service => service.Create(null))
            .Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void Read_ShouldCallRepositoryRead()
    {
        // Arrange
        var animal = new Animal { Name = "TestAnimal", Species = Species.LION, DateOfBirth = DateTime.Now, CaretakerId = Guid.NewGuid() };
        _mockRepository.Read(animal.Id).Returns(animal);

        // Act
        var retrievedAnimal = _animalService.Read(animal.Id);

        // Assert
        retrievedAnimal.Should().NotBeNull();
        retrievedAnimal.Id.Should().Be(animal.Id);
        retrievedAnimal.Name.Should().Be(animal.Name);
        retrievedAnimal.Species.Should().Be(animal.Species);
    }

    [Fact]
    public void Update_ShouldCallRepositoryUpdate()
    {
        // Arrange
        var animalId = Guid.NewGuid();
        var updatedAnimal = new Animal {Name = "UpdatedAnimal", Species = Species.PANDA, DateOfBirth = DateTime.Now, CaretakerId = Guid.NewGuid() };

        // Act
        _animalService.Update(animalId, updatedAnimal);

        // Assert
        _mockRepository.Received(1).Update(animalId, updatedAnimal);
    }

    [Fact]
    public void Update_NullAnimal_ShouldThrowException()
    {
        // Arrange
        var animalId = Guid.NewGuid();
        Animal updatedAnimal = null;

        // Act and Assert
        _animalService.Invoking(repo => repo.Update(animalId, updatedAnimal))
            .Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void Delete_ShouldCallRepositoryDelete()
    {
        // Arrange
        var animalId = Guid.NewGuid();

        // Act
        _animalService.Delete(animalId);

        // Assert
        _mockRepository.Received(1).Delete(animalId);
    }
}