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
    public partial class AddCoursePage : ContentPage
    {
        private Term term { get; set; }

        public AddCoursePage(Term term)
        {
            InitializeComponent();
            this.term = term;
            BindingContext = new CoursePageViewModel();
        }

        private async void SaveCourse_OnClicked(object sender, EventArgs e)
        {
            Course course = new Course();
            course.courseNotifications = CourseNotification.IsToggled;

            if (!(CourseInfo.Text is null) && CourseInfo.Text.Length > 1)
            {
                course.courseTitle = CourseInfo.Text;

                if (courseStartDatePicker.Date <= courseendDatePicker.Date)
                {
                    course.courseStartDate = courseStartDatePicker.Date;

                    if (courseendDatePicker.Date >= courseStartDatePicker.Date)
                    {
                        course.courseEndDate = courseendDatePicker.Date;
                        if (!(courseSelect.SelectedItem is null))
                        {
                            course.courseStatus = courseSelect.SelectedItem.ToString();
                            await Navigation.PopAsync();
                        }
                        else
                        {
                            await DisplayAlert("Error.", "Please select course status", "Ok");
                        }
                        
                    }
                    else
                    {
                        await DisplayAlert("Error.", "End Date should be after the start date", "Ok");
                    }
                }
                else
                {
                    await DisplayAlert("Error.", "Start Date should be before the end date", "Ok");
                }
            }
            else
            {
                await DisplayAlert("Error.", "Please ensure Course field is not empty", "Ok");
            }
        }
    }
}