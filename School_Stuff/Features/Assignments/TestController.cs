using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School_Stuff2.Database;
using School_Stuff2.Features.Models;
using School_Stuff2.Features.Views;

namespace School_Stuff2.Features.Assignments;

[ApiController]
[Route("tests")]
public class TestController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public TestController(AppDbContext dbContext)
    {
        this._dbContext = dbContext;
    }

    [HttpPost]
    public async Task<TestResponse> Add(TestRequest request)
    {
        var test = new TestModel
        {
            Id = Guid.NewGuid().ToString(),
            Created = DateTime.UtcNow,
            Updated = DateTime.UtcNow,
            Title = request.Title,
            TestDate = request.TestDate,
            Subjects = request.Subjects
        };

        var response = await _dbContext.AddAsync(test);
        await _dbContext.SaveChangesAsync();

        return new TestResponse
        {
            Id=response.Entity.Id,
            Title = response.Entity.Title,
            TestDate = response.Entity.TestDate,
            Subjects = response.Entity.Subjects
        };
    }

    [HttpGet]
    public async Task<IEnumerable<TestResponse>> Get()
    {
        var entitties = await _dbContext.Tests.ToListAsync();

        return entitties.Select(
            test => new TestResponse
            {
                Id = test.Id,
                Title = test.Title,
                TestDate = test.TestDate,
                Subjects = test.Subjects
            });
    }

    [HttpGet("{Id}")]
    public async Task<ActionResult<TestResponse>> Get([FromRoute] string id)
    {
        var entity = await _dbContext.Tests.FirstOrDefaultAsync(x => x.Id == id);
        if (entity == null)
            return NotFound("test not found");

        return new TestResponse
        {
            Id = entity.Id,
            Title = entity.Title,
            TestDate = entity.TestDate,
            Subjects = entity.Subjects
        };
    }

    [HttpDelete]
    public async Task<ActionResult<TestResponse>> Delete([FromRoute] string id)
    {
        var entity = await _dbContext.Tests.FirstOrDefaultAsync(x => x.Id == id);
        if (entity is null)
            return NotFound("Test not found");

        _dbContext.Tests.Remove(entity);
        await _dbContext.SaveChangesAsync();

        return new TestResponse
        {
            Id = entity.Id,
            Title = entity.Title,
            TestDate = entity.TestDate,
            Subjects = entity.Subjects
        };
    }

    [HttpPatch]
    public async Task<ActionResult<TestResponse>> Patch([FromRoute] string id,[FromBody] TestRequest req)
    {
        var entity = await _dbContext.Tests.FirstOrDefaultAsync(x => x.Id == id);
        if (entity is null)
            return NotFound("Test not found");

        entity.Title = req.Title;
        entity.TestDate = req.TestDate;
        entity.Subjects = req.Subjects;
        entity.Updated = DateTime.UtcNow;

        await _dbContext.SaveChangesAsync();

        return new TestResponse
        {
            Id = entity.Id,
            Title = entity.Title,
            TestDate = entity.TestDate,
            Subjects = entity.Subjects
        };
    }
}