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
        public void ToCsvTests()
        {
          
            //string AppPath = Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]);
            //DirectoryInfo myDir = new DirectoryInfo(AppPath);
            //string dataDir = myDir.Parent.Parent.FullName.ToString();
            string path = "C:\\Users\\user\\source\\repos\\Anjimoo\\HETSPrism\\bin\\HomeExerciseReport.csv";
            HomeExercise homeExercise = new HomeExercise();
            var homeExercises = new ObservableCollection<HomeExercise>();
            homeExercises.Add(homeExercise);
            ExportToExcel.ToCsv(homeExercises);
            Assert.True(File.Exists(path));
          



        }
    }
}
