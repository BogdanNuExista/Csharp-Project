namespace School_Stuff2.Features.Models;

public class TestModel : Base.Model
{
    public string Title { get; set; }

    public DateTime TestDate { get; set; }
    
    public List<SubjectModel> Subjects { get; set; }
}