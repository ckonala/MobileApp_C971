using MobileApp_C971_Chakradhar_Konala.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MobileApp_C971_Chakradhar_Konala
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            PerformDummyInsert = true;
        }

        private bool PerformDummyInsert { get; set; }

        private async void addTerm_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddTermPage());
        }

        protected override async void OnAppearing()
        {
            if (PerformDummyInsert)
            {
                var term = new Term()
                {
                    termTitle = "Term 1",
                    startDate = DateTime.Now.Date,
                    endDate = DateTime.Now.Date.AddDays(29)
                };

                var term2 = new Term()
                {
                    termTitle = "Term 2",
                    startDate = DateTime.Now.Date.AddDays(30),
                    endDate = DateTime.Now.Date.AddDays(60)
                };

                App.Database.CreateTable();

                if (!(await App.Database.GetTermAsync()).Any())
                {
                    App.Database.InsertTermAsync(term).Wait();
                    App.Database.InsertTermAsync(term2).Wait();
                }

                PerformDummyInsert = false;
            }

            var _termList = await App.Database.GetTermAsync();
            TermInformation.ItemsSource = _termList;
            base.OnAppearing();
        }

        private async void TermInformation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            await Navigation.PushAsync(new EditTermPage(e.CurrentSelection.FirstOrDefault() as Term));

        }
    }
}
