using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;

namespace FileValidator
{
    public abstract class FileValidatorBase : FileValidatorConfigBase
    {

        public bool ValidateTheFile(FileType fileType,  string fileName, long fileLength)
        {
            switch (fileType)
            {
                case FileType.Document:
                    return ValidateDocumentFile(fileName, fileLength);
                case FileType.Video:
                    return ValidateVideoFile(fileName, fileLength);
                case FileType.Audio:
                    return ValidateAudioFile(fileName, fileLength);
                case FileType.Image:
                    return ValidateImageFile(fileName, fileLength);
                default:
                    return false;
            }
        }

        public bool ValidateDocumentFile(string fileName, long fileLength)
        {
            return ValidateFileType(DocumentMimeTypes, DocumentFileExtensins, fileName) && ValidateFileSize(DocumentMaxBytes, DocumentMinimumBytes, fileLength);
        }
        public bool ValidateImageFile(string fileName, long fileLength)
        {
            return ValidateFileType(ImageMimeTypes, ImageFileExtensions, fileName) && ValidateFileSize(ImageMaximumBytes, ImageMinimumBytes, fileLength);
        }

        public bool ValidateVideoFile(string fileName, long fileLength)
        {
            return ValidateFileType(VideoMimeTypes, VideoFileExtensions, fileName) && ValidateFileSize(VideoMaxBytes, VideoMinimumBytes, fileLength);
        }

        public bool ValidateAudioFile(string fileName, long fileLength)
        {
            return ValidateFileType(AudioMimeTypes, AudioFileExtensions, fileName) && ValidateFileSize(AudioMaxBytes, AudioMinimumBytes, fileLength);
        }

        private bool ValidateFileType(IList<string> mimeTypeList, IList<string> fileExtList, string fileName)
        {
            var extProvider = new FileExtensionContentTypeProvider();
            string contentType = "unknown";
            extProvider.TryGetContentType(fileName, out contentType);
            return mimeTypeList.Any(x => x.ToLower() == contentType.ToLower()) && fileExtList.Any(x => fileName.ToLower().EndsWith(x));
        }
        private bool ValidateFileSize(long maxSize, long minSize, long fileLength)
        {
            return fileLength < maxSize && fileLength > minSize;
        }

    }
}