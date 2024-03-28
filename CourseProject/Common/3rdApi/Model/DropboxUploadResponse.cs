namespace CourseProject.Common._3rdApi.Model
{
    public class DropboxUploadResponse
    {
        public string name { get; set; }
        public string id { get; set; }
        public string client_modified { get; set; }
        public string server_modified { get; set; }
        public long rev { get; set; }
        public long size { get; set; }
        public string path_lower { get; set; }
        public string path_display { get; set; }
    }
}
