using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApp.Controllers;
using WpfApp.Models;

namespace WpfApp
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        LoginData loginData;
        public LoginWindow()
        {
            InitializeComponent();

            if(AppController.Init() == AppController.NextWindow.Main)
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }


            loginData = new LoginData();
            this.DataContext = loginData;
            LoginController.OnCompleted += LoginController_OnCompleted;

            PasswordInputBox.Password = "123456";
        }

        private void LoginController_OnCompleted(bool isSuccess, string message)
        {
            Dispatcher.Invoke(() =>
            {
                if (isSuccess)
                {
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    this.Close();
                }
                else
                {
                    this.IsEnabled = true;
                    ButtonError.Content = message;
                    ButtonError.Visibility = Visibility.Visible;
                    ButtonError.Opacity = 1;


                    DoubleAnimation borderAnim = new DoubleAnimation();
                    borderAnim.From = 1;
                    borderAnim.To = 0;
                    borderAnim.Duration = new Duration(TimeSpan.FromMilliseconds(5000));
                    ButtonError.BeginAnimation(Button.OpacityProperty, borderAnim);
                    ButtonError.Visibility = Visibility.Visible;
                }
            });
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (loginData.UserName != null && loginData.UserName.Length > 0 && PasswordInputBox.Password != null && PasswordInputBox.Password.Length >= 6)
            {
                loginData.Password = PasswordInputBox.Password;
                LoginController.SignInAsync(loginData);
                this.IsEnabled = false;
            }
            else {
                LoginController_OnCompleted(false,"Введите корректные данные для входа.");
            }
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Opacity = 0.6;
            this.DragMove();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Opacity = 1;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
