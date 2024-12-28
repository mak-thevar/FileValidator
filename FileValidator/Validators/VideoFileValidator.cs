using FileValidator.Config;

namespace FileValidator.Validators;

public class VideoFileValidator : BaseFileValidator
{
    public VideoFileValidator(FileValidatorSettings settings) : base(settings)
    {
    }

    public override bool IsValid(string fileName, long fileLength)
    {
        return ValidateFileType(fileName,
                                _settings.VideoExtensions,
                                _settings.VideoMimeTypes)
            && ValidateFileSize(fileLength,
                                _settings.VideoMinBytes,
                                _settings.VideoMaxBytes);
    }
}
