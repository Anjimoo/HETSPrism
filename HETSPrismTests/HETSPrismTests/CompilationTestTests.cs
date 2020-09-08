﻿using System;
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
        public async void StartCompilationTestOverallTest()
        {
            //Arrange
            HomeExercise homeExercise1 = new HomeExercise(){HomeExercisePath = @"C:\Users\anton\Desktop\HETS.Project\Matala3\JavaExercises\Exc1\excercise1\_495418_assignsubmission_file_\ArithmeticApp.java" };
            HomeExercise homeExercise2 = new HomeExercise(){HomeExercisePath = @"C:\Users\anton\Desktop\HETS.Project\Matala3\JavaExercises\Exc1\excercise1\_495401_assignsubmission_file_\ArithmeticApp.java" };
            List<HomeExercise> homeExercises = new List<HomeExercise>();
            homeExercises.Add(homeExercise1);
            homeExercises.Add(homeExercise2);
            //Act
            await CompilationTest.StartCompilationTest(homeExercises);
            //Assert
            Assert.True(File.Exists(@"C:\Users\anton\Desktop\HETS.Project\Matala3\JavaExercises\Exc1\excercise1\_495401_assignsubmission_file_\ArithmeticApp.class"));
            Assert.True(File.Exists(@"C:\Users\anton\Desktop\HETS.Project\Matala3\JavaExercises\Exc1\excercise1\_495418_assignsubmission_file_\ArithmeticApp.class"));
        }

        [Fact]
        public async void StartCompilationTestFileNotCompilatedTest()
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

        [Fact]
        public async void StartCompilationTestJavacIsNotInSystemVariablesTest()
        {
            //Arrange
            HomeExercise homeExercise1 = new HomeExercise() { HomeExercisePath = @"C:\Users\anton\Desktop\HETS.Project\Matala3\JavaExercises\Exc1\excercise1\_495418_assignsubmission_file_\ArithmeticApp.java" };
            HomeExercise homeExercise2 = new HomeExercise() { HomeExercisePath = @"C:\Users\anton\Desktop\HETS.Project\Matala3\JavaExercises\Exc1\excercise1\_495401_assignsubmission_file_\ArithmeticApp.java" };
            List<HomeExercise> homeExercises = new List<HomeExercise>();
            homeExercises.Add(homeExercise1);
            homeExercises.Add(homeExercise2);
            //Act
            string output = await CompilationTest.StartCompilationTest(homeExercises);
            //Assert
            Assert.Equal("Error: javac.exe compiler not found in path variables", output);
        }
    }
}
