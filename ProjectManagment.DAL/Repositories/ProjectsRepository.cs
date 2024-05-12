using ProjectManagment.DAL.Contexts;
using ProjectManagment.DAL.Entities;
using ProjectManagment.DAL.Interfaces;

namespace ProjectManagment.DAL.Repositories;

public sealed class ProjectsRepository(AppDbContext context) : BaseRepository<Project>(context), IProjectRepository;
