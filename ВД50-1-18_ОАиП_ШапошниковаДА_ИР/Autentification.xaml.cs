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
    public sealed partial class Autentification : Page
    {
        public Autentification()
        {
            this.InitializeComponent();
        }
        public class Data
        {
            public string Email { get; set; }
            public int Group { get; set; }
            public bool Status { get; set; }
            public Data(string email, int group, bool status)
            {
                Email = email;
                Group = group;
                Status = status;
            }
        }
        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            User user = new User();
            Config config = new Config();
            Activated();
            try
            {
                if (user.Authentication(LoginBox.Text, PasswordBox.Password.ToString()))
                {
                    Frame contentFrame = Window.Current.Content as Frame; // Обращение к родительскому экрану 
                    MainPage mp = contentFrame.Content as MainPage; // Изменение состояния родительского экрана
                    mp.Frame.Navigate(typeof(PanelFrame));
                }
                else
                {
                    await config.Alert("Проверьте правильность введённых данных и повторите попытку.", "Ошибка");
                }
            }
            catch (Exception ex)
            {
                await config.Alert("Произошла ошибка с текстом исключения «" + ex.Message + "». Повторите попытку позднее или обратитесь к системному администратору.", "Ошибка " + ex.Source);
            }
        }
        private object TryAuth()
        {
            var data_base = new Database();
            var data_table = data_base.Select("SELECT * FROM `users` WHERE " +
            "(`users`.`user_login` = '" + LoginBox.Text + "' OR " +
            "`users`.`user_email` = '" + LoginBox.Text + "') AND BINARY " +
            "`users`.`user_password` = '" + PasswordBox.Password.ToString() + "'");
            bool row_count = Convert.ToBoolean(data_table.Rows.Count);
            string user_email;
            int id_group;
            if (row_count)
            {
                user_email = data_table.Rows[0]["user_email"].ToString();
                id_group = Convert.ToInt32(data_table.Rows[0]["id_group"]);
            }
            else
            {
                user_email = null; id_group = 0;
            }
            return new Data(user_email, id_group, row_count);
        }
        private async void Activated()
        {
            var config = new Config();
            try
            {
                var AuthResult = TryAuth();
                bool AuthStatus = Convert.ToBoolean(config.GetValue(AuthResult, "Status"));
                if (AuthStatus)
                {
                    string UserGroup = config.GetValue(AuthResult, "Group");
                    string UserEmail = config.GetValue(AuthResult, "Email");
                    await config.Alert("Вы находитесь в группе: " + UserGroup + Environment.NewLine
                    + "Ваша электронная почта: " + UserEmail, "Успешно");
                }
                else
                {
                    await config.Alert("Проверьте правильность введённых данных", "Ошибка");
                }
            }
            catch (Exception ex)
            {
                await config.Alert(ex.ToString(), "Ошибка");
            }
        }
     
        private void LoginBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (LoginBox.Text != "" && PasswordBox.Password != "")
            {
                LoginButton.IsEnabled = true;
                if (e.Key == Windows.System.VirtualKey.Enter)
                {
                    Activated();
                }
            }
        }
        private void PasswordBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (LoginBox.Text != "" && PasswordBox.Password != "")
            {
                LoginButton.IsEnabled = true;
                if (e.Key == Windows.System.VirtualKey.Enter)
                {
                    Activated();
                }
            }
        }

    }
}