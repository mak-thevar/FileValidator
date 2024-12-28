using FileValidator.Config;
using Microsoft.AspNetCore.StaticFiles;

namespace FileValidator.Validators;

/// <summary>
/// Base class that all specific file validators can extend.
/// </summary>
public abstract class BaseFileValidator
{
    protected readonly FileValidatorSettings _settings;

    protected BaseFileValidator(FileValidatorSettings settings)
    {
        _settings = settings;
    }

    /// <summary>
    /// Validates the provided file name and length.
    /// </summary>
    /// <param name="fileName">Name of the file with extension.</param>
    /// <param name="fileLength">File size in bytes.</param>
    /// <returns>True if valid, else false.</returns>
    public abstract bool IsValid(string fileName, long fileLength);

    /// <summary>
    /// Compares file extension and mime type to known lists.
    /// </summary>
    protected bool ValidateFileType(
        string fileName,
        List<string> validExtensions,
        List<string> validMimeTypes)
    {
        var extensionProvider = new FileExtensionContentTypeProvider();
        if (!extensionProvider.TryGetContentType(fileName, out string contentType))
        {
            // Fallback if unrecognized
            contentType = "application/octet-stream";
        }

        var ext = System.IO.Path.GetExtension(fileName)?.ToLower() ?? string.Empty;
        bool hasValidExtension = validExtensions.Contains(ext);
        bool hasValidMimeType = validMimeTypes.Exists(m => m.Equals(contentType, System.StringComparison.OrdinalIgnoreCase));

        return hasValidExtension && hasValidMimeType;
    }

    /// <summary>
    /// Checks if the file's size is within the allowed range.
    /// </summary>
    protected bool ValidateFileSize(long fileLength, long minBytes, long maxBytes)
    {
        return fileLength >= minBytes && fileLength <= maxBytes;
    }
}

