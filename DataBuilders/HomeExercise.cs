using Prism.Mvvm;
using System;
using System.Windows;

namespace DataBuilders
{
    public class HomeExercise : BindableBase
    {
        private string homeExercisePath;
        public string HomeExercisePath
        {
            
            get { return homeExercisePath; }
            set { SetProperty(ref homeExercisePath, value); }
        }
        private string homeExerciseName;
        public string HomeExerciseName
        {
            get { return homeExerciseName; }
            set { SetProperty(ref homeExerciseName, value); }
        }

        private string compilationOutput;
        public string CompilationOutput
        {
            get { return compilationOutput; }
            set { SetProperty(ref compilationOutput, value); }
        }

        private string runTestOutput;
        public string RunTestOutput
        {
            get { return runTestOutput; }
            set { SetProperty(ref runTestOutput, value); }
        }

        private string runTestErrorOutput;

        public string RunTestErrorOutput
        {
            get { return runTestErrorOutput; }
            set { SetProperty(ref runTestErrorOutput, value); } 
        }

        private string compilationErrorOutput;
        public string CompilationErrorOutput
        {
            get { return compilationErrorOutput; }
            set { SetProperty(ref compilationErrorOutput, value); }
        }

    }
}
