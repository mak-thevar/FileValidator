using FileValidator.Config;
using FileValidator.Enums;
using FileValidator.Services;
using Microsoft.AspNetCore.Http;

namespace FileValidator.Extensions;

public static class FileValidatorExtensions
{
    private static FileValidatorService _service
        = new FileValidatorService(DefaultFileValidatorSettings.GetDefaults());

    /// <summary>
    /// Updates the default configuration used for validation.
    /// </summary>
    public static void UpdateDefaultConfiguration(FileValidatorSettings newSettings)
    {
        _service = new FileValidatorService(newSettings);
    }

    /// <summary>
    /// Validates an IFormFile, optionally restricting which file types are allowed.
    /// </summary>
    public static bool IsValidFile(this IFormFile file, FileType[] allowedFileTypes = null)
    {
        return _service.ValidateFile(file.FileName, file.Length, allowedFileTypes);
    }

    /// <summary>
    /// Validates a FileInfo object, optionally restricting which file types are allowed.
    /// </summary>
    public static bool IsValidFile(this FileInfo fileInfo, FileType[] allowedFileTypes = null)
    {
        return _service.ValidateFile(fileInfo.Name, fileInfo.Length, allowedFileTypes);
    }
}
