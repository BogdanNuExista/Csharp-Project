using School_Stuff2.Features.Models;

namespace School_Stuff2.Features.Views;

public class SubjectResponse
{
    public string Id { get; set; }
    
    public string Name { get; set; }

    public string ProfessorMail { get; set; }

    public List<Double> Grades { get; set; }
    
    public AssignmentModel Assignment { get; set; }
    
    public List<TestModel> Tests { get; set; }
}