using Moq;
using ProjectManagment.BLL.Services;
using ProjectManagment.DAL.Entities;
using ProjectManagment.DAL.Interfaces;

namespace ProjectManagment.BLL.Tests.Services;

public class EmployeesServiceTests
{
    [Fact]
    public void GetByName_NameExists_ReturnsMatchingEnployees()
    {
        var employees = new List<Employee>
        {
            new() { Id = Guid.NewGuid(), FirstName = "Alex", LastName = "Smith" },
            new() { Id = Guid.NewGuid(), FirstName = "Ivan", LastName = "Ivamov" }
        };

        var searchName = "Ivan";
        var expected = employees.Where(e => e.FirstName.Contains(searchName, StringComparison.CurrentCultureIgnoreCase) || e.LastName.Contains(searchName, StringComparison.CurrentCultureIgnoreCase)).ToList();

        var repository = new Mock<IEmployeeRepository>();
        repository.Setup(r => r.Find(It.IsAny<Func<Employee, bool>>()))
        .Returns(expected);
        var service = new EmployeesService(repository.Object);

        var result = service.GetByName(searchName);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void GetByName_NoMatchingName_ReturnsEmptyList()
    {
        var employees = new List<Employee>
        {
            new() { Id = Guid.NewGuid(), FirstName = "Alex", LastName = "Smith" },
            new() { Id = Guid.NewGuid(), FirstName = "Ivan", LastName = "Ivamov" }
        };


        var searchName = "NonExistingName";
        var expected = employees.Where(e => e.FirstName.Contains(searchName, StringComparison.CurrentCultureIgnoreCase) || e.LastName.Contains(searchName, StringComparison.CurrentCultureIgnoreCase)).ToList();

        var repository = new Mock<IEmployeeRepository>();
        repository.Setup(r => r.Find(It.IsAny<Func<Employee, bool>>()))
        .Returns(expected);
        var service = new EmployeesService(repository.Object);

        var result = service.GetByName(searchName);

        Assert.Empty(result);
    }

    [Fact]
    public async Task Delete_NonExistingEmployee_ReturnsResultFailure()
    {
        var nonExistingEntity = new Employee { Id = Guid.NewGuid() };

        var repository = new Mock<IEmployeeRepository>();
        var service = new EmployeesService(repository.Object);

        var result = await service.Delete(nonExistingEntity.Id);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task Delete_BusyEmployee_ReturnsResultFailure()
    {
        var existingEntity = new Employee { Id = Guid.NewGuid() };

        var repository = new Mock<IEmployeeRepository>();
        repository.Setup(r => r.Get(existingEntity.Id)).ReturnsAsync(existingEntity);
        repository.Setup(r => r.HasEmployeeManagedProjects(existingEntity.Id)).ReturnsAsync(true);
        repository.Setup(r => r.HasEmployeeProjects(existingEntity.Id)).ReturnsAsync(true);
        var service = new EmployeesService(repository.Object);

        var result = await service.Delete(existingEntity.Id);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task Delete_ExistingEmployee_ReturnsSuccess()
    {
        var existingEntity = new Employee { Id = Guid.NewGuid() };

        var repository = new Mock<IEmployeeRepository>();
        repository.Setup(r => r.Get(existingEntity.Id)).ReturnsAsync(existingEntity);
        var service = new EmployeesService(repository.Object);

        var result = await service.Delete(existingEntity.Id);

        repository.Verify(r => r.Delete(existingEntity.Id), Times.Once);
        Assert.True(result.IsSuccess);
    }
}
