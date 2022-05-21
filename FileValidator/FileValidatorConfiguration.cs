namespace FileValidator
{
    public class FileValidatorConfiguration : FileValidatorBase
    {

        /// <summary>
        /// Configure the min and max size to be allowed by the validator for different file types.
        /// </summary>
        /// <param name="imageMinBytes">Default 512 bytes</param>
        /// <param name="imageMaxBytes">Default 6Mb</param>
        /// <param name="documentMinBytes">Default 100 bytes</param>
        /// <param name="documentMaxBytes">Default 1 Mb</param>
        /// <param name="videoMinBytes">Default 1 Mb</param>
        /// <param name="videoMaxBytes">Default 50 Mb</param>
        /// <param name="audioMaxBytes">Default 5 Mb</param>
        /// <param name="audioMinBytes">Default 10 Kb</param>
        public FileValidatorConfiguration(int imageMinBytes = 512, int imageMaxBytes = 6 * 1024 * 1024, int documentMinBytes = 100, int documentMaxBytes = 10 * 1024 * 1024, int videoMinBytes = 1024 * 1024, int videoMaxBytes = 50 * 1024 * 1024, int audioMaxBytes = 1024 * 10, int audioMinBytes = 5 * 1024 * 1024)
        {
            this.ImageMinimumBytes = imageMinBytes;
            this.ImageMaximumBytes = imageMaxBytes;
            this.DocumentMinimumBytes = documentMinBytes;
            this.DocumentMaxBytes = documentMaxBytes;
            this.VideoMinimumBytes = videoMinBytes;
            this.VideoMaxBytes = videoMaxBytes;
            this.AudioMaxBytes = audioMaxBytes;
            this.AudioMinimumBytes = audioMinBytes;
        }
    }
}