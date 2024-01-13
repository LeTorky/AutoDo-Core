using System.Net.Http.Headers;

public class GenerativeService:IGenerativeService{
private IConfiguration configuration;
public GenerativeService(IConfiguration configuration){
    this.configuration = configuration;
}
public async Task UploadImage(IFormFile file, string AccessToken)
    {
        string serviceUrl = configuration["Services:AI:Domain"] + "image/list/";

        using (var httpClient = new HttpClient())
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("AccessToken", AccessToken);

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                ByteArrayContent byteContent = new ByteArrayContent(stream.ToArray());

                byteContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);

                using (var formData = new MultipartFormDataContent())
                {
                    formData.Add(byteContent, "image", file.FileName); 

                    HttpResponseMessage response = await httpClient.PostAsync(serviceUrl, formData);
                }
            }
        }
    }
}
