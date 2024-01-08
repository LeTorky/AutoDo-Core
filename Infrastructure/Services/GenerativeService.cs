using System.Net.Http.Headers;

class GenerativeService{
public static async Task UploadImage(IFormFile file, string AccessToken)
    {
        string serviceUrl = "http://localhost:8000/image/list/";

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
