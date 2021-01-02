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
    public partial class EditTermPage : ContentPage
    {
        private Term term { get; set; }

        public EditTermPage(Term term)
        {
            InitializeComponent();
            this.term = term;
            UpdateTermInfo();
        }

        public void UpdateTermInfo()
        {
            TermInfo.Text = term.termTitle;
            termStartDatePicker.Date = term.startDate;
            termendDatePicker.Date = term.endDate;
        }

        private async void TermInfo_OnCompleted(object sender, EventArgs e)
        {
            if (TermInfo.Text.Length > 1)
            {
                term.termTitle = TermInfo.Text;
                await App.Database.UpdateTermAsync(term);
            }
            else
            {
                await DisplayAlert("Error.", "Please ensure Term field is not empty", "Ok");
            }
        }

        private async void StartDatePicker_OnDateSelected(object sender, DateChangedEventArgs e)
        {
            if (termStartDatePicker.Date != term.startDate)
            {
                if (termStartDatePicker.Date <= termendDatePicker.Date)
                {
                    term.startDate = termStartDatePicker.Date;
                    await App.Database.UpdateTermAsync(term);
                }
                else
                {
                    await DisplayAlert("Error.", "Start Date should be before the end date", "Ok");
                }
            }
        }

        private async void TermendDatePicker_OnDateSelected(object sender, DateChangedEventArgs e)
        {
            if (termendDatePicker.Date != term.endDate)
            {
                if (termendDatePicker.Date >= termStartDatePicker.Date)
                {
                    term.endDate = termendDatePicker.Date;
                    await App.Database.UpdateTermAsync(term);
                }
                else
                {
                    await DisplayAlert("Error.", "End Date should be after the start date", "Ok");
                }
            }
        }


        private async void DeleteTerm_OnClicked(object sender, EventArgs e)
        {
            if (await DisplayAlert("Confirmation",
                "This will delete Term, related courses and assesments. Are you sure?", "Yes", "No"))
            {
                var getCourseWithTermID = await App.Database.GetCourseAsync(term.ID);
                foreach (var course in getCourseWithTermID)
                {
                    var getAssessmentWithCourseID = await App.Database.GetAssessmentsAsync(course.ID);
                    foreach (var assessments in getAssessmentWithCourseID)
                    {
                        await App.Database.DeleteAssessmentsAsync(assessments);
                    }
                    await App.Database.DeleteCourseAsync(course);
                }

                await App.Database.DeleteTermAsync(term);
                await Navigation.PopToRootAsync();
            }
        }
    }
}