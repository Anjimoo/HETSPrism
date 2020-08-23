using HETSPrism.Views;
using IOTestModule;
using IOTestModule.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Unity;
using ResultsModule;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace HETSPrism
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
            // register other needed services here
        }
        protected override Window CreateShell()
        {
            Views.MainWindowView w = Container.Resolve<Views.MainWindowView>();
            return w;
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<ModuleIOTest>();
            moduleCatalog.AddModule<ModuleResults>();
        }

    }
}
