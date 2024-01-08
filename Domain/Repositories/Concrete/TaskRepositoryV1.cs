public class TaskRepositoryV1:ITaskRepository{
    private TaskDBContext _dbContext;
    public TaskRepositoryV1(TaskDBContext dbContext){
        _dbContext = dbContext;
    }
    public List<TaskItem> ListAllTasksForUser(User user){
        return _dbContext.TaskItems.Where(task=>user.UserId == user.UserId).ToList();
    }
    public TaskItem CreateTask(string Description, User user){
        TaskItem NewTask = new TaskItem{
            Description=Description,
            Status=false,
            User=user
        };
        _dbContext.TaskItems.Add(NewTask);
        _dbContext.SaveChanges();
        return NewTask;
    }
    public TaskItem ModifyTask(int TaskId, string Description, bool Status, User user){
        TaskItem EditTask = _dbContext.TaskItems.FirstOrDefault(task=>task.TaskId==TaskId && task.User == user);
        if(EditTask != null){
            EditTask.Description = Description;
            EditTask.Status = Status;
            _dbContext.SaveChanges();
        }
        return EditTask;
    }
    public List<TaskItem> CreateMultipleTasks(List<Tuple<string, bool?>> Tasks, User user){
        List<TaskItem> NewTasks = new List<TaskItem>();
        foreach(var Item in Tasks){
            var status = Item.Item2 == true ? true : false;
            var NewTask = new TaskItem{
                Description=Item.Item1,
                Status=status,
                User=user
            };
            _dbContext.TaskItems.Add(NewTask);
            NewTasks.Add(NewTask);
        }
        _dbContext.SaveChanges();
        return NewTasks;
    }
    public bool CreateTasksWithImage(byte[] imageData){
        return true;
    }
    public bool DeleteTask(int TaskId, User user){
        var TaskToDelete = _dbContext.TaskItems.FirstOrDefault(task=> task.TaskId == TaskId && task.User == user);
        if(TaskToDelete != null){
            _dbContext.TaskItems.Remove(TaskToDelete);
            _dbContext.SaveChanges();
            return true;
        }
        return false;
    }
}
