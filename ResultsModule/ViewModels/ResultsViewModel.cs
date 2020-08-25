using DataBuilders;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using ResultsModule.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ResultsModule.ViewModels
{
    public class ResultsViewModel : BindableBase
    {
        private IEventAggregator _eventAggregator;

        public DelegateCommand ExportToExcel { get; set; }

        //list with HomeExercises that View can see
        public ObservableCollection<HomeExercise> HomeExercises { get; set; }

        public ResultsViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            HomeExercises = new ObservableCollection<HomeExercise>();
            _eventAggregator.GetEvent<UpdateHomeExercisesEvent>().Subscribe(UpdatedHomeExercises);
            ExportToExcel = new DelegateCommand(ExecuteExportToExcel);
            
        }

        //called on Export To Excel click
        private void ExecuteExportToExcel()
        {
            //TODO with _homeExercises as send parameter
        }

        private void UpdatedHomeExercises(ObservableCollection<HomeExercise> homeExercises)
        {
            HomeExercises.Clear();
            if (HomeExercises.Count == 0)
            {
                foreach(var homeExercise in homeExercises)
                {
                    HomeExercises.Add(homeExercise);
                }
            }
        }
    }
}
