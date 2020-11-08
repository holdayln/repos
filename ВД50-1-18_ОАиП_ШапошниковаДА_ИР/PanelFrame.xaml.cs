using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace ВД50_1_18_ОАиП_ШапошниковаДА_ИР
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class PanelFrame : Page
    {
        Config config = new Config();
        public PanelFrame()
        {
            this.InitializeComponent();
            string initials = " " + config.localSettings.Values["first_name"].ToString().Substring(0, 1) + ".";
            // Проверка на пустое значение поля «Отчество». В случае наличия, происходит обрезание значения до одного символа с точкой по аналогии с именем сотрудника 
            if (config.localSettings.Values["mid_name"] == "")
            {
                initials += " " + config.localSettings.Values["mid_name"].ToString().Substring(0, 1) + ".";
                UserProfileBtn.Content = config.localSettings.Values["last_name"] + initials;
            }
        }
    }
}
