using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[ApiController]
[Route("[controller]")]
public class TaskController : Controller
{
    private readonly ITaskRepository _taskRepositroy;
    private readonly IUserRepository _userRepositroy;

    public TaskController(ITaskRepository taskRepository, IUserRepository userRepository)
    {
        _taskRepositroy = taskRepository;
        _userRepositroy = userRepository;
    }
    
    private User _GetUserFromContext(){
        var UserClaim = HttpContext.Items["User"] as ClaimsPrincipal;
        var userEmail = UserClaim?.FindFirst("Email")?.Value;
        return _userRepositroy.GetUserByEmail(userEmail);
    }

    [HttpGet]
    public ActionResult<List<Task>> GetAllTasks()
    {
        var User = _GetUserFromContext();
        var tasks = _taskRepositroy.ListAllTasksForUser(User);
        return Ok(tasks);
    }

    [HttpPost]
    public ActionResult<Task> CreateNewTask(TaskItemDTO TaskEntry)
    {
        var User = _GetUserFromContext();
        var NewTask = _taskRepositroy.CreateTask(TaskEntry.Description, User);
        return Ok(NewTask);
    }

    [HttpPost("Multiple")]
    public ActionResult<Task> CreateNewTasks(TaskItemDTO[] TaskEntry)
    {
        var User = _GetUserFromContext();
        var NewTasks = new List<Tuple<string, bool?>>();
        foreach (var Entry in TaskEntry){
            Entry.Status = false;
            NewTasks.Add(new Tuple<string, bool?>(Entry.Description, Entry.Status));
        }
        _taskRepositroy.CreateMultipleTasks(NewTasks, User);
        return Ok(TaskEntry);
    }

    [HttpDelete]
    public ActionResult<bool> DeleteTask(SpecificTaskItemDTO TaskEntry)
    {
        var User = _GetUserFromContext();
        var DeletedTask = _taskRepositroy.DeleteTask(TaskEntry.TaskId, User);
        return Ok(DeletedTask);
    }

    [HttpPut]
    public ActionResult<Task> UpdateTask(MandatoryTaskItemDTO TaskEntry)
    {
        var User = _GetUserFromContext();
        var UpdatedTask = _taskRepositroy.ModifyTask(TaskEntry.TaskId, TaskEntry.Description, TaskEntry.Status, User);
        return Ok(UpdatedTask);
    }

    [HttpPost("Image")]
    public ActionResult<bool> CreateTasksWithImage([FromForm] IFormFile file){
        var UserClaim = HttpContext.Items["User"] as ClaimsPrincipal;
        var token = UserClaim?.FindFirst("Token")?.Value;
        GenerativeService.UploadImage(file, token);
        return Ok(true);
    }
}
