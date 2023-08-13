namespace Web.Configurations.Helpers
{
    public class FileHelper
    {
        public static string GetContentType(string fileName)
        {
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(fileName, out string contentType))
                contentType = "application/octet-stream";

            return contentType;
        }
    }
}
