using System.ComponentModel.DataAnnotations;

public class User{
    [Key]
    public int UserId {get; set;}
    public string Email {get; set;}
    public HashSet<TaskItem> Tasks{get; set;}
}
