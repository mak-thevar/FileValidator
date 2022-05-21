using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using NUnit.Framework;
using System;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;

namespace FileValidator.Tests
{
    public class FormFileValidatorTest
    {
        private string _fileDirectory;

        [OneTimeSetUp]
        public void Setup()
        {
            _fileDirectory = Path.Combine(Environment.CurrentDirectory, "Files");
        }

        [Test]
        public void TestTextFileExpectException()
        { 
            ///Arrange
            var filePath = Path.Combine(_fileDirectory, "f.txt");
            var fileStream = File.OpenRead(filePath);
            IFormFile formFile = new FormFile(fileStream, 0, fileStream.Length, "f.txt", fileStream.Name);
            FileValidatorExtension.UpdateDefaultConfiguration(new FileValidatorConfiguration());

            //Act
            var isValid = formFile.IsValidFile();
            
            //Assert
            Assert.IsFalse(isValid);
        }


        [Test]
        public void TestTextFile()
        {
            //Arrange
            var filePath = Path.Combine(_fileDirectory, "f.txt");
            var fileStream = File.OpenRead(filePath);
            IFormFile formFile = new FormFile(fileStream, 0, fileStream.Length, "f.txt", fileStream.Name);

            var fileValidatorConfig = new FileValidatorConfiguration();
            fileValidatorConfig.DocumentFileExtensins.Add(".txt");
            fileValidatorConfig.DocumentMimeTypes.Add("text/plain");
            FileValidatorExtension.UpdateDefaultConfiguration(fileValidatorConfig);

            //Act
            var isValidFile = formFile.IsValidFile();

            //Assert
            Assert.True(isValidFile);
        }

        [Test]
        public void TestByRemovingAType()
        {
            var fileInfo = new FileInfo(Path.Combine(_fileDirectory, "f.pdf"));
            var fileValidatorConfig = new FileValidatorConfiguration();
            fileValidatorConfig.DocumentFileExtensins.Remove(".pdf");
            FileValidatorExtension.UpdateDefaultConfiguration(fileValidatorConfig);

            //Act
            var isValidFile = fileInfo.IsValidFile(new FileType[] { FileType.Image });

            //Assert
            Assert.IsFalse(isValidFile);
        }
    }
}