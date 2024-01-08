using Microsoft.EntityFrameworkCore;

public class UserRepositoryV1 : IUserRepository{
    private readonly TaskDBContext _dbContext;
    public UserRepositoryV1(TaskDBContext dbContext){
        _dbContext = dbContext;
    }
    public bool UserExistsByEmail(string Email){
        var UserExists = _dbContext.Users.Any(user=>user.Email == Email);
        return UserExists ? true : false;
    }
    public void CreateUserWithEmailAsync(string Email){
        var newUser = new User{
            Email = Email
        };
        _dbContext.Users.Add(newUser);
        _dbContext.SaveChanges();
    }
    public User GetUserByEmail(string Email){
        var user = _dbContext.Users.FirstOrDefault(user=>user.Email == Email);
        return user; 
    }
}
