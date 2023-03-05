using System.ComponentModel.DataAnnotations;
using School_Stuff2.Features.Models;

namespace School_Stuff2.Features.Views;

public class TestRequest
{
    [Required]public string Title { get; set; }

    [Required]public DateTime TestDate { get; set; }
    
    //[Required]public List<SubjectModel> Subjects { get; set; }
}