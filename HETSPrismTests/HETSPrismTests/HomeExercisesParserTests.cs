using DataBuilders;
using HETSPrism.Services;
using System;
using Xunit;

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
    }
}



