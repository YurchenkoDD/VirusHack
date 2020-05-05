using CoreLib;
using Microsoft.SqlServer.Server;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using WpfApp.Models;

namespace WpfApp.Controllers
{
    public static class AppController
    {
        public enum NextWindow { Login, Main }
        private static readonly string ROOT_DIR = ".\\Data";
        private static readonly string PATH_TO_LOGININFO = ROOT_DIR + "\\LoginInfo.json";
        private static readonly string PATH_TO_ROOMSFILES = ROOT_DIR + "\\Rooms";
        private static readonly string PATH_TO_RECORDS = ROOT_DIR + "\\Records";

        private static LoginInfoData loginInfo;
        public static LoginInfoData LoginInfo
        {
            get { return loginInfo; }
            set
            {
                loginInfo = value;
                SaveLoginInfo();
            }
        }
        public static bool IsListen = false;
        public static void StartListener() {
            if (!IsListen)
            {
                IsListen = true;
                Core.inputDevice.StartListen(PATH_TO_RECORDS);
            }
        }
        public static void StopListener() {
            IsListen = false;
            Core.inputDevice.StopListen();
        }

        public static void TestRoom()
        {
            RoomData roomData = new RoomData("Хакатон");
            roomData.Users.Add(new RoomUserData("Николай"));
            roomData.Users.Add(new RoomUserData("Hello_Guest"));
            roomData.Users.Add(new RoomUserData("Team_Lead"));
            roomData.Messages.Add(new MessageData() { Date = "23.05.2020", Text = "Здравствуйте как прошел ваш день", UserName = "Nikolay" });
            roomData.Messages.Add(new MessageData() { Date = "23.05.2020", Text = "Отлично", UserName = "TeamLead" });
            roomData.Messages.Add(new MessageData() { Date = "23.05.2020", Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", UserName = "WhoAreMe?" });
            string output = JsonConvert.SerializeObject(roomData, Formatting.Indented);
            File.WriteAllText(PATH_TO_ROOMSFILES + "\\" + roomData.RoomName + ".json", output);
        }

        public static RoomData[] GetRooms()
        {
            var files = Directory.GetFiles(PATH_TO_ROOMSFILES);
            List<RoomData> result = new List<RoomData>();
            foreach (var file in files)
            {
                try
                {
                    string json = File.ReadAllText(file);
                    RoomData data = JsonConvert.DeserializeObject<RoomData>(json);
                    result.Add(data);
                }
                catch
                {

                }
            }
            return result.ToArray();
        }

        public static void SaveRoom(RoomData data) {
            try
            {
                string output = JsonConvert.SerializeObject(data, Formatting.Indented);
                File.WriteAllText(PATH_TO_ROOMSFILES + "\\" + data.RoomName + ".json", output);
            }
            catch { }
        }

        private static void SaveLoginInfo()
        {
            ThreadPool.QueueUserWorkItem((object state) =>
            {
                try
                {
                    string output = JsonConvert.SerializeObject(AppController.LoginInfo);
                    File.WriteAllText(AppController.PATH_TO_LOGININFO, output);
                }
                catch { }
            });
        }

        public static NextWindow Init()
        {
            try
            {
                if (!Directory.Exists(ROOT_DIR))
                {
                    Directory.CreateDirectory(ROOT_DIR);
                }
                if (!Directory.Exists(PATH_TO_ROOMSFILES))
                {
                    Directory.CreateDirectory(PATH_TO_ROOMSFILES);
                }
                if (!Directory.Exists(PATH_TO_RECORDS))
                {
                    Directory.CreateDirectory(PATH_TO_RECORDS);
                }
            }
            catch
            {

            }

            NextWindow nextWindow = NextWindow.Login;

            if (File.Exists(AppController.PATH_TO_LOGININFO))
            {
                try
                {
                    string json = File.ReadAllText(AppController.PATH_TO_LOGININFO);
                    LoginInfoData loginInfo = JsonConvert.DeserializeObject<LoginInfoData>(json);
                    AppController.LoginInfo = loginInfo;
                    nextWindow = NextWindow.Main;
                }
                catch
                {

                }
            }

            return nextWindow;
        }
    }
}
