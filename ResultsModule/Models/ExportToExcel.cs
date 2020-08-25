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
        {
            string AppPath = Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]);
            DirectoryInfo myDir = new DirectoryInfo(AppPath);
            string dataDir = myDir.Parent.Parent.FullName.ToString();
           
            StringBuilder csvcontent = new StringBuilder();
            csvcontent.AppendLine("homeExercisePath,homeExerciseName,compilationOutput,compilationErrorOutput");
            foreach (var homeExercise in homeExercises)
            {
                csvcontent.AppendLine($"{homeExercise.HomeExercisePath}, {homeExercise.HomeExerciseName}, {homeExercise.CompilationOutput},{homeExercise.CompilationErrorOutput}");      
            }
            File.AppendAllText(dataDir, csvcontent.ToString());
        }








    }
}
