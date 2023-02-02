namespace School_Stuff2.Features.Models;

public class SubjectModel : Base.Model
{
    public string Name { get; set; }

    public string ProfessorMail { get; set; }

    public List<Double> Grades { get; set; }
    
    public AssignmentModel Assignment { get; set; }
    
    public List<TestModel> Tests { get; set; }
}