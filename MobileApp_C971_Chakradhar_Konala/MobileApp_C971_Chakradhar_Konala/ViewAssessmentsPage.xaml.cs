using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileApp_C971_Chakradhar_Konala.Model;
using MobileApp_C971_Chakradhar_Konala.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp_C971_Chakradhar_Konala
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewAssessmentsPage : ContentPage
    {
        private Course course { get; set; }

        public ViewAssessmentsPage(Course course)
        {
            InitializeComponent();
            this.course = course;
            BindingContext = new CoursePageViewModel();
        }

        private async void AddAssessment_OnClicked(object sender, EventArgs e)
        {
            var tempCount = await App.Database.GetAssessmentsAsync(course.ID);
            if (tempCount.Count < 2)
            {
                await Navigation.PushAsync(new AddAssessmentPage(course));
            }
            else
            {
                await DisplayAlert("Error", "You have already added the maximum number of Assessments for this Course.", "Ok");
            }
        }

        private async void AssessmentList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            await Navigation.PushAsync(new DetailedAssessmentViewPage(e.CurrentSelection.FirstOrDefault() as Assessments,course));
        }

        protected override async void OnAppearing()
        {
            courseTitle.Text = course.courseTitle;
            courseStartDate.Text = $"{course.courseStartDate.Date.ToString("MM/dd/yyyy")} - {course.courseEndDate.Date.ToString("MM/dd/yyyy")}";
            var _assessmentsList = await App.Database.GetAssessmentsAsync(course.ID);
            AssessmentList.ItemsSource = _assessmentsList;
            base.OnAppearing();
        }

    }
}