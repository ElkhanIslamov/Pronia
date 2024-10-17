namespace Pronia.Helpers.Extensions
{
    public static class FileExtension
    {
<<<<<<< HEAD
        public static bool CheckFileType(this IFormFile file ,string fileType) 
            =>file.ContentType.Contains(fileType);
        public static bool CheckFileSize(this IFormFile file ,int size)
            =>file.Length/1024<size;
=======
        public static bool CheckFileType(this IFormFile file , string fileType)
            =>file.ContentType.Contains(fileType);
        public static bool CheckFileSize(this IFormFile file, int size)
            => file.Length / 1024 < size;
>>>>>>> 57991215b1568482bb7c3fa294a1a116d2b1b740
    }
}
