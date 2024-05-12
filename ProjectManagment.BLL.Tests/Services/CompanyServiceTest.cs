using Moq;
using ProjectManagment.BLL.Services;
using ProjectManagment.DAL.Entities;
using ProjectManagment.DAL.Interfaces;

namespace ProjectManagment.BLL.Tests.Services;

public class CompanyServiceTest
{
    [Fact]
    public void GetByName_NameExists_ReturnsMatchingCompanies()
    {
        var companies = new List<Company>
        {
            new() { Id = Guid.NewGuid(), Name = "Acme Inc." },
            new() { Id = Guid.NewGuid(), Name = "Globex Corporation" },
            new() { Id = Guid.NewGuid(), Name = "Stark Industries" }
        };

        var searchName = "Inc";
        var expectedCompanies = companies.Where(c => c.Name.Contains(searchName, StringComparison.CurrentCultureIgnoreCase)).ToList();

        var repository = new Mock<ICompanyRepository>();
        repository.Setup(r => r.Find(It.IsAny<Func<Company, bool>>()))
        .Returns(expectedCompanies);
        var service = new CompaniesService(repository.Object);

        var result = service.GetByName(searchName);

        Assert.Equal(expectedCompanies, result);
    }

    [Fact]
    public void GetByName_NoMatchingName_ReturnsEmptyList()
    {
        var companies = new List<Company>
        {
            new() { Id = Guid.NewGuid(), Name = "Acme Inc." },
            new() { Id = Guid.NewGuid(), Name = "Globex Corporation" },
            new() { Id = Guid.NewGuid(), Name = "Stark Industries" }
        };

        var searchName = "NonExistingName";
        var expectedCompanies = companies.Where(c => c.Name.Contains(searchName, StringComparison.CurrentCultureIgnoreCase));

        var repository = new Mock<ICompanyRepository>();
        repository.Setup(r => r.Find(It.IsAny<Func<Company, bool>>()))
        .Returns(expectedCompanies);
        var service = new CompaniesService(repository.Object);

        var result = service.GetByName(searchName);

        Assert.Empty(result);
    }

    [Fact]
    public async Task Delete_NonExistingCompany_ReturnsResultFailure()
    {
        var nonExistingEntity = new Company { Id = Guid.NewGuid() };

        var repository = new Mock<ICompanyRepository>();
        var service = new CompaniesService(repository.Object);

        var result = await service.Delete(nonExistingEntity.Id);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task Delete_BusyCompany_ReturnsResultFailure()
    {
        var existingEntity = new Company { Id = Guid.NewGuid() };

        var repository = new Mock<ICompanyRepository>();
        repository.Setup(r => r.Get(existingEntity.Id)).ReturnsAsync(existingEntity);
        repository.Setup(r => r.HasCompanyProjects(existingEntity.Id)).ReturnsAsync(true);
        var service = new CompaniesService(repository.Object);

        var result = await service.Delete(existingEntity.Id);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task Delete_ExistingCompany_ReturnsSuccess()
    {
        var existingEntity = new Company { Id = Guid.NewGuid() };

        var repository = new Mock<ICompanyRepository>();
        repository.Setup(r => r.Get(existingEntity.Id)).ReturnsAsync(existingEntity);
        var service = new CompaniesService(repository.Object);

        var result = await service.Delete(existingEntity.Id);

        repository.Verify(r => r.Delete(existingEntity.Id), Times.Once);
        Assert.True(result.IsSuccess);
    }
}
