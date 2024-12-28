using FileValidator.Config;

namespace FileValidator.Validators;

public class ImageFileValidator : BaseFileValidator
{
    public ImageFileValidator(FileValidatorSettings settings) : base(settings)
    {
    }

    public override bool IsValid(string fileName, long fileLength)
    {
        return ValidateFileType(fileName,
                                _settings.ImageExtensions,
                                _settings.ImageMimeTypes)
            && ValidateFileSize(fileLength,
                                _settings.ImageMinBytes,
                                _settings.ImageMaxBytes);
    }
}