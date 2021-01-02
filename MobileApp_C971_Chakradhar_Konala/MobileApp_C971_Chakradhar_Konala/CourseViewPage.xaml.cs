using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileApp_C971_Chakradhar_Konala.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp_C971_Chakradhar_Konala
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CourseViewPage : ContentPage
    {
        private Term term { get; set; }

        public CourseViewPage(Term term)
        {
            InitializeComponent();
            this.term = term;
        }

        private async void AddCourse_OnClicked(object sender, EventArgs e)
        {
            var tempCount = await App.Database.GetCourseAsync(term.ID);
            if (tempCount.Count < 6)
            {
                await Navigation.PushAsync(new AddCoursePage(term));
            }
            else
            {
                await DisplayAlert("Error", "Max number of courses have already been added to this term", "Ok");
            }
        }

        private async void CourseList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            await Navigation.PushAsync(new DetailedCourseViewPage(e.CurrentSelection.FirstOrDefault() as Course));
        }

        protected override async void OnAppearing()
        {
            termTitle.Text = term.termTitle;
            termStartDate.Text = $"{term.startDate.Date.ToString("MM/dd/yyyy")} - {term.endDate.Date.ToString("MM/dd/yyyy")}";
            var _courseList = await App.Database.GetCourseAsync(term.ID);
            CourseList.ItemsSource = _courseList;
            base.OnAppearing();
        }

        private async void EditDeleteTerm_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditTermPage(term));
        }
    }
}