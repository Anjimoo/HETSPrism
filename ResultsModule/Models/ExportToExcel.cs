using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections.ObjectModel;
using DataBuilders;

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
            StringBuilder csvcontent = new StringBuilder();
            csvcontent.AppendLine("homeExercisePath,homeExerciseName,compilationOutput,compilationErrorOutput");
            foreach (var homeExercise in homeExercises)
            {
                csvcontent.AppendLine($"{homeExercise.HomeExercisePath}, {homeExercise.HomeExerciseName}, {homeExercise.CompilationOutput},{homeExercise.CompilationErrorOutput}");      
            }
          
             File.AppendAllText(csvFiles, csvcontent.ToString());
        }

    }
}
