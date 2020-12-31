using System;
using System.IO;
using MobileApp_C971_Chakradhar_Konala.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp_C971_Chakradhar_Konala
{
    public partial class App : Application
    {
        static Database database;

        public static Database Database
        {
            get
            {
                database = new Database((Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "mobileappC971.db3")));
                return database;
            }
        }

        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
