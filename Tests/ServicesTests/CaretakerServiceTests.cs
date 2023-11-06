using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Zoo.Entities;
using Zoo.Services;
using ZooApi.Interface;

namespace Tests.ServicesTests;
public class CaretakerServiceTests
{
    private readonly IRepository<Caretaker> _mCareTakerRepository;

    private readonly CaretakerService _caretakerService;

    public CaretakerServiceTests()
    {
        _mCareTakerRepository = Substitute.For<IRepository<Caretaker>>();
        _caretakerService = new CaretakerService(_mCareTakerRepository);
    }

    [Fact]
    public void Create_WhenCaretakerIsValid_ShouldCallRepositoryCreate()
    {
        //Arrange
        var caretaker = new Caretaker
        {
            Address = new Address { Street = "mStreet", ZipCode = "mZipCode", City = "mCity" },
            FirstName = "mFirstName",
            LastName = "mLastName",
            Animals = new List<Animal>()
        };

        //Act
        _caretakerService.Create(caretaker);

        //Assert
        _mCareTakerRepository.Received(1).Create(caretaker);
    }

    [Fact]
    public void Create_WhenCaretakerIsNull_ShouldThrowArgumentNullException()
    {
        //Act and Assert
        _caretakerService.Invoking(service => service.Create(null))
            .Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void Create_WhenCaretakerWithIdIsInDb_ShouldThrowInvalidOperationException()
    {
        //Arrange
        var caretaker = new Caretaker
        {
            Address = new Address { Street = "street", City = "city", ZipCode = "11-222" },
            FirstName = "John",
            LastName = "Doe"
        };

        //Act
        _mCareTakerRepository.Read(caretaker.Id).Returns(caretaker);

        //Assert
        _caretakerService.Invoking(service => service.Create(caretaker))
            .Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void Read_WhenCaretakerIsValid_ShouldReturnCaretaker()
    {
        //Arrange
        var caretaker = new Caretaker
        {
            Address = new Address { Street = "mStreet", ZipCode = "mZipCode", City = "mCity" },
            FirstName = "mFirstName",
            LastName = "mLastName",
            Animals = new List<Animal>()
        };

        _mCareTakerRepository.Read(caretaker.Id).Returns(caretaker);

        //Act
        var retrievedCaretaker = _caretakerService.Read(caretaker.Id);

        //Assert
        retrievedCaretaker.Should().NotBeNull();
        retrievedCaretaker.Should().Be(caretaker);
    }

    [Fact]
    public void Update_WhenInputCaretakerIsValid_ShouldUpdateCaretaker()
    {
        //Arrange
        var caretaker = new Caretaker
        {
            Address = new Address { City = "Warsaw", Street = "Nowowiejska", ZipCode = "11-229" },
            FirstName = "John",
            LastName = "Doe"
        };

        var updatedCaretaker = new Caretaker
        {
            Address = new Address { Street = "mStreet", ZipCode = "mZipCode", City = "mCity" },
            FirstName = "mUpdatedFirstName",
            LastName = "mUpdatedLastName"
        };

        _mCareTakerRepository.Read(caretaker.Id).Returns(caretaker);

        //Act
        _caretakerService.Update(caretaker.Id, updatedCaretaker);

        //Assert
        _mCareTakerRepository.Received(1).Update(caretaker.Id, updatedCaretaker);
    }

    [Fact]
    public void Update_WhenUpdatedCaretakerIsNull_ShouldThrowArgumentNullException()
    {
        //Arrange
        var caretakerId = Guid.NewGuid();

        //Act and Assert
        _caretakerService.Invoking(service => service.Update(caretakerId, null))
            .Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void Update_WhenCaretakerDoesNotExist_ShoukdThrowException()
    {
        //Arrange
        var caretakerId = Guid.NewGuid();

        var caretakerToUpdate = new Caretaker
        {
            Address = new Address { City = "Warsaw", Street = "Nowowiejska", ZipCode = "11-229" },
            FirstName = "John",
            LastName = "Doe"
        };

        _mCareTakerRepository.Read(caretakerId).ReturnsNull();

        //Act and Assert
        _caretakerService.Invoking(service => service.Update(caretakerId, caretakerToUpdate))
            .Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void Delete_WhenIdIsValid_DeleteCaretaker()
    {
        //Arrange
        var caretaker = new Caretaker
        {
            Address = new Address { City = "Warsaw", Street = "Nowowiejska", ZipCode = "11-229" },
            FirstName = "John",
            LastName = "Doe"
        };

        //Act
        _mCareTakerRepository.Read(caretaker.Id).Returns(caretaker);

        _caretakerService.Delete(caretaker.Id);

        //Assert
        _mCareTakerRepository.Received(1).Delete(caretaker.Id);
    }

    [Fact]
    public void Delete_WhenCaretakerDoesNotExist_ShouldThrowException()
    {
        //Arrange
        var caretaker = new Caretaker
        {
            Address = new Address { City = "Warsaw", Street = "Nowowiejska", ZipCode = "11-229" },
            FirstName = "John",
            LastName = "Doe"
        };

        _mCareTakerRepository.Read(caretaker.Id).ReturnsNull();

        //Act and Assert
        _caretakerService.Invoking(service => service.Delete(caretaker.Id))
            .Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void Create_WhenFirstNameIsNull_ShouldthrowException()
    {
        //Arrange
        var caretaker = new Caretaker
        {
            Address = new Address { City = "Warsaw", Street = "Nowowiejska", ZipCode = "11-229" },
            FirstName = null,
            LastName = "Doe"
        };

        //Act and Assert
        _caretakerService.Invoking(service => service.Create(caretaker))
            .Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void Create_WhenLastNameIsNull_ShouldThrowException()
    {
        //Arrange
        var caretaker = new Caretaker
        {
            Address = new Address { City = "Warsaw", Street = "Nowowiejska", ZipCode = "11-229" },
            FirstName = "John",
            LastName = null
        };

        //Act and Assert
        _caretakerService.Invoking(service => service.Create(caretaker))
            .Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void Create_WhenAddressIsNull_ShouldThrowException()
    {
        //Arrange
        var caretaker = new Caretaker
        {
            Address = null,
            FirstName = "John",
            LastName = "Doe"
        };

        //Act and Assert
        _caretakerService.Invoking(service => service.Create(caretaker))
            .Should().Throw<ArgumentNullException>();
    }
}
