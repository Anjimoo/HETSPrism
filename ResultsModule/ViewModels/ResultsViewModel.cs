using DataBuilders;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using ResultsModule.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ResultsModule.ViewModels
{
    public class ResultsViewModel : BindableBase, INavigationAware
    {
        private List<HomeExercise> _homeExercises;
        public DelegateCommand ExportToExcel { get; set; }
        public ObservableCollection<HomeExercise> homeExercises { get; set; }

        public ResultsViewModel()
        {
            ExportToExcel = new DelegateCommand(ExecuteExportToExcel);
        }

        private void ExecuteExportToExcel()
        {
           //TODO
        }

        

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (navigationContext.Parameters.ContainsKey("homeexercises"))
            {
                _homeExercises = navigationContext.Parameters.GetValue<List<HomeExercise>>("homeexercises");
                // TODO convert to excelModel function
            }
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
           
        }
    }
}
