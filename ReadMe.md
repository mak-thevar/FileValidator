# FileValidator
This library adds an extension method to [FileInfo](https://docs.microsoft.com/en-us/dotnet/api/system.io.fileinfo) & [IFormFile](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.iformfile) to validate the file based on thier configuration(s).


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
#### ⚙️Using the FileInfo Class
```csharp
var fileInfo = new FileInfo("C:/mypath/document.pdf");
var isValid = fileInfo.IsValidFile();
//If you want to only validate the Images then you can pass the allowed FileType[] parameter.
var checkValidImage = fileInfo.IsValidFile(new FileType[] { FileType.Image })
```

#### ⚙️Updating the default configuration (add/remove allowed extensions & MIME types)
```csharp
var fileInfo = new FileInfo("C:/mypath/document.pdf");

//If you want to allow json documents then update the configuration
var fileValidatorConfig = new FileValidatorConfiguration();
fileValidatorConfig.DocumentFileExtensins.Add(".json");
fileValidatorConfig.DocumentMimeTypes.Add("application/json");
FileValidatorExtension.UpdateDefaultConfiguration(fileValidatorConfig);

var isValid = fileInfo.IsValidFile(); //Returns true

//If you want to remove any existing valid extension then you can remove the extension from the configuration
fileValidatorConfig.ImmageFileExtensions.Remove(".gif"); //This will return false on any gif file.
FileValidatorExtension.UpdateDefaultConfiguration(fileValidatorConfig);

```

## Contributing
Contributions are what make the open source community such an amazing place to be learn, inspire, and create. Any contributions you make are greatly appreciated.
Feel free to request for any changes or any additional features.

## License
Distributed under the MIT License. See [LICENSE](https://github.com/mak-thevar/FileValidator/blob/main/LICENSE) for more information.

## Contact
Name: [Muthukumar Thevar](https://www.linkedin.com/in/mak11/)

Email: mak.thevar@outlook.com

Project Link: https://github.com/mak-thevar/FileValidator
