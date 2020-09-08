using System;
using System.Collections.Generic;
using DataBuilders;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace HETSPrism.Services
{
    public static class CompilationTest
    {

        public static async Task<string> StartCompilationTest(IReadOnlyList<HomeExercise> homeExercises, IProgress<double> progress=null)
        {
            for (var index = 0; index < homeExercises.Count; index++)
            {
                var homeExercise = homeExercises[index];
                // definition of process
                Process process = new Process();
                process.StartInfo.FileName = "javac.exe";
                process.StartInfo.Arguments = $"-Xlint {homeExercise.HomeExercisePath}";
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.RedirectStandardInput = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                try
                {
                    process.Start();
                }
                catch
                {
                    throw new Exception("Error: javac.exe compiler not found in path variables");
                }

                StreamReader se = process.StandardError;
                homeExercise.CompilationErrorOutput = await se.ReadToEndAsync();
                if (homeExercise.CompilationErrorOutput != "")
                {
                    homeExercise.IsCompilationTestOk = "Has Error";
                }
                else
                {
                    homeExercise.IsCompilationTestOk = "Success";
                }
                progress?.Report(((double)(index + 1) / homeExercises.Count) * 100);
            }

            return "OK";
        }
    }
}
