using Prism.Mvvm;
using System.Collections.Generic;
namespace DataBuilders
{
    public class HomeExercise : BindableBase
    {
        private string _homeExercisePath;
        
        public string HomeExercisePath
        {
            get { return _homeExercisePath; }
            set { SetProperty(ref _homeExercisePath, value); }
        }

        private string _homeExerciseFolderName;
        public string HomeExerciseFolderName
        {
            get { return _homeExerciseFolderName; }
            set { SetProperty(ref _homeExerciseFolderName, value); }
        }

        private string _runTestOutput;
        public string RunTestOutput
        {
            get { return _runTestOutput; }
            set { SetProperty(ref _runTestOutput, value); }
        }
        public List<string> CompatibleRunTestList { get; set; }

        private string _isCompatibleRunTest;
        public string IsCompatibleRunTest
        {
            get { return _isCompatibleRunTest; }
            set { SetProperty(ref _isCompatibleRunTest, value); }
        }

        private string _runTestErrorOutput;
        public string RunTestErrorOutput
        {
            get { return _runTestErrorOutput; }
            set { SetProperty(ref _runTestErrorOutput, value); } 
        }

        private string _compilationErrorOutput;
        public string CompilationErrorOutput
        {
            get { return _compilationErrorOutput; }
            set { SetProperty(ref _compilationErrorOutput, value); }
        }

        private string _isRunTestOk;
        public string IsRunTestOk
        {
            get { return _isRunTestOk; }
            set { SetProperty(ref _isRunTestOk, value); }
        }

        private string _isCompilationTestOk;
        public string IsCompilationTestOk
        {
            get { return _isCompilationTestOk; }
            set { SetProperty(ref _isCompilationTestOk, value); }
        }

    }
}
