using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Leaf.xNet;
using Newtonsoft.Json;

namespace Reader_books_pdf
{
    class VkAuth
    {
        private string Login { get; set; }

        private string Password { get; set; }

        private string Ip_h { get; set; }

        private string Lg_h { get; set; }

        public string Cookei { get; set; }

        public VkAuth(string Login, string Password)
        {
            this.Login = Login;
            this.Password = Password;

            ParsDataAuth();
        }

        //Выгружаем главную страницу для дальнейшей работы с её кодом
        private HttpResponse GetAuth()
        {
            HttpRequest request = new HttpRequest();
            request.UserAgentRandomize();
            request.KeepAlive = true;

            var response = request.Get("https://vk.com/login");
            return response;
        }

        //Парсим через регулярное значение айп_х, лг_р, эти данные нужны для авторизации
        private void ParsDataAuth()
        {
            var GetAuths = GetAuth();
            Ip_h = System.Text.RegularExpressions.Regex.Match(GetAuths.ToString(), "name=\"ip_h\" value=\"(.*?)\"").Groups[1].Value;
            Lg_h = System.Text.RegularExpressions.Regex.Match(GetAuths.ToString(), "name=\"lg_h\" value=\"(.*?)\"").Groups[1].Value;
            Cookei = GetAuths.Cookies.GetCookieHeader("https://vk.com/login");
        }

        //Метод авторизации
        private string Auth()
        {
            HttpRequest request = new HttpRequest();
            request.AddHeader("Accept", "text/html,application/xhtml+xml, application/xml;q=0.9, image/webp, image/apng, */*; q=0.8");
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddHeader("Upgrade-Insecure-Requests", "1");
            request.AddHeader("Cookie", Cookei);
            request.AddHeader("Referer", "https://vk.com/login");
            request.UserAgentRandomize();
            request.KeepAlive = true;

            RequestParams Params = new RequestParams();
            Params["act"] = "login";
            Params["role"] = "al_frame";
            Params["_origin"] = "https://vk.com";
            Params["ip_h"] = Ip_h;
            Params["lg_h"] = Lg_h;
            Params["email"] = Login;
            Params["pass"] = TextSett.ConvertEngToRus(Password);

            string response = request.Post("https://login.vk.com/?act=login", Params).ToString();
            Cookei = request.Cookies.GetCookieHeader("https://vk.com/");

            return response;
        }

        //Проверка авторизации
        public int CheckAuth()
        {
            string auths = Auth();
            int Otvet = 0;

            if (System.Text.RegularExpressions.Regex.Match(auths, "Notifier.init((.*))").Groups[1].Value.Length > 0) // Если верно
                Otvet = 1;
            else if (auths.Contains("try{parent.location.href='/login?act=authcheck'}catch(e){}")) // Если необходимо подтверждение
                Otvet = 2;
            else if (auths.Contains("parent.stManager.add(['notifier.js', 'notifier.css'], function() {")) // Если неверные данные
                Otvet = 0;
            return Otvet;
        }

        //Конвертация русского пароля в английский
        public static class TextSett
        {
            const string English = "qwertyuiop[]asdfghjkl;'zxcvbnm,.";
            const string Russian = "йцукенгшщзхъфывапролджэячсмитьбю";

            public static string ConvertEngToRus(string input)
            {
                var result = new StringBuilder(input.Length);
                int index;
                foreach(var symbol in input)
                    result.Append((index = Russian.IndexOf(symbol)) != -1 ? English[index] : symbol);
                return result.ToString();
            }
        }
        //private Form main_form;
        //private string access_token = null;
        //private string user_id = null;

        //const int AppID = 1;
        //const string vkMethodURL = "https://api.vkontakte.ru/method/";

        //public string AccessToken
        //{
        //    get { return this.access_token; }
        //}
        //public string UserID
        //{
        //    get { return this.user_id; }
        //}

        //public vkAPI(Form main_form)
        //{
        //    this.main_form = main_form;
        //}

        //public bool isAuthorized()
        //{
        //    if (access_token == null || user_id == null) return false;

        //    //дописать проверку на время действия access_token, чтобы не посылать всегда лишний запрос

        //    string profiles = vkMethodURL + "getProfiles?uid=1&access_token=" + access_token;

        //    System.Net.WebRequest reqGET = System.Net.WebRequest.Create(profiles);
        //    System.Net.WebResponse resp = reqGET.GetResponse();
        //    System.IO.Stream stream = resp.GetResponseStream();
        //    System.IO.StreamReader sr = new System.IO.StreamReader(stream);
        //    string s = sr.ReadToEnd();

        //    JObject o = JObject.Parse(s);
        //    JObject error = (JObject)o["error"];

        //    if (error == null) return true;
        //    else return false;
        //}

        //public bool Authorize()
        //{
        //    if (isAuthorized()) return true;

        //    AuthForm authform = new AuthForm();
        //    WebBrowser browser = (WebBrowser)authform.Controls["webBrowser"];
        //    browser.Navigated += new WebBrowserNavigatedEventHandler(Authorize_proceed);
        //    browser.Navigate("http://api.vkontakte.ru/oauth/authorize?client_id=" + AppID.ToString() +
        //        "&scope=photos&redirect_uri=" +
        //        "http://api.vkontakte.ru/blank.html&display=popup&response_type=token&hash=0");
        //    authform.ShowDialog();

        //    if (access_token == null || user_id == null)
        //    {
        //        MessageBox.Show("Авторизация не пройдена!\r\nДля работы программы вы должны авторизироваться!\r\nЗапрос авторизации появится автоматически.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        return false;
        //    }
        //    else return true;
        //}

        //private void Authorize_proceed(object sender, WebBrowserNavigatedEventArgs e)
        //{
        //    string[] parts = e.Url.AbsoluteUri.Split('#');
        //    WebBrowser browser = (WebBrowser)sender;
        //    AuthForm authform = (AuthForm)browser.Parent;

        //    if (parts[0] == "http://api.vkontakte.ru/blank.html")
        //    {
        //        if (parts[1].Substring(0, 5) == "error") authform.Close();

        //        else if (parts[1].Substring(0, 12) == "access_token")
        //        {
        //            //MessageBox.Show("Complete!");
        //            parts = parts[1].Split('&');

        //            access_token = parts[0].Split('=')[1];
        //            user_id = parts[2].Split('=')[1];
        //            authform.Close();
        //        }
        //    }
        //    else
        //    {
        //        parts = e.Url.AbsoluteUri.Split('?');
        //        if (parts[0] == "http://api.vkontakte.ru/oauth/grant_access")
        //            browser.Navigate("http://api.vkontakte.ru/oauth/authorize?client_id=" + AppID.ToString() + "&scope=photos&redirect_uri=http://api.vkontakte.ru/blank.html&display=popup&response_type=token&hash=0");
        //    }
        //}

        ////Тестовая функция API
        //public string[] GetMyProfile()
        //{
        //    Authorize();

        //    string profiles = vkMethodURL + "getProfiles?uid=" + user_id + "&fields=photo_medium_rec,sex,bdate,online&access_token=" + access_token;

        //    System.Net.WebRequest reqGET = System.Net.WebRequest.Create(profiles);
        //    System.Net.WebResponse resp = reqGET.GetResponse();
        //    System.IO.Stream stream = resp.GetResponseStream();
        //    System.IO.StreamReader sr = new System.IO.StreamReader(stream);
        //    string s = sr.ReadToEnd();
        //    //MessageBox.Show(s);
        //    JObject o = JObject.Parse(s);
        //    JArray response = (JArray)o["response"];

        //    string[] profile = new string[6];
        //    profile[0] = (string)response[0]["first_name"];
        //    profile[1] = (string)response[0]["last_name"];
        //    //profile[2] = (string)response[0]["sex"];
        //    if ((int)response[0]["sex"] == 1) profile[2] = "Женский";
        //    else profile[2] = "Мужской";
        //    profile[3] = (string)response[0]["photo_medium_rec"];
        //    profile[4] = (string)response[0]["bdate"];
        //    if ((int)response[0]["online"] == 1) profile[5] = "онлайн";
        //    else profile[5] = "не онлайн";

        //    return profile;
        //}
    }
}
