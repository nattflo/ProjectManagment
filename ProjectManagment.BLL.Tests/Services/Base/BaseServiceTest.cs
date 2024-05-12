using Moq;
using ProjectManagment.DAL.Entities;
using ProjectManagment.DAL.Interfaces;

namespace ProjectManagment.BLL.Tests.Services.Base;

public class BaseServiceTest
{

    [Fact]
    public async Task GetAll_ReturnsAll()
    {
        var entities = new List<Company>() { new(), new() };
        var repository = new Mock<ICompanyRepository>();
        repository.Setup(r => r.GetAll()).ReturnsAsync(entities);

        var service = new TestService(repository.Object);

        var result = await service.GetAll();

        Assert.Equal(result, entities);
    }

    [Fact]
    public async Task Get_ValidId_ReturnsEntity()
    {
        var entity = new Company() { Id = new Guid() };
        var repository = new Mock<ICompanyRepository>();
        repository.Setup(r => r.Get(entity.Id)).ReturnsAsync(entity);
        var service = new TestService(repository.Object);

        var result = await service.Get(entity.Id);

        Assert.Equal(result, entity);
    }

    [Fact]
    public async Task Get_InvalidId_ReturnsNull()
    {
        var invalidId = Guid.NewGuid();
        var repository = new Mock<ICompanyRepository>();
        repository.Setup(r => r.Get(invalidId)).ReturnsAsync((Company)null);
        var service = new TestService(repository.Object);

        var result = await service.Get(invalidId);

        Assert.True(result == null);
    }

    [Fact]
    public async Task Add_AddsEntityToRepository()
    {
        var entity = new Company();
        var repository = new Mock<ICompanyRepository>();
        var service = new TestService(repository.Object);

        await service.Add(entity);

        repository.Verify(r => r.Add(entity), Times.Once);
    }

    [Fact]
    public async Task Update_ExistingEntity_UpdatesEntityInRepository()
    {
        var existingEntity = new Company { Id = Guid.NewGuid() };
        var repository = new Mock<ICompanyRepository>();
        repository.Setup(r => r.Get(existingEntity.Id))
            .ReturnsAsync(existingEntity);
        var service = new TestService(repository.Object);

        await service.Update(existingEntity);

        repository.Verify(r => r.Update(existingEntity), Times.Once);
    }

    [Fact]
    public async Task Update_NonExistingEntity_DoesNotUpdateRepository()
    {
        var nonExistingEntity = new Company { Id = Guid.NewGuid() };
        var repository = new Mock<ICompanyRepository>();
        repository.Setup(r => r.Get(nonExistingEntity.Id))
            .ReturnsAsync((Company)null);
        var service = new TestService(repository.Object);

        await service.Update(nonExistingEntity);

        repository.Verify(r => r.Update(nonExistingEntity), Times.Never);
    }

    [Fact]
    public async Task Delete_ExistingEntity_DeletesEntityInRepository()
    {
        var existingEntity = new Company { Id = Guid.NewGuid() };
        var repository = new Mock<ICompanyRepository>();
        repository.Setup(r => r.Get(existingEntity.Id))
            .ReturnsAsync(existingEntity);
        var service = new TestService(repository.Object);

        await service.Delete(existingEntity.Id);

        repository.Verify(r => r.Delete(existingEntity.Id), Times.Once);
    }

    [Fact]
    public async Task Delete_NonExistingEntity_DoesNotDeleteRepository()
    {
        var nonExistingEntity = new Company { Id = Guid.NewGuid() };
        var repository = new Mock<ICompanyRepository>();
        repository.Setup(r => r.Get(nonExistingEntity.Id))
            .ReturnsAsync((Company)null);
        var service = new TestService(repository.Object);

        await service.Delete(nonExistingEntity.Id);

        repository.Verify(r => r.Delete(nonExistingEntity.Id), Times.Never);
    }
}
