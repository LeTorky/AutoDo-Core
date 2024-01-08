public interface ITaskRepository{
    List<TaskItem> ListAllTasksForUser(User user);
    TaskItem CreateTask(string Description, User user);
    TaskItem ModifyTask(int TaskId, string Description, bool Status, User user);
    List<TaskItem> CreateMultipleTasks(List<Tuple<string, bool?>> Tasks, User user);
    bool CreateTasksWithImage(byte[] imageData);
    bool DeleteTask(int TaskId, User user);
}
