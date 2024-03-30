using Azure.Identity;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Graph;
using Microsoft.Graph.Models;

namespace CourseProject.Common._3rdApi.Service
{
    public class OneDrive
    {
        string clientId = "7c24a752-7ac1-4f6e-b73b-ce96f0b85710";
        string clientSecret = "6c764ec6-f8c1-4341-8b69-0cf966959f0c";
        string tenantId = "88fd5b9f-9a9d-4721-8f31-ec12f97b7fd7";

        async Task<GraphServiceClient> GetAuthenticatedGraphClient()
        {
            var scopes = new[] { "User.Read" };

            // For authorization code flow, the user signs into the Microsoft
            // identity platform, and the browser is redirected back to your app
            // with an authorization code in the query parameters
            var authorizationCode = "AUTH_CODE_FROM_REDIRECT";

            // using Azure.Identity;
            var options = new AuthorizationCodeCredentialOptions
            {
                AuthorityHost = AzureAuthorityHosts.AzurePublicCloud,
            };

            // https://learn.microsoft.com/dotnet/api/azure.identity.authorizationcodecredential
            var authCodeCredential = new AuthorizationCodeCredential(
                tenantId, clientId, clientSecret, authorizationCode, options);

            var graphClient = new GraphServiceClient(authCodeCredential, scopes);

            return graphClient;
        }

        public async Task UploadFile(IBrowserFile file)
        {
            GraphServiceClient graphClient = await GetAuthenticatedGraphClient();

            string fileName = file.Name;

            // Tải lên tệp tin
            using (var fileStream = file.OpenReadStream())
            {
                var driveItem = await graphClient.Me.Drive.GetAsync();
                var uploadSession =  await graphClient.Drives[driveItem.Id].Items["root"].Children.GetAsync();


                int maxChunkSize = 320 * 1024; // 320 KB

                var largeFileUploadTask = new LargeFileUploadTask<DriveItem>(uploadSession, fileStream, maxChunkSize);

                var response = await largeFileUploadTask.UploadAsync();
                if (response.UploadSucceeded)
                {
                    Console.WriteLine($"File {fileName} uploaded successfully.");
                }
                else
                {
                    Console.WriteLine($"File upload failed: {response.UploadSucceeded}");
                }
            }

        }
    }
}
