using School_Stuff2.Features.Models;

namespace School_Stuff2.Features.Views;

public class TestResponse
{
    public string Id { get; set; }
    
    public string Title { get; set; }

    public DateTime TestDate { get; set; }
    
    public List<SubjectModel> Subjects { get; set; }
}