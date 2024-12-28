![Build Status](https://github.com/mak-thevar/FileValidator/actions/workflows/dotnet.yml/badge.svg)
[![LinkedIn](https://img.shields.io/badge/-LinkedIn-black.svg?style=flat-square&logo=linkedin&colorB=555)](https://www.linkedin.com/in/mak11/)

# FileValidator
FileValidator is a .NET library that adds extension methods to both [FileInfo](https://docs.microsoft.com/en-us/dotnet/api/system.io.fileinfo) and [IFormFile](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.iformfile). It helps you verify files against certain rules, such as size limits, allowed extensions, and MIME types. By default, it checks for documents, images, videos, and audio files, but you can easily customize it.




## Installation
- With package manager :
```Install-Package FileValidator```
- With dotnet cli :
```dotnet add package FileValidator```
- Other options and version history :
 https://www.nuget.org/packages/FileValidator

## Usage
#### ⚙️Using IFormFile on ActionMethod
```csharp
  public async Task<IActionResult> OnPostUploadAsync(IFormFile file)
  {
      /*By default it validates all the files like Documents,Images, Videos, Audios*/
      bool isValid = file.IsValidFile();
      if(isValid){
          var filePath = Path.GetTempFileName();
          await file.CopyToAsync(File.OpenWrite(filePath));
          return Ok(new { message = "File uploaded successfully." });
      }
      return BadRequest(new {message = "Invalid file"});
      
  }
```
#### ⚙️Using FileInfo Directly
```csharp
var fileInfo = new FileInfo("C:/mypath/myimage.jpg");
var isValid = fileInfo.IsValidFile();
//If you want to only validate the Images then you can pass the allowed FileType[] parameter.
var checkValidImage = fileInfo.IsValidFile([ FileType.Image ])
```

#### ⚙️Updating the default configuration (You can add or remove file extensions and MIME types as needed)
```csharp
var fileInfo = new FileInfo("C:/mypath/document.pdf");

// Create a configuration and allow JSON as a document type
var fileValidatorConfig = new FileValidatorConfiguration();
fileValidatorConfig.DocumentFileExtensins.Add(".json");
fileValidatorConfig.DocumentMimeTypes.Add("application/json");
FileValidatorExtension.UpdateDefaultConfiguration(fileValidatorConfig);

// Now .json files will pass validation as documents
bool isValid = fileInfo.IsValidFile(); 

// Removing a known file type (for example, GIF in images)
fileValidatorConfig.ImmageFileExtensions.Remove(".gif");
FileValidatorExtension.UpdateDefaultConfiguration(fileValidatorConfig);

// GIF files will now fail validation

```

## Contributing
Contributions are what make the open source community such an amazing place to be learn, inspire, and create. Any contributions you make are greatly appreciated.
Feel free to request for any changes or any additional features.

## License
Distributed under the MIT License. See [LICENSE](https://github.com/mak-thevar/FileValidator/blob/main/LICENSE) for more information.

## Contact
- Name: [Muthukumar Thevar](https://www.linkedin.com/in/mak11/)
- Email: mak.thevar@outlook.com
- Portfolio: https://mak-thevar.dev
- Project Link: https://github.com/mak-thevar/FileValidator
