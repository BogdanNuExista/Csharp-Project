using Microsoft.AspNetCore.Mvc;
using School_Stuff2.Features.Models;
using School_Stuff2.Features.Views;
using Microsoft.EntityFrameworkCore;
using School_Stuff2.Database;

namespace School_Stuff2.Features.Assignments;

[ApiController]
[Route("assignments")]
public class AssignmentController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public AssignmentController(AppDbContext dbContext)
    {
        this._dbContext = dbContext;
    }

    [HttpPost]
    public async Task<AssignmentResponse> Add(AssignmentRequest request)
    {
        var assignment = new AssignmentModel
        {
            Id = Guid.NewGuid().ToString(),
            Created = DateTime.UtcNow,
            Updated = DateTime.UtcNow,
            Subject = request.Subject,
            Description = request.Description,
            Deadline = request.Deadline
        };
        
        var response = await _dbContext.AddAsync(assignment);
        await _dbContext.SaveChangesAsync();

        return new AssignmentResponse
        {
            Id =response.Entity.Id,
            Subject = response.Entity.Subject,
            Description = response.Entity.Description,
            Deadline = response.Entity.Deadline
        };
    }

    [HttpGet]
    public async Task<IEnumerable<AssignmentResponse>> Get()
    {
        var entities = await _dbContext.Assignments.ToListAsync();
        
        return entities.Select(
            assignment => new AssignmentResponse
            {
                Id=assignment.Id,
                Subject = assignment.Subject,
                Description = assignment.Description,
                Deadline = assignment.Deadline
            });
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AssignmentResponse>> Get([FromRoute] string id)
    {
        var entity = await _dbContext.Assignments.FirstOrDefaultAsync(x => x.Id == id);
        if (entity is null)
            return NotFound("Assignment not found");
        
        return new AssignmentResponse
        {
            Id = entity.Id,
            Subject = entity.Subject,
            Description = entity.Description,
            Deadline = entity.Deadline
        };
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<AssignmentResponse>> Delete([FromRoute] string id)
    {
        var entity = await _dbContext.Assignments.FirstOrDefaultAsync(x => x.Id == id);

        if (entity is null)
            return NotFound();
        
        _dbContext.Remove(entity);             
        await _dbContext.SaveChangesAsync();
        
        return new AssignmentResponse
        {
            Id = entity.Id,
            Subject = entity.Subject,
            Description = entity.Description,
            Deadline = entity.Deadline
        };
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult<AssignmentResponse>> Patch([FromRoute] string id,[FromBody] AssignmentRequest @new)
    {
        var entity = await _dbContext.Assignments.FirstOrDefaultAsync(x => x.Id == id);

        if (entity is null)
            return NotFound();
        
        entity.Subject = @new.Subject;
        entity.Description = @new.Description;
        entity.Deadline = @new.Deadline;
        entity.Updated=DateTime.UtcNow;

        await _dbContext.SaveChangesAsync();

        return new AssignmentResponse
        {
            Id=entity.Id,
            Subject = entity.Subject,
            Description = entity.Description,
            Deadline = entity.Deadline
        };
    }
}