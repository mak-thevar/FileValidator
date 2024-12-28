using FileValidator.Config;

namespace FileValidator.Validators;

public class AudioFileValidator : BaseFileValidator
{
    public AudioFileValidator(FileValidatorSettings settings) : base(settings)
    {
    }

    public override bool IsValid(string fileName, long fileLength)
    {
        return ValidateFileType(fileName,
                                _settings.AudioExtensions,
                                _settings.AudioMimeTypes)
            && ValidateFileSize(fileLength,
                                _settings.AudioMinBytes,
                                _settings.AudioMaxBytes);
    }
}