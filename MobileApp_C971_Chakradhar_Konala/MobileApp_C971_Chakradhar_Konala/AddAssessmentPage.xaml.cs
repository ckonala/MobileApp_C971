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
    public partial class AddAssessmentPage : ContentPage
    {
        private Course course { get; set; }

        public AddAssessmentPage(Course course)
        {
            InitializeComponent();
            this.course = course;
            BindingContext = new CoursePageViewModel();
        }

        private async void SaveAssessment_OnClicked(object sender, EventArgs e)
        {
            Assessments assessment = new Assessments();

            assessment.courseID = course.ID;
            assessment.assessmentNotifications = AssessmentNotification.IsToggled;

            if (!(AssessmentSelect.SelectedItem is null))
            {
                assessment.assessmentTitle = AssessmentSelect.SelectedItem.ToString();

                if (!(AssessmentCode.Text is null) && AssessmentCode.Text.Length > 1)
                {
                    assessment.assessmentCode = AssessmentCode.Text;

                    if (AssessmentDueDatePicker.Date >= course.courseStartDate)
                    {
                        assessment.anticipatedDueDate = AssessmentDueDatePicker.Date;
                        await App.Database.InsertAssessmentsAsync(assessment);
                        await Navigation.PopAsync();
                    }
                    else
                    {
                        await DisplayAlert("Error", "Anticipated Due date should be after the Course Start date", "Ok");
                    }

                }
                else
                {
                    await DisplayAlert("Error", "Please ensure Assessment code field is not empty", "Ok");
                }
            }
            else
            {
                await DisplayAlert("Error", "Please select an Assessment", "Ok");
            }

        }

        protected override async void OnAppearing()
        {
            var assessmentList = await App.Database.GetAssessmentsAsync(course.ID);
            if (assessmentList.Count != 0)
            {
                var assessmentSource = new CoursePageViewModel().AssessmentTypeSource;
                assessmentSource.Remove(assessmentList[0].assessmentTitle.ToString());
                AssessmentSelect.SelectedItem = assessmentSource[0];
                AssessmentSelect.IsEnabled = false;
            }

            base.OnAppearing();
        }
    }
}