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
            return FileValidator(file.FileName, file.Length, allowedFileTypes);
        }

        public static bool IsValidFile(this FileInfo fileInfo, FileType[]? allowedFileTypes = null)
        {
            return FileValidator(fileInfo.Name, fileInfo.Length, allowedFileTypes);
        }

        private static bool FileValidator(string fileName,long fileLength, FileType[]? allowedFileTypes = null)
        {
            var fileType = DetermineTheFileType(fileName);
            if (fileType is null)
                return false;

            if (allowedFileTypes is not null)
                if (!allowedFileTypes.Contains(fileType.Value))
                    return false;

            return validatorConfig.ValidateTheFile(fileType.Value, fileName, fileLength);
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