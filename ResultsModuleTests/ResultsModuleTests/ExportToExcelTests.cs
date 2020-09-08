using System;
using Xunit;
using System.IO;
using ResultsModule.Models;
using System.Collections.ObjectModel;
using DataBuilders;

namespace ResultsModuleTests
{
    public class ExportToExcelTests
    {
        [Fact]
        public void ToCsvSuccessTests()
        {
            //Arrange
            string AppPath = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);
            DirectoryInfo myDir = new DirectoryInfo(AppPath);
            string dataDir = myDir.Parent.Parent.FullName.ToString();
            HomeExercise homeExercise = new HomeExercise();
            var homeExercises = new ObservableCollection<HomeExercise>();
            homeExercises.Add(homeExercise);
            //Act
            ExportToExcel.ToCsv(homeExercises);
            //Assert
            Assert.True(File.Exists(dataDir+"\\HomeExerciseReport.csv"));
            
        }
    }
}
