using FluentAssertions;
using NSubstitute;
using Zoo.Entities;
using Zoo.Services;
using ZooApi.Interface;

namespace Tests.ServicesTests;

public class CareTakerServiceTests
{
    private readonly IRepository<Caretaker> _mCareTakerRepository;

    private readonly CaretakerService _caretakerService;

    public CareTakerServiceTests()
    {
        _mCareTakerRepository = Substitute.For<IRepository<Caretaker>>();
        _caretakerService = new CaretakerService(_mCareTakerRepository);
    }

    [Fact]
    public void Create_WhenCaretakerIsValid_ShouldCallRepositoryCreate()
    {
        var caretaker = new Caretaker
        {
            Address = new Address { Street = "mStreet", ZipCode = "mZipCode", City = "mCity" },
            FirstName = "mFirstName",
            LastName = "mLastName",
            Animals = new List<Animal>()
        };

        _caretakerService.Create(caretaker);

        _mCareTakerRepository.Received(1).Create(caretaker);
    }

    [Fact]
    public void Create_WhenCaretakerIsNull_ShouldThrowArgumentNullException()
    {
        _caretakerService.Invoking(service => service.Create(null))
            .Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void Read_WhenCareTakerIsValid_ShouldReturnCaretaker()
    {
        var caretaker = new Caretaker
        {
            Address = new Address { Street = "mStreet", ZipCode = "mZipCode", City = "mCity" },
            FirstName = "mFirstName",
            LastName = "mLastName",
            Animals = new List<Animal>()
        };
        _mCareTakerRepository.Read(caretaker.Id).Returns(caretaker);

        var retrievedCaretaker = _caretakerService.Read(caretaker.Id);

        retrievedCaretaker.Should().NotBeNull();
        retrievedCaretaker.FirstName.Should().Be(caretaker.FirstName);
        retrievedCaretaker.LastName.Should().Be(caretaker.LastName);
    }

    [Fact]
    public void Update_WhenInputAnimalIsValid_ShouldUpdateCaretaker()
    {
        var caretakerId = Guid.NewGuid();
        var updatedCaretaker = new Caretaker
        {
            Address = new Address { Street = "mStreet", ZipCode = "mZipCode", City = "mCity" },
            FirstName = "mUpdatedFirstName",
            LastName = "mUpdatedLastName",
            Animals = new List<Animal>()
        };

        _caretakerService.Update(caretakerId, updatedCaretaker);

        _mCareTakerRepository.Received(1).Update(caretakerId, updatedCaretaker);
    }

    [Fact]
    public void Update_WhenUpdatedAnimalIsNull_ShouldThrowArgumentNullException()
    {
        var caretakerId = Guid.NewGuid();

        _caretakerService.Invoking(service => service.Update(caretakerId, null))
            .Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void Delete_WhenIdIsValid_DeleteCaretaker()
    {
        var caretakerId = Guid.NewGuid();

        _caretakerService.Delete(caretakerId);

        _mCareTakerRepository.Received(1).Delete(caretakerId);
    }
}