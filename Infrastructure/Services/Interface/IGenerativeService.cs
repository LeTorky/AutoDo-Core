public interface IGenerativeService{
    Task UploadImage(IFormFile file, string AccessToken);
}
