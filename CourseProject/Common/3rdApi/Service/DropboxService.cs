namespace CourseProject.Common._3rdApi
{
    using CourseProject.Common._3rdApi.Model;
    using System.Net.Http.Headers;
    using System.Text.Json;
    using System.Threading.Tasks;

    public class DropboxService
    {
        private const string DropboxApiBaseUrl = "https://content.dropboxapi.com/2/files";
        private const string AccessToken = "YOUR_DROPBOX_ACCESS_TOKEN";

        public async Task<string> UploadFileAsync(Stream fileStream, string fileName, string folderPath = "")
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);
                client.DefaultRequestHeaders.Add("Dropbox-API-Arg", JsonSerializer.Serialize(new
                {
                    path = "/" + folderPath + "/" + fileName,
                    mode = "add",
                    autorename = true,
                    mute = false
                }));

                var content = new StreamContent(fileStream);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                var response = await client.PostAsync($"{DropboxApiBaseUrl}/upload", content);
                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync();
                var uploadResponse = JsonSerializer.Deserialize<DropboxUploadResponse>(responseContent);

                return uploadResponse?.path_display;
            }
        }
    }

    
}
