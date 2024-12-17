using Bookish_Project.Model;
using Model;

public class UserProgress
{
    public int Id { get; set; }
    public int BookId { get; set; }  
    public int UserId { get; set; }  
    public int PagesRead { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public Book Book { get; set; }  
    public User User { get; set; } 
}
