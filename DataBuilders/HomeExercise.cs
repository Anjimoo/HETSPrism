using Prism.Mvvm;
using System.Collections.Generic;
namespace DataBuilders
{
    public class HomeExercise : BindableBase
    {
        private string _homeExercisePath;
        
        public string HomeExercisePath { get; set; }

        private string _homeExerciseFolderName;
        public string HomeExerciseFolderName
        {
            get { return _homeExerciseFolderName; }
            set { SetProperty(ref _homeExerciseFolderName, value); }
        }

        private string _runTestOutput;
        public string RunTestOutput { get; set; }

        public List<string> CompatibleRunTestList { get; set; }

        private string _isCompatibleRunTest;
        public string IsCompatibleRunTest
        {
            get { return _isCompatibleRunTest; }
            set { SetProperty(ref _isCompatibleRunTest, value); }
        }

        private string _runTestErrorOutput;
        public string RunTestErrorOutput { get; set; }

        private string _compilationErrorOutput;
        public string CompilationErrorOutput { get; set; }

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
