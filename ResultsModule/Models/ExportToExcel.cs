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
            string appPath = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);
            DirectoryInfo myDir = new DirectoryInfo(appPath);
            string dataDir = myDir.Parent.Parent.FullName.ToString();
            string csvFiles = $"{dataDir}\\HomeExerciseReport.csv";

            using (var writer = new StreamWriter(csvFiles))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(homeExercises);
            }
        }

    }
}
