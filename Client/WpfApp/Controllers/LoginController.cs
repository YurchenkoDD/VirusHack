using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WpfApp.Models;

namespace WpfApp.Controllers
{
    public static class LoginController
    {
        private static readonly LoginData[] _accounts = new LoginData[] {
        new LoginData(){UserName="Test_Account",Password="123456"},
        new LoginData(){UserName="Lead_Account",Password="123456"},
        new LoginData(){UserName="User_Account",Password="123456"}
        };

        public static event Action<bool,string> OnCompleted;

        public static async void SignInAsync(LoginData loginData) {
            await Task.Run(()=> {
                //При отправке обязательно ЗАШИФРОВАТЬ ПАРОЛЬ по открытому ключу полученным с сервера.

                //Отпарвка post или get запроса к серверу.

                //Имитируем задержку сервера
                Thread.Sleep(3000);

                //Заглушка
                var account = _accounts.Where(x => x.UserName.Equals(loginData.UserName) && x.Password.Equals(loginData.Password));
                if (account.Count() > 0)
                {
                    AppController.LoginInfo = new LoginInfoData() { Login = account.First().UserName };

                    OnCompleted?.Invoke(true, "Success");
                }
                else
                {
                    OnCompleted?.Invoke(false, "Неверный логин или пароль.");
                }
            });
        }
    }
}
