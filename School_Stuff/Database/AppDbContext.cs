using Microsoft.EntityFrameworkCore;
using School_Stuff2.Features.Models;

namespace School_Stuff2.Database;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options){}

    public DbSet<AssignmentModel> Assignments { get; set; } = null!;

    public DbSet<SubjectModel> Subjects { get; set; } = null!;

    public DbSet<TestModel> Tests { get; set; } = null!;

}