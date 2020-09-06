using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections.ObjectModel;
using System.Globalization;
using DataBuilders;
using CsvHelper;

namespace ResultsModule.Models
{
     public class ExportToExcel
    { 
       public static void ToCsv(ObservableCollection<HomeExercise> homeExercises)
        {   // the path to folder project
            string AppPath = Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]);
            DirectoryInfo myDir = new DirectoryInfo(AppPath);
            string dataDir = myDir.Parent.Parent.FullName.ToString();
            string csvFiles = $"{dataDir}\\HomeExerciseReport.csv";

            // create a table and export to csv file
            //StringBuilder csvcontent = new StringBuilder();
            //csvcontent.AppendLine("Home Exercise Path, Home Exercise Name, Passed Compilation Test, " +
            //                      "Compilation Error Output, Passed Run Test, Passed I/O Test, Run Test Output, " +
            //                      "Run Test Error");
            //foreach (var homeExercise in homeExercises)
            //{
            //    csvcontent.AppendLine($"{homeExercise.HomeExercisePath}, {homeExercise.HomeExerciseFolderName}, " +
            //                          $"{homeExercise.IsCompilationTestOk}, {homeExercise.CompilationErrorOutput}, " +
            //                          $"{homeExercise.IsRunTestOk}, {homeExercise.IsCompatibleRunTest}, " +
            //                          $"{homeExercise.RunTestOutput}, {homeExercise.RunTestErrorOutput}");
            //}

            //File.AppendAllText(csvFiles, csvcontent.ToString());
            using (var writer = new StreamWriter(csvFiles))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(homeExercises);
            }
        }

    }
}
