using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;

namespace FileValidator
{
    public abstract class FileValidatorBase : FileValidatorConfigBase
    {
        public bool ValidateDocumentFile(FileInfo file)
        {
            return ValidateFileType(DocumentMimeTypes, DocumentFileExtensins, file) && ValidateFileSize(DocumentMaxBytes, DocumentMinimumBytes, file.Length);
        }
        public bool ValidateImageFile(FileInfo file)
        {
            return ValidateFileType(ImageMimeTypes, ImageFileExtensions, file) && ValidateFileSize(ImageMaximumBytes, ImageMinimumBytes, file.Length);
        }

        public bool ValidateVideoFile(FileInfo file)
        {
            return ValidateFileType(VideoMimeTypes, VideoFileExtensions, file) && ValidateFileSize(VideoMaxBytes, VideoMinimumBytes, file.Length);
        }

        public bool ValidateAudioFile(FileInfo file)
        {
            return ValidateFileType(AudioMimeTypes, AudioFileExtensions, file) && ValidateFileSize(AudioMaxBytes, AudioMinimumBytes, file.Length);
        }

        private bool ValidateFileType(IList<string> mimeTypeList, IList<string> fileExtList, FileInfo file)
        {
            var extProvider = new FileExtensionContentTypeProvider();
            string contentType = "unknown";
            extProvider.TryGetContentType(file.FullName, out contentType);
            return mimeTypeList.Any(x => x.ToLower() == contentType.ToLower()) && fileExtList.Any(x => file.FullName.ToLower().EndsWith(x));
        }
        private bool ValidateFileSize(long maxSize, long minSize, long fileLength)
        {
            return fileLength < maxSize && fileLength > minSize;
        }

    }
}