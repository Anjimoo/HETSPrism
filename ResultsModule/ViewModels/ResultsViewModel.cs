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
        public DelegateCommand ExportToExcel { get; set; }
        public ObservableCollection<ExcelModel> excelModels { get; set; }

        public ResultsViewModel()
        {
            ExportToExcel = new DelegateCommand(ExecuteExportToExcel);
        }

        private void ExecuteExportToExcel()
        {
           
        }



        public void OnNavigatedTo(NavigationContext navigationContext)
        {
         
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
