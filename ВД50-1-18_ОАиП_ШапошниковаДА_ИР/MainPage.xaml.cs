using Google.Api;
using Nito.AsyncEx;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Page = Windows.UI.Xaml.Controls.Page;


// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x419

namespace ВД50_1_18_ОАиП_ШапошниковаДА_ИР
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        public MainPage()
        {
            this.InitializeComponent();
            navAutentification.Navigate(typeof(Autentification));
            MainAsync();
          /////  AsyncContext.Run(() => MainAsync());
        }

        private async void MainAsync()
        {
            User user = new User();
            switch (user.CheckAuth())
            {
                case 0:
                    navAutentification.Navigate(typeof(Authentication));
                    break;
                case 1:
                    navAutentification.Navigate(typeof(PanelFrame));
                    break;
                default:
                    var messageDialog = new MessageDialog("Произошла ошибка при подключении к Интернету. Пожалуйста, повторите попытку.", "Ошибка");
                    messageDialog.Commands.Add(new Windows.UI.Popups.UICommand("Повторить", (command) =>
                    {
                        navAutentification.Navigate(typeof(MainPage));
                    }));
                    messageDialog.Commands.Add(new Windows.UI.Popups.UICommand("Выход", (command) =>
                    {
                        Application.Current.Exit();
                    }));
                    await messageDialog.ShowAsync();
                    break;
            }
        }
    }
}

