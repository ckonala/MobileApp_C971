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
    public partial class EditAssessmentPage : ContentPage
    {
        private Assessments assessments { get; set; }

        private Course course { get; set; }
        public EditAssessmentPage(Assessments assessments, Course course)
        {
            InitializeComponent();
            this.assessments = assessments;
            this.course = course;
            BindingContext = new CoursePageViewModel();
        }

        private async void SaveAssessment_OnClicked(object sender, EventArgs e)
        {
            var assessmentList = await App.Database.GetAssessmentsAsync(course.ID);

            var duplicateAssessment = false;
            if (!assessments.assessmentTitle.Equals(AssessmentSelect.SelectedItem.ToString()))
            {
                foreach (var temp in assessmentList)
                {
                    if (temp.assessmentTitle.Equals(AssessmentSelect.SelectedItem.ToString()))
                    {
                        duplicateAssessment = true;
                    }
                }
            }

            assessments.assessmentNotifications = AssessmentNotification.IsToggled;

            if (!(AssessmentSelect.SelectedItem is null))
            {
                if (!duplicateAssessment)
                {
                    assessments.assessmentTitle = AssessmentSelect.SelectedItem.ToString();

                    if (!(AssessmentCode.Text is null) && AssessmentCode.Text.Length > 1)
                    {
                        assessments.assessmentCode = AssessmentCode.Text;

                        if (AssessmentDueDatePicker.Date >= course.courseStartDate)
                        {
                            assessments.anticipatedDueDate = AssessmentDueDatePicker.Date;
                            await App.Database.UpdateAssessmentsAsync(assessments);
                            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
                            await Navigation.PopAsync();
                        }
                        else
                        {
                            await DisplayAlert("Error", "Anticipated Due date should be after the Course Start date",
                                "Ok");
                        }

                    }
                    else
                    {
                        await DisplayAlert("Error", "Please ensure Assessment code field is not empty", "Ok");
                    }
                }
                else
                {
                    await DisplayAlert("Error", $"{AssessmentSelect.SelectedItem.ToString()} already exists in this course", "Ok");
                }
            }
            else
            {
                await DisplayAlert("Error", "Please select an Assessment", "Ok");
            }
        }

        protected override async void OnAppearing()
        {
            AssessmentSelect.SelectedItem = assessments.assessmentTitle;
            AssessmentCode.Text = assessments.assessmentCode;
            AssessmentDueDatePicker.Date = assessments.anticipatedDueDate.Date;
            AssessmentNotification.IsToggled = assessments.assessmentNotifications;

            base.OnAppearing();
        }

        private async void DeleteAssessment_OnClicked(object sender, EventArgs e)
        {
            if (await DisplayAlert("Confirmation",
                "Are you sure you want to delete the Assessment?", "Yes", "No"))
            {
                await App.Database.DeleteAssessmentsAsync(assessments);
                Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
                await Navigation.PopAsync();
            }
        }
    }
}