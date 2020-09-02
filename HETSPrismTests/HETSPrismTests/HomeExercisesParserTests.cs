using DataBuilders;
using HETSPrism.Services;
using System;
using Xunit;
using System.IO;
namespace HETSPrismTests
{
    public class HomeExercisesParserTests
    {
        [Fact]
        public void CreateHomeExerciseTest()
        {
            //Arrange
            string path = "C:\\Users\anton\\Desktop\\HETS.Project\\Matala3\\JavaExercises\\Exc1\\excercise1\\495398\\ArithmeticApp.java";
            var parser = new HomeExercisesParser("folder");
            var homeExercise = new HomeExercise() { HomeExerciseName = path };
            
            //Act
            parser.CreateHomeExercise(path);
   

            //Assert
            Assert.Collection<HomeExercise>(parser.HomeExercises, item => item.HomeExercisePath.Contains(path));
        }
        [Fact]
        public void ExtractZipFileTests()
        {
            string path = @"C:\Users\user\Desktop\New folder\_495349_assignsubmission_file_\313410631_308310903.zip";
            string path1 = "C:\\Users\\user\\Desktop\\New folder\\_495349_assignsubmission_file_\\ArithmeticApp.java";
            var parser = new HomeExercisesParser("folder");
            parser.ExtractZipFile(path);
            Assert.False(File.Exists(path));
            Assert.True(File.Exists(path1));
            
        }



        









    }
}



