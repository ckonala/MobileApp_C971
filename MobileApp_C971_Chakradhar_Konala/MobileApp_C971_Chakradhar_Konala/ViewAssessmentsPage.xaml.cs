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

        private void AddAssessment_OnClicked(object sender, EventArgs e)
        {
           // throw new NotImplementedException();
        }

        private async void AssessmentList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            await Navigation.PushAsync(new DetailedAssessmentViewPage(e.CurrentSelection.FirstOrDefault() as Assessments));
        }

        private void EditDeleteTerm_OnClicked(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
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