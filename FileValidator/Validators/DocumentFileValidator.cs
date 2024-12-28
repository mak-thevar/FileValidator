using FileValidator.Config;

namespace FileValidator.Validators;

public class DocumentFileValidator : BaseFileValidator
{
    public DocumentFileValidator(FileValidatorSettings settings) : base(settings)
    {
    }

    public override bool IsValid(string fileName, long fileLength)
    {
        return ValidateFileType(fileName,
                                _settings.DocumentExtensions,
                                _settings.DocumentMimeTypes)
            && ValidateFileSize(fileLength,
                                _settings.DocumentMinBytes,
                                _settings.DocumentMaxBytes);
    }
}