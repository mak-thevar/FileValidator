namespace FileValidator.Config;

/// <summary>
/// Supplies default values for file validation.
/// </summary>
public static class DefaultFileValidatorSettings
{
    public static FileValidatorSettings GetDefaults()
    {
        return new FileValidatorSettings
        {
            // Document defaults
            DocumentMinBytes = 100,
            DocumentMaxBytes = 10 * 1024 * 1024, // 10 MB
            DocumentExtensions = new List<string>
                {
                    ".doc", ".docx", ".pdf", ".xls", ".xlsx",
                    ".ppt", ".pptx"
                },
            DocumentMimeTypes = new List<string>
                {
                    "application/pdf",
                    "application/vnd.openxmlformats-officedocument.presentationml.presentation",
                    "application/vnd.ms-powerpoint",
                    "application/vnd.ms-excel",
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    "application/msword",
                    "application/vnd.openxmlformats-officedocument.wordprocessingml.document"
                },

            // Image defaults
            ImageMinBytes = 512,
            ImageMaxBytes = 6 * 1024 * 1024, // 6 MB
            ImageExtensions = new List<string>
                {
                    ".jpg", ".jpeg", ".png", ".gif"
                },
            ImageMimeTypes = new List<string>
                {
                    "image/jpg", "image/jpeg", "image/pjpeg",
                    "image/gif", "image/x-png", "image/png"
                },

            // Video defaults
            VideoMinBytes = 1 * 1024 * 1024,  // 1 MB
            VideoMaxBytes = 50 * 1024 * 1024, // 50 MB
            VideoExtensions = new List<string>
                {
                    ".mp4", ".3gp", ".avi", ".mpeg", ".mov"
                },
            VideoMimeTypes = new List<string>
                {
                    "video/mp4", "video/quicktime", "video/x-msvideo",
                    "video/mpeg", "video/3gpp", "video/avi"
                },

            // Audio defaults
            AudioMinBytes = 10 * 1024,       // 10 KB
            AudioMaxBytes = 5 * 1024 * 1024, // 5 MB
            AudioExtensions = new List<string>
                {
                    ".mp3", ".wav", ".m3u"
                },
            AudioMimeTypes = new List<string>
                {
                    "audio/mpeg", "audio/mp4", "audio/x-mpegurl",
                    "audio/vnd.wav", "audio/wave"
                }
        };
    }
}
