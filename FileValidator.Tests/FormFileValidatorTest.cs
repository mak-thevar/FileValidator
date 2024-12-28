using FileValidator.Config;
using FileValidator.Enums;
using FileValidator.Extensions;
using FileValidator.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using NUnit.Framework;
using System;
using System.IO;

namespace FileValidator.Tests;

[TestFixture]
public class FormFileValidatorTest
{
    private string _fileDirectory;

    [OneTimeSetUp]
    public void Setup()
    {
        _fileDirectory = Path.Combine(Environment.CurrentDirectory, "Files");
    }

    [Test]
    public void ShouldReturnFalseForTxtWhenNotConfigured()
    {
        // Arrange
        var filePath = Path.Combine(_fileDirectory, "f.txt");
        var fileStream = File.OpenRead(filePath);
        IFormFile formFile = new FormFile(fileStream, 0, fileStream.Length, "f.txt", fileStream.Name);

        // Load default settings (no .txt or text/plain included)
        var defaultSettings = DefaultFileValidatorSettings.GetDefaults();
        FileValidatorExtensions.UpdateDefaultConfiguration(defaultSettings);

        //Act
        var isValid = formFile.IsValidFile();
        
        //Assert
        Assert.That(isValid == false);
    }


    [Test]
    public void ShouldValidateTxtWhenConfigured()
    {
        // Arrange
        var filePath = Path.Combine(_fileDirectory, "f.txt");
        var fileStream = File.OpenRead(filePath);
        IFormFile formFile = new FormFile(fileStream, 0, fileStream.Length, "f.txt", fileStream.Name);

        // Get defaults and add .txt, text/plain to document settings
        var customSettings = DefaultFileValidatorSettings.GetDefaults();
        customSettings.DocumentExtensions.Add(".txt");
        customSettings.DocumentMimeTypes.Add("text/plain");
        FileValidatorExtensions.UpdateDefaultConfiguration(customSettings);

        // Act
        var isValidFile = formFile.IsValidFile();

        // Assert
        Assert.That(isValidFile);
    }

    [Test]
    public void ShouldInvalidateFileWhenExtensionRemoved()
    {
        // Arrange
        var fileInfo = new FileInfo(Path.Combine(_fileDirectory, "f.pdf"));
        var customSettings = DefaultFileValidatorSettings.GetDefaults();

        // Remove .pdf from valid document extensions
        customSettings.DocumentExtensions.Remove(".pdf");
        FileValidatorExtensions.UpdateDefaultConfiguration(customSettings);

        // Act
        // Pass FileType.Image to confirm .pdf is no longer accepted
        var isValidFile = fileInfo.IsValidFile(new[] { FileType.Image });

        // Assert
        Assert.That(isValidFile == false);
    }

    [Test]
    public void ShouldFailWhenExceedingMaxSize()
    {
        // Arrange
        var fileInfo = new FileInfo(Path.Combine(_fileDirectory, "f.txt"));
        var customSettings = DefaultFileValidatorSettings.GetDefaults();

        // Restrict document max size to 100 bytes
        customSettings.DocumentMaxBytes = 100;
        FileValidatorExtensions.UpdateDefaultConfiguration(customSettings);

        // Act
        var isValidFile = fileInfo.IsValidFile();

        // Assert
        Assert.That(isValidFile == false);
    }

    [Test]
    public void ShouldFailWhenFileIsEmpty()
    {
        // Arrange
        // Create a FormFile with zero length
        var emptyFile = new FormFile(Stream.Null, 0, 0, "empty.txt", "empty.txt");

        // Load or adjust default settings (still has minimum byte requirement)
        var customSettings = DefaultFileValidatorSettings.GetDefaults();
        FileValidatorExtensions.UpdateDefaultConfiguration(customSettings);

        // Act
        bool isValidFile = emptyFile.IsValidFile();

        // Assert
        Assert.That(isValidFile == false, "Empty files should not be valid with the default minimum size.");
    }

    [Test]
    public void ShouldValidateOnlyDocuments()
    {
        // Arrange
        // Create a FormFile with zero length
        var emptyFile = new FormFile(Stream.Null, 0, 0, "empty.txt", "empty.txt");
        var documentFileType = new[] { FileType.Document };
        var documentFile = new FileInfo(Path.Combine(_fileDirectory, "f.pdf"));

        // Act
        bool emptyFileIsValid = emptyFile.IsValidFile();
        bool documentIsVaid = documentFile.IsValidFile(documentFileType);



        // Assert
        Assert.That(documentIsVaid, "Document files should be valid.");
        Assert.That(emptyFileIsValid == false, "Empty files should not be valid with the default minimum size.");
    }
}