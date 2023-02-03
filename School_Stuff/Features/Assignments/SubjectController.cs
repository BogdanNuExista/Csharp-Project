using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School_Stuff2.Database;
using School_Stuff2.Features.Models;
using School_Stuff2.Features.Views;

namespace School_Stuff2.Features.Assignments;

[ApiController]
[Route("subjects")]
public class SubjectController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public SubjectController(AppDbContext dbContext)
    {
        this._dbContext = dbContext;
    }

    [HttpPost]
    public async Task<SubjectResponse> Add(SubjectRequest request)
    {
        var subject = new SubjectModel
        {
            Id = Guid.NewGuid().ToString(),
            Created = DateTime.UtcNow,
            Updated = DateTime.UtcNow,
            Name = request.Name,
            ProfessorMail = request.ProfessorMail,
            Grades = request.Grades,
            Assignment = request.Assignment,
            Tests = request.Tests
        };

        var response = await _dbContext.AddAsync(subject);
        await _dbContext.SaveChangesAsync();

        return new SubjectResponse
        {
            Id = response.Entity.Id,
            Name = response.Entity.Name,
            ProfessorMail = response.Entity.ProfessorMail,
            Grades = response.Entity.Grades,
            Assignment = response.Entity.Assignment,
            Tests = response.Entity.Tests
        };
    }

    [HttpGet]
    public async Task<IEnumerable<SubjectResponse>> Get()
    {
        var entities = await _dbContext.Subjects.ToListAsync();

        return entities.Select(
            subject => new SubjectResponse
            {
                Id = subject.Id,
                Name = subject.Name,
                ProfessorMail = subject.ProfessorMail,
                Grades = subject.Grades,
                Assignment = subject.Assignment,
                Tests = subject.Tests
            });
    }

    [HttpGet("{Id}")]
    public async Task<ActionResult<SubjectResponse>> Get([FromRoute] string id)
    {
        var entity = await _dbContext.Subjects.FirstOrDefaultAsync(x => x.Id == id);
        if (entity is null)
            return NotFound("subject not found");

        return new SubjectResponse
        {
            Id = entity.Id,
            Name= entity.Name,
            ProfessorMail = entity.ProfessorMail,
            Grades = entity.Grades,
            Assignment = entity.Assignment,
            Tests = entity.Tests
        };
    }

    [HttpDelete]
    public async Task<ActionResult<SubjectResponse>> Delete([FromRoute] string id)
    {
        var entity =await _dbContext.Subjects.FirstOrDefaultAsync(x => x.Id == id);
        if (entity is null)
            return NotFound("Subject not found");

        _dbContext.Subjects.Remove(entity);
        await _dbContext.SaveChangesAsync();

        return new SubjectResponse
        {
            Id = entity.Id,
            Name = entity.Name,
            ProfessorMail = entity.ProfessorMail,
            Grades = entity.Grades,
            Assignment = entity.Assignment,
            Tests = entity.Tests
        };
    }

    [HttpPatch]
    public async Task<ActionResult<SubjectResponse>> Patch([FromRoute] string id, [FromBody] SubjectRequest req)
    {
        var entity = await _dbContext.Subjects.FirstOrDefaultAsync(x => x.Id == id);
        if (entity is null)
            return NotFound("Subject not found");

        entity.Name = req.Name;
        entity.ProfessorMail = req.ProfessorMail;
        entity.Grades = req.Grades;
        entity.Assignment = req.Assignment;
        entity.Tests = req.Tests;
        entity.Updated = DateTime.UtcNow;

        await _dbContext.SaveChangesAsync();

        return new SubjectResponse()
        {
            Id=entity.Id,
            Name=entity.Name,
            ProfessorMail = entity.ProfessorMail,
            Grades = entity.Grades,
            Assignment = entity.Assignment,
            Tests = entity.Tests
        };
    }
}