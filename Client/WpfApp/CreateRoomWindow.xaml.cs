using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApp.Models;

namespace WpfApp
{
    /// <summary>
    /// Логика взаимодействия для CreateRoomWindow.xaml
    /// </summary>
    public partial class CreateRoomWindow : Window
    {
        private MainWindow mainWindow;
        public CreateRoomWindow(MainWindow window)
        {
            InitializeComponent();
            mainWindow = window;
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            if (RoomNameTextBox.Text != null && RoomNameTextBox.Text.Length > 3)
            {
                RoomData room = new RoomData(RoomNameTextBox.Text);
                room.Users.Add(new RoomUserData("Team_Lead"));
                mainWindow.mainData.AddRoom(room);
                mainWindow.CloseBlur();
                this.Close();
            }
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Opacity = 1;
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Opacity = 0.6;
            this.DragMove();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.CloseBlur();
            this.Close();
        }
    }
}
