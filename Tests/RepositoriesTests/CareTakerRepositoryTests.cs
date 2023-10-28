using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Zoo.Entities;
using ZooApi.Data;
using ZooApi.Repositories;

namespace Tests.RepositoriesTests;
public class CareTakerRepositoryTests
{
    private readonly ZooContext _context;
    private readonly CaretakerRepository _careTakerRepository;

    public CareTakerRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<ZooContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _context = new ZooContext(options);
        _careTakerRepository = new CaretakerRepository(_context);
    }

    [Fact]
    public void Create_WhenInputCareTakerIsValid_ShouldSaveInDB()
    {
        var caretaker = new Caretaker
        {
            Address = new Address { Street = "mStreet", ZipCode = "mZipCode", City = "mCity" },
            FirstName = "mFirstName",
            LastName = "mLastName",
            Animals = new List<Animal>()
        };

        _careTakerRepository.Create(caretaker);

        var savedCaretaker = _context.Caretakers.FirstOrDefault(c => c.Id == caretaker.Id);
        savedCaretaker.Should().NotBeNull();
        savedCaretaker.FirstName.Should().Be(caretaker.FirstName);
        savedCaretaker.LastName.Should().Be(caretaker.LastName);
        savedCaretaker.Address.Should().Be(caretaker.Address);
    }

    [Fact]
    public void Read_WhenInputIdIsValid_ShouldReturnCaretaker()
    {
        var caretaker = new Caretaker
        {
            Address = new Address { Street = "mStreet", ZipCode = "mZipCode", City = "mCity" },
            FirstName = "mFirstName",
            LastName = "mLastName",
            Animals = new List<Animal>()
        };
        _context.Caretakers.Add(caretaker);
        _context.SaveChanges();

        var retrievedCarTaker = _careTakerRepository.Read(caretaker.Id);

        retrievedCarTaker.Should().NotBeNull();
        retrievedCarTaker.FirstName.Should().Be(caretaker.FirstName);
        retrievedCarTaker.LastName.Should().Be(caretaker.LastName);
        retrievedCarTaker.Address.Should().Be(caretaker.Address);
    }

    [Fact]
    public void Update_WhenInputIdIsValid_ShouldUpdateCaretaker()
    {
        var caretaker = new Caretaker
        {
            Address = new Address { Street = "mStreet", ZipCode = "mZipCode", City = "mCity" },
            FirstName = "mFirstName",
            LastName = "mLastName",
            Animals = new List<Animal>()
        };
        var updatedCareTaker = new Caretaker
        {
            Address = new Address { Street = "mStreet", ZipCode = "mZipCode", City = "mCity" },
            FirstName = "mUpdatedFirstName",
            LastName = "mUpdatedLastName",
            Animals = new List<Animal>()
        };
        _context.Caretakers.Add(caretaker);
        _context.SaveChanges();

        _careTakerRepository.Update(caretaker.Id, updatedCareTaker);

        var retrievedCareTaker = _context.Caretakers.FirstOrDefault(c => c.Id == caretaker.Id);
        retrievedCareTaker.Should().NotBeNull();
        retrievedCareTaker.FirstName.Should().Be(updatedCareTaker.FirstName);
        retrievedCareTaker.LastName.Should().Be(updatedCareTaker.LastName);
    }

    [Fact]
    public void Delete_ShouldRemoveAnimalFromDatabase()
    {
        var caretaker = new Caretaker
        {
            Address = new Address { Street = "mStreet", ZipCode = "mZipCode", City = "mCity" },
            FirstName = "mFirstName",
            LastName = "mLastName",
            Animals = new List<Animal>()
        };
        _context.Caretakers.Add(caretaker);
        _context.SaveChanges();

        _careTakerRepository.Delete(caretaker.Id);

        var deletedCareTaker = _context.Animals.FirstOrDefault(c => c.Id == caretaker.Id);
        deletedCareTaker.Should().BeNull();
    }
}