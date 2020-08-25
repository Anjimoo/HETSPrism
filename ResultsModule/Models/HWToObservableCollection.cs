using DataBuilders;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ResultsModule.Models
{
    public static class HWToObservableCollection
    {
        public static ObservableCollection<HomeExercise> ParseToOC(List<HomeExercise> homeExercises)
        {
            var homeExercisesCollection = new ObservableCollection<HomeExercise>();

            foreach(var homeExercise in homeExercises)
            {
                homeExercisesCollection.Add(homeExercise);
            }
            return homeExercisesCollection;
        }
    }
}
