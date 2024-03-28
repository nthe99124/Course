using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Components.Forms;

namespace CourseProject.Common._3rdApi.Service
{
    public class AmazonS3
    {
        public async Task<string> UploadToS3(IBrowserFile file)
        {
            // Sử dụng SDK của AWS để tải tệp lên S3
            // Đây là một ví dụ đơn giản, bạn cần thay thế nó bằng mã thực tế
            // Xem thêm tài liệu AWS SDK để biết cách sử dụng
            // https://docs.aws.amazon.com/sdk-for-net/latest/developer-guide/s3-apis-intro.html
            // ẩn key để múc sang github, azure nó check
            var BucketName = "";
            var s3Client = new AmazonS3Client();

            using (var client = s3Client)
            {
                using (var stream = file.OpenReadStream(150000000))
                {
                    var request = new Amazon.S3.Model.PutObjectRequest
                    {
                        BucketName = BucketName,
                        Key = Guid.NewGuid().ToString() + Path.GetExtension(file.Name),
                        InputStream = stream,
                        ContentType = file.ContentType
                    };

                    var response = await client.PutObjectAsync(request);
                    if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var link =  $"https://{BucketName}.s3.amazonaws.com/{request.Key}";
                        var linkVideo = await GetVideoFromS3Url(link);
                        return linkVideo;
                    }
                    else
                    {
                        throw new Exception("Upload failed.");
                    }
                }
            }
           
        }

        static async Task<string> GetVideoFromS3Url(string s3VideoUrl)
        {
            // ẩn key để múc sang github, azure nó check
            var s3Client = new AmazonS3Client();
            try
            {
                // Lấy tên bucket và khóa đối tượng (object key) từ URL của video trên S3
                Uri uri = new Uri(s3VideoUrl);
                string objectKey = uri.AbsolutePath.TrimStart('/');

                var bucketName = "";
                // Tạo yêu cầu để lấy đối tượng từ bucket S3
                GetObjectRequest request = new GetObjectRequest
                {
                    BucketName = bucketName,
                    Key = objectKey
                };

                // Thực hiện yêu cầu để lấy đối tượng từ bucket S3
                GetObjectResponse response = await s3Client.GetObjectAsync(request);

                // Tạo mã HTML cho thẻ <video> với URL của video từ S3
                string videoHtml = $"<video controls><source src=\"{s3VideoUrl}\" type=\"video/mp4\">Your browser does not support the video tag.</video>";

                return videoHtml;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Đã xảy ra lỗi khi tải video từ Amazon S3: " + ex.Message);
                return null;
            }
        }
    }
}
