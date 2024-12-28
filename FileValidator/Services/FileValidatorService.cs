using FileValidator.Config;
using FileValidator.Enums;
using FileValidator.Validators;

namespace FileValidator.Services;

/// <summary>
/// Coordinates which specific validator to use based on the file type.
/// </summary>
public class FileValidatorService
{
    private readonly FileValidatorSettings _settings;
    private readonly Dictionary<FileType, BaseFileValidator> _validators;

    public FileValidatorService(FileValidatorSettings settings)
    {
        _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        _validators = new Dictionary<FileType, BaseFileValidator>
            {
                { FileType.Document, new DocumentFileValidator(_settings) },
                { FileType.Image, new ImageFileValidator(_settings) },
                { FileType.Video, new VideoFileValidator(_settings) },
                { FileType.Audio, new AudioFileValidator(_settings) }
            };
    }

    /// <summary>
    /// Validates the file by type. Returns false if extension is unknown or if validation fails.
    /// </summary>
    public bool ValidateFile(string fileName, long fileLength, FileType[] allowedFileTypes = null)
    {
        FileType determinedType = DetermineFileType(fileName);

        if (determinedType == FileType.Unknown)
            return false;

        // If user specified allowed file types, confirm the file is in that list.
        if (allowedFileTypes != null && !allowedFileTypes.Contains(determinedType))
            return false;

        return _validators[determinedType].IsValid(fileName, fileLength);
    }

    /// <summary>
    /// Figures out the file type from extension alone. 
    /// If no match is found, returns Unknown.
    /// </summary>
    private FileType DetermineFileType(string fileName)
    {
        string ext = System.IO.Path.GetExtension(fileName)?.ToLower() ?? string.Empty;

        if (_settings.DocumentExtensions.Contains(ext)) return FileType.Document;
        if (_settings.ImageExtensions.Contains(ext)) return FileType.Image;
        if (_settings.VideoExtensions.Contains(ext)) return FileType.Video;
        if (_settings.AudioExtensions.Contains(ext)) return FileType.Audio;

        return FileType.Unknown;
    }
}