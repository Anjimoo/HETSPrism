using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using DataBuilders;
using CsvHelper;

namespace ResultsModule.Models
{
     public class ExportToExcel
    { 
       public static void ToCsv(ObservableCollection<HomeExercise> homeExercises)
        {   // the path to folder project
            string appPath = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);
            DirectoryInfo myDir = new DirectoryInfo(appPath);
            string dataDir = myDir.Parent.Parent.FullName.ToString();
            string csvFile = $"{dataDir}\\HomeExerciseReport.csv";

            using (var writer = new StreamWriter(csvFile))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(homeExercises);
            }
            if(Directory.Exists(dataDir))
            {
                ProcessStartInfo startInfo = new ProcessStartInfo { Arguments = dataDir, FileName = "explorer.exe" };
                Process.Start(startInfo);
            }
        }
    }
}
