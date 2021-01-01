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
                    endDate = DateTime.Now.Date.AddDays(30)
                };

                App.Database.CreateTermTable();

                if (!(await App.Database.GetTermAsync()).Any())
                {
                   await App.Database.InsertTermAsync(term);
                   var temptermPrimaryKey = await App.Database.RetreiveTermLastRow();
                   var termPrimaryKey = temptermPrimaryKey[0].ID;

                   var course = new Course()
                       {
                           courseTitle = "Course 1",
                           courseStartDate = DateTime.Now.Date,
                           courseEndDate = DateTime.Now.Date.AddDays(30),
                           courseStatus = "Not Started",
                           courseInstructor = "Chuck Konala",
                           courseInstructorPhone = "817-727-5432",
                           courseInstructorEmail = "ckonala@wgu.edu",
                           courseNotes = "",
                           courseNotifications = true,
                           termID = termPrimaryKey
                       };

                       App.Database.CreateCourseTable();
                       await App.Database.InsertCourseAsync(course);
                }

                PerformDummyInsert = false;
            }

            var _termList = await App.Database.GetTermAsync();
            TermInformation.ItemsSource = _termList;
            base.OnAppearing();
        }

        private async void TermInformation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            await Navigation.PushAsync(new CourseViewPage(e.CurrentSelection.FirstOrDefault() as Term));

        }
    }
}
