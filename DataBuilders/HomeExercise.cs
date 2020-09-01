using Prism.Mvvm;
using System;

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

        private string compilationErrorOutput;
        public string CompilationErrorOutput
        {
            get { return compilationErrorOutput; }
            set { SetProperty(ref compilationErrorOutput, value); }
        }

    }
}
