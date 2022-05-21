using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System.Linq;

namespace FileValidator
{
    public static class FileValidatorExtension
    {

        private static FileValidatorConfiguration validatorConfig = new FileValidatorConfiguration();
        public static void UpdateDefaultConfiguration(FileValidatorConfiguration configuration)
        {
            validatorConfig = configuration;
        }
        public static bool IsValidFile(this IFormFile file, FileType[]? allowedFileTypes = null)
        {
            return IsValidFile(new FileInfo(file.FileName), allowedFileTypes);
        }

        public static bool IsValidFile(this FileInfo fileInfo, FileType[]? allowedFileTypes = null)
        {

            var fileType = DetermineTheFileType(fileInfo.FullName);
            if(fileType is null)
                return false;

            if (allowedFileTypes is not null)
                if (!allowedFileTypes.Contains(fileType.Value))
                    return false;

            switch (fileType.Value)
            {
                case FileType.Document:
                    return validatorConfig.ValidateDocumentFile(fileInfo);
                case FileType.Video:
                    return validatorConfig.ValidateVideoFile(fileInfo);
                case FileType.Audio:
                    return validatorConfig.ValidateAudioFile(fileInfo);
                case FileType.Image:
                    return validatorConfig.ValidateImageFile(fileInfo);
                default:
                    return false;
            }
        }

        private static FileType? DetermineTheFileType(string fileName)
        {
            var allFileExtLists = new Dictionary<FileType?, List<string>>
            {
                { FileType.Document, validatorConfig.DocumentFileExtensins },
                { FileType.Image, validatorConfig.ImageFileExtensions },
                { FileType.Video, validatorConfig.VideoFileExtensions },
                { FileType.Audio, validatorConfig.AudioFileExtensions }
            };
            var currentFileExt = Path.GetExtension(fileName);

            var currentMimeType = allFileExtLists.Where(x => x.Value.Any(f => f == currentFileExt)).FirstOrDefault();

            return currentMimeType.Key;

        }
    }
}