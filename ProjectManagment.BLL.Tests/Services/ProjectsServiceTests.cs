using Moq;
using ProjectManagment.BLL.Services;
using ProjectManagment.DAL.Entities;
using ProjectManagment.DAL.Interfaces;

namespace ProjectManagment.BLL.Tests.Services;

public class ProjectsServiceTests
{
    [Fact]
    public void GetByName_NameExists_ReturnsMatchingProjects()
    {
        var projects = new List<Project>
        {
            new() { Id = Guid.NewGuid(), Name = "ToDo App" },
            new() { Id = Guid.NewGuid(), Name = "Facebook" }
        };

        var searchName = "ToDo";
        var expectedCompanies = projects.Where(c => c.Name.Contains(searchName, StringComparison.CurrentCultureIgnoreCase)).ToList();

        var repository = new Mock<IProjectRepository>();
        repository.Setup(r => r.Find(It.IsAny<Func<Project, bool>>()))
        .Returns(expectedCompanies);
        var service = new ProjectsService(repository.Object);

        var result = service.GetByName(searchName);

        Assert.Equal(expectedCompanies, result);
    }

    [Fact]
    public void GetByName_NoMatchingName_ReturnsEmptyList()
    {
        var projects = new List<Project>
        {
            new() { Id = Guid.NewGuid(), Name = "ToDo App" },
            new() { Id = Guid.NewGuid(), Name = "Facebook" }
        };

        var searchName = "NonExistingName";
        var expectedCompanies = projects.Where(c => c.Name.Contains(searchName, StringComparison.CurrentCultureIgnoreCase)).ToList();

        var repository = new Mock<IProjectRepository>();
        repository.Setup(r => r.Find(It.IsAny<Func<Project, bool>>()))
        .Returns(expectedCompanies);
        var service = new ProjectsService(repository.Object);

        var result = service.GetByName(searchName);

        Assert.Empty(result);
    }
}
