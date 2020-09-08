using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using DataBuilders;
using IOTestModule.Services;

namespace IOTestModuleTests
{
    public class RunTestTests
    {
        [Theory]
        [InlineData("C:\\Users\anton\\Desktop\\HETS.Project\\Matala3\\JavaExercises\\Exc1\\excercise1\\_495418_assignsubmission_file_\\ArithmeticApp.java")]
        [InlineData("C:\\Users\\anton\\Desktop\\HETS.Project\\Matala3\\JavaExercises\\Exc1\\495418\\308073535_039040498\\ArithmeticApp.java")]
        [InlineData("C:\\Users\\anton\\Desktop\\HETS.Project\\Matala3\\JavaExercises\\Exc1\\excercise1\\_495378_assignsubmission_file_\\ArithmeticApp.java")]
        public async void StartRunTestOverAllSuccessTest(string homeExercisePath1)
        {
            //Arrange
            HomeExercise homeExercise1 = new HomeExercise() 
                { HomeExercisePath = homeExercisePath1 };

            List<HomeExercise> homeExercises = new List<HomeExercise>();
            homeExercises.Add(homeExercise1);


            string inputFilePath = @"C:\Users\anton\Desktop\HETS.Project\Matala3\JavaExercises\Exc1\HW1-inout\in1.txt";
            string outputFilePath = @"C:\Users\anton\Desktop\HETS.Project\Matala3\JavaExercises\Exc1\HW1-inout\out1.txt";
            string input = InputOutputParser.Parser(inputFilePath);
            string output = InputOutputParser.Parser(outputFilePath);
            InputOutputModel inputOutputModel = new InputOutputModel(){InputText = input, OutputText = output};
            List<InputOutputModel> inputOutputModels = new List<InputOutputModel>();
            inputOutputModels.Add(inputOutputModel);

            int secondsToWait = 5;
            //Act
            await RunTest.StartRunTest(homeExercises, inputOutputModels, secondsToWait);
            //Assert
            Assert.NotNull(homeExercise1.IsRunTestOk);
        }
        [Fact]
        public void InputOutputParserSuccessTest()
        {
            //Arrange
            string expected = " - 1 + 5 * (6-2 ) + 18/(10-4)";
            string filePath = @"C:\Users\anton\Desktop\HETS.Project\Matala3\JavaExercises\Exc1\HW1-inout\in1.txt";
            //Act
            string output = InputOutputParser.Parser(filePath);
            //Assert
            Assert.Contains(expected, output);
        }
    }
}



