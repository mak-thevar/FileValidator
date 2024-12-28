namespace FileValidator.Config;

/// <summary>
/// Holds custom settings for file validation, such as sizes and allowed formats.
/// </summary>
public class FileValidatorSettings
{
    // Document
    public long DocumentMinBytes { get; set; }
    public long DocumentMaxBytes { get; set; }
    public List<string> DocumentExtensions { get; set; }
    public List<string> DocumentMimeTypes { get; set; }

    // Image
    public long ImageMinBytes { get; set; }
    public long ImageMaxBytes { get; set; }
    public List<string> ImageExtensions { get; set; }
    public List<string> ImageMimeTypes { get; set; }

    // Video
    public long VideoMinBytes { get; set; }
    public long VideoMaxBytes { get; set; }
    public List<string> VideoExtensions { get; set; }
    public List<string> VideoMimeTypes { get; set; }

    // Audio
    public long AudioMinBytes { get; set; }
    public long AudioMaxBytes { get; set; }
    public List<string> AudioExtensions { get; set; }
    public List<string> AudioMimeTypes { get; set; }
}