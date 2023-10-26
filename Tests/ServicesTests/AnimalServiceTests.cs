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
        var animal = new Animal { Name = "TestAnimal", Species = Species.TIGER };

        // Act
        _animalService.Create(animal);

        // Assert
        _mockRepository.Received(1).Create(animal);
    }

    [Fact]
    public void Read_ShouldCallRepositoryRead()
    {
        // Arrange
        var animalId = Guid.NewGuid();
        _mockRepository.Read(animalId).Returns(new Animal { Id = animalId, Name = "TestAnimal", Species = Species.LION });

        // Act
        var retrievedAnimal = _animalService.Read(animalId);

        // Assert
        retrievedAnimal.Should().NotBeNull();
        retrievedAnimal.Id.Should().Be(animalId);
        retrievedAnimal.Name.Should().Be("TestAnimal");
        retrievedAnimal.Species.Should().Be(Species.LION);
    }

    [Fact]
    public void Update_ShouldCallRepositoryUpdate()
    {
        // Arrange
        var animalId = Guid.NewGuid();
        var updatedAnimal = new Animal { Id = animalId, Name = "UpdatedAnimal", Species = Species.PANDA };

        // Act
        _animalService.Update(animalId, updatedAnimal);

        // Assert
        _mockRepository.Received(1).Update(animalId, updatedAnimal);
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