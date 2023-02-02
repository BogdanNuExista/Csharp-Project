namespace School_Stuff2.Features.Models;

public class AssignmentModel : Base.Model
{
    public string Subject { get; set; }
    
    public string Description { get; set; }
    
    public DateTime Deadline { get; set; }
}