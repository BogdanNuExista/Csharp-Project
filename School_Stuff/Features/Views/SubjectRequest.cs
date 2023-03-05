using System.ComponentModel.DataAnnotations;
using School_Stuff2.Features.Models;

namespace School_Stuff2.Features.Views;

public class SubjectRequest
{
    [Required]public string Name { get; set; }

    [Required]public string ProfessorMail { get; set; }

    [Required]public List<Double> Grades { get; set; }
    
    [Required]public AssignmentModel Assignment { get; set; }
    
    //[Required]public List<TestModel> Tests { get; set; }
}