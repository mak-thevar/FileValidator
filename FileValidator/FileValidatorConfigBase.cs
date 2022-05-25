namespace FileValidator
{
    public abstract class FileValidatorConfigBase
    {

        public long ImageMinimumBytes { get; set; } = 512;
        public long ImageMaximumBytes { get; set; }
        public long DocumentMinimumBytes { get; set; }
        public long DocumentMaxBytes { get; set; }
        public long VideoMinimumBytes { get; set; }
        public long VideoMaxBytes { get; set; }
        public long AudioMinimumBytes { get; set; }
        public long AudioMaxBytes { get; set; }

        public List<string> ImageMimeTypes { get; set; } = new List<string>
                                   {
                                       "image/jpg",
                                       "image/jpeg",
                                       "image/pjpeg",
                                       "image/gif",
                                       "image/x-png",
                                       "image/png"
                                   };

        public List<string> VideoMimeTypes { get; set; } = new List<string>
        {
            "video/mp4",
            "video/quicktime",
            "video/x-msvideo",
            "video/mpeg",
            "video/3gpp",
            "video/avi"
        };

        public List<string> AudioMimeTypes { get; set; } = new List<string>
        {
            "audio/mpeg",
            "audio/mp4",
            "audio/x-mpegurl",
            "audio/vnd.wav",
            "audio/wave"
        };


        public  List<string> DocumentMimeTypes { get; set; } = new List<string>
        {
            "application/pdf",
            "application/vnd.openxmlformats-officedocument.presentationml.presentation",
            "application/vnd.ms-powerpoint",
            "application/vnd.ms-excel",
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            "application/msword",
            "application/vnd.openxmlformats-officedocument.wordprocessingml.document"
        };


        public List<string> ImageFileExtensions { get; set; } = new List<string>
                                   {
                                       ".jpg",
                                       ".png",
                                       ".gif",
                                       ".jpeg"
                                   };

        public List<string> AudioFileExtensions { get; set; } = new List<string>
                                   {
                                       ".mp3",
                                       ".wav",
                                       ".m3u"
                                   };

        public List<string> VideoFileExtensions { get; set; } = new List<string>
        {
            ".mp4",
            ".3gp",
            ".avi",
            ".mpeg",
            ".mov"
        };

        public List<string> DocumentFileExtensins { get; set; } = new List<string>
        {
            ".doc",
            ".docx",
            ".pdf",
            ".xls",
            ".xlsx",
            ".ppt",
            ".pptx",
        };
    }
}