using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp.Controllers;
using WpfApp.Models;

namespace WpfApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainData mainData;
        public MainWindow()
        {
            InitializeComponent();

            UserNameLabel.Content = AppController.LoginInfo.Login;
            mainData = new MainData();
            mainData.OnRoomChange += MainData_OnRoomChange;
            mainData.OnNewCurrentRoom += MainData_OnNewCurrentRoom;

            RoomList.SelectionMode = SelectionMode.Single;
            RoomList.SelectionChanged += RoomList_SelectionChanged;
            CloseUi();

            var rooms = AppController.GetRooms();
            foreach (var item in rooms)
            {
                mainData.AddRoom(item);
            }
        }

        private void MainData_OnNewCurrentRoom()
        {
            if (mainData.CurrentRoom == null)
            {
                CloseUi();
                AppController.StopListener();
            }
            else
            {
                AppController.StartListener();
                RoomNameButton.Content = mainData.CurrentRoom.RoomName;
                UserList.ItemsSource = mainData.CurrentRoom.Users;
            }
        }

        private void RoomList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems != null && e.AddedItems.Count > 0) {
                RoomData room = (RoomData)e.AddedItems[0];
                mainData.SelectRoom(room);
                RoomList.SelectedItem = null;
                OpenUi();
            }
        }

        private void MainData_OnRoomChange()
        {
            RoomList.ItemsSource = mainData.Rooms;
            EmptyBoxHide();
        }

        private void OffButton_Click(object sender, RoutedEventArgs e)
        {
            mainData.SelectRoom(null);
        }


        //Анимация
        private static bool IsCollapse = false;
        private static bool IsOpen = true;

        private const int ANIM_SPEED = 400;

        private void RoomNameButton_Click(object sender, RoutedEventArgs e)
        {
            if (IsCollapse)
            {
                UnCollapseUi();
            }
            else {
                CollapseUi();
            }
        }

        private void CollapseUi() {
            IsCollapse = true;
            //Свернуть box текущей комнаты
            DoubleAnimation anim = new DoubleAnimation();
            anim.From = CurrentRoomBox.Height;
            anim.To = 60;
            anim.Duration = new Duration(TimeSpan.FromMilliseconds(ANIM_SPEED));
            CurrentRoomBox.BeginAnimation(Border.HeightProperty, anim);
            anim.From = RoomListBox.Height;
            anim.To = RoomListBox.MaxHeight - 90;
            RoomListBox.BeginAnimation(Border.HeightProperty, anim);
        }

        private void UnCollapseUi() {
            IsCollapse = false;
            DoubleAnimation anim = new DoubleAnimation();
            anim.From = CurrentRoomBox.Height;
            anim.To = CurrentRoomBox.MaxHeight;
            anim.Duration = new Duration(TimeSpan.FromMilliseconds(ANIM_SPEED));
            CurrentRoomBox.BeginAnimation(Border.HeightProperty, anim);
            anim.From = RoomListBox.Height;
            anim.To = 230;
            RoomListBox.BeginAnimation(Border.HeightProperty, anim);
        }

        private void CloseUi() {
            if (IsOpen)
            {
                IsOpen = false;
                CurrentRoomBox.IsEnabled = false;

                DoubleAnimation anim = new DoubleAnimation();
                anim.From = CurrentRoomBox.Height;
                anim.To = 0;
                anim.Duration = new Duration(TimeSpan.FromMilliseconds(ANIM_SPEED));
                CurrentRoomBox.BeginAnimation(Border.HeightProperty, anim);
                anim.From = RoomListBox.Height;
                anim.To = RoomListBox.MaxHeight;
                RoomListBox.BeginAnimation(Border.HeightProperty, anim);
            }
        }

        private void OpenUi() {
            if (!IsOpen) {
                IsOpen = true;
                CurrentRoomBox.IsEnabled = true;

                UnCollapseUi();
            }
        }

        private void netBox_MouseLeave(object sender, MouseEventArgs e)
        {
            netBox.IsEnabled = false;
            netBox.Visibility = Visibility.Hidden;
        }

        public void OpenBlur() {
            blurBorder.IsEnabled = true;
            blurBorder.Visibility = Visibility.Visible;
        }

        public void CloseBlur() {
            blurBorder.IsEnabled = false;
            blurBorder.Visibility = Visibility.Hidden;
        }

        private void CreateNetButton_Click(object sender, RoutedEventArgs e)
        {
            OpenBlur();
            CreateRoomWindow window = new CreateRoomWindow(this);
            window.Owner = this;
            window.ShowDialog();
        }

        private void LoginNetButton_Click(object sender, RoutedEventArgs e)
        {
            OpenBlur();
        }

        private void NetSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            netBox.IsEnabled = true;
            netBox.Visibility = Visibility.Visible;
        }

        private void EmptyBoxHide() {
            EmptyBox.IsEnabled = false;
            EmptyBox.Visibility = Visibility.Hidden;
            RoomListBox.IsEnabled = true;
            RoomListBox.Visibility = Visibility.Visible;
        }

        private void LoginRoomButton_Click(object sender, RoutedEventArgs e)
        {
            OpenBlur();
            LoginRoomWindow window = new LoginRoomWindow(this);
            window.Owner = this;
            window.ShowDialog();
        }

        private void ChatButton_Click(object sender, RoutedEventArgs e)
        {
            ChatBox.Visibility = Visibility.Visible;
            ChatList.ItemsSource = mainData.CurrentRoom.Messages;
            RoomChatBox.Content = mainData.CurrentRoom.RoomName;
        }

        private void ChatOffButton_Click(object sender, RoutedEventArgs e)
        {
            ChatBox.Visibility = Visibility.Hidden;
        }

        private void FormatButton_Click(object sender, RoutedEventArgs e)
        {
            OpenBlur();
            FormatSelBox.Visibility = Visibility.Visible;
        }

        private void FormatButtonDone_Click(object sender, RoutedEventArgs e)
        {
            FormatSelBox.Visibility = Visibility.Hidden;
            FormatOkBox.Visibility = Visibility.Visible;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FormatOkBox.Visibility = Visibility.Hidden;
            CloseBlur();
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
