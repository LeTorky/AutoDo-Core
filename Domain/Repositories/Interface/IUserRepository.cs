public interface IUserRepository{
    bool UserExistsByEmail(string Email);
    void CreateUserWithEmailAsync(string Email);
    User GetUserByEmail(string Email);
}
