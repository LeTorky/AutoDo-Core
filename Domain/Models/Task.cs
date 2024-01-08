using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class TaskItem{
    [Key]
    public int TaskId {get; set;}
    public string Description {get; set;}
    public bool Status {get; set;}
    public int UserId {get; set;}
    [ForeignKey("UserId")]
    public User User {get; set;}
}
