using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DataBuilders;
using Xunit;
using HETSPrism.Services;

namespace HETSPrismTests
{
    public class CompilationTestTests
    {
        [Fact]
        public async void StartCompilationTestOverallSuccessTest()
        {
            //Arrange
            HomeExercise homeExercise1 = new HomeExercise(){HomeExercisePath = @"C:\Users\anton\Desktop\HETS.Project\Matala3\JavaExercises\Exc1\excercise1\_495418_assignsubmission_file_\ArithmeticApp.java" };
            List<HomeExercise> homeExercises = new List<HomeExercise>();
            homeExercises.Add(homeExercise1);
            //Act
            await CompilationTest.StartCompilationTest(homeExercises);
            //Assert
            Assert.True(File.Exists(@"C:\Users\anton\Desktop\HETS.Project\Matala3\JavaExercises\Exc1\excercise1\_495401_assignsubmission_file_\ArithmeticApp.class"));
        }

        [Fact]
        public async void StartCompilationTestFailedTest()
        {
            //Arrange
            HomeExercise homeExercise1 = new HomeExercise() { HomeExercisePath = @"C:\Users\anton\Desktop\HETS.Project\Matala3\JavaExercises\Exc1\excercise1\_495387_assignsubmission_file_\ArithmeticApp.java" };
      
            List<HomeExercise> homeExercises = new List<HomeExercise>();
            homeExercises.Add(homeExercise1);
            //Act
            await CompilationTest.StartCompilationTest(homeExercises);
            //Assert
            Assert.False(File.Exists(@"C:\Users\anton\Desktop\HETS.Project\Matala3\JavaExercises\Exc1\excercise1\_495387_assignsubmission_file_\ArithmeticApp.class"));

        }

    }
}
