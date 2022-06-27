
using D2P0JX_SG1_21_22_2.WpfClient.BL.Interfaces;
using D2P0JX_SG1_21_22_2.WpfClient.Infrastructure;
using D2P0JX_SG1_21_22_2.WpfClient.BL.Implementation;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;
using System.Windows;
using CommonServiceLocator;

namespace D2P0JX_SG1_21_22_2.WpfClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIocAsServiceLocator.Instance);

            SimpleIocAsServiceLocator.Instance.Register<IPizzaEditorService, PizzaEditorViaWindowService>();
            SimpleIocAsServiceLocator.Instance.Register<IPizzaDisplayService, PizzaDisplayService>();
            SimpleIocAsServiceLocator.Instance.Register<IPizzaHandlerService, PizzaHandlerService>();
            SimpleIocAsServiceLocator.Instance.Register(() => Messenger.Default);
        }
    }
}
