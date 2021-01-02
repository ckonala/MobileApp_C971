using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MobileApp_C971_Chakradhar_Konala.Model;
using MobileApp_C971_Chakradhar_Konala.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp_C971_Chakradhar_Konala
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditCoursePage : ContentPage
    {
        private Course course { get; set; }

        public EditCoursePage(Course course)
        {
            InitializeComponent();
            this.course = course;
            BindingContext = new CoursePageViewModel();
        }

        protected override async void OnAppearing()
        {
            CourseInfo.Text = course.courseTitle;
            courseStartDatePicker.Date = course.courseStartDate.Date;
            courseendDatePicker.Date = course.courseEndDate.Date;
            courseSelect.SelectedItem = course.courseStatus;
            CourseInstructor.Text = course.courseInstructor;
            CourseInstructorNumber.Text = course.courseInstructorPhone;
            CourseInstructorEmail.Text = course.courseInstructorEmail;
            CourseNotes.Text = course.courseNotes;
            CourseNotification.IsToggled = course.courseNotifications;

            base.OnAppearing();
        }

        private async void SaveCourse_OnClicked(object sender, EventArgs e)
        {
            course.courseNotifications = CourseNotification.IsToggled;

            if (!(CourseNotes.Text is null))
            {
                course.courseNotes = CourseNotes.Text;
            }

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

                            if (!(CourseInstructor.Text is null) && CourseInstructor.Text.Length > 1)
                            {
                                course.courseInstructor = CourseInstructor.Text;
                                Regex phonepattern = new Regex(@"(?<!\d)\d{10}(?!\d)"); //Phone number Pattern
                                if (!(CourseInstructorNumber.Text is null) && CourseInstructorNumber.Text.Length == 10 && phonepattern.IsMatch(CourseInstructorNumber.Text))
                                {
                                    course.courseInstructorPhone = CourseInstructorNumber.Text;

                                    if (!(CourseInstructorEmail.Text is null) && IsValidEmail(CourseInstructorEmail.Text))
                                    {
                                        course.courseInstructorEmail = CourseInstructorEmail.Text;
                                        await App.Database.UpdateCourseAsync(course);
                                        Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
                                        await Navigation.PopAsync();
                                    }
                                    else
                                    {
                                        await DisplayAlert("Error", "Please ensure Course Instructor email field is not empty and is valid, such as (test@example.com)", "Ok");
                                    }
                                }
                                else
                                {
                                    await DisplayAlert("Error", "Please ensure Course Instructor number field is not empty and has 10 digits", "Ok");
                                }
                            }
                            else
                            {
                                await DisplayAlert("Error", "Please ensure Course Instructor field is not empty", "Ok");
                            }
                        }
                        else
                        {
                            await DisplayAlert("Error", "Please select course status", "Ok");
                        }

                    }
                    else
                    {
                        await DisplayAlert("Error", "End Date should be after the start date", "Ok");
                    }
                }
                else
                {
                    await DisplayAlert("Error", "Start Date should be before the end date", "Ok");
                }
            }
            else
            {
                await DisplayAlert("Error", "Please ensure Course field is not empty", "Ok");
            }
        }

        private bool IsValidEmail(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        private async void DeleteCourse_OnClicked(object sender, EventArgs e)
        {
            if (await DisplayAlert("Confirmation",
                "This will delete course and related assesments. Are you sure?", "Yes", "No"))
            {
                var getAssessmentWithCourseID = await App.Database.GetAssessmentsAsync(course.ID);
                foreach (var assessments in getAssessmentWithCourseID)
                {
                    await App.Database.DeleteAssessmentsAsync(assessments);
                }
                await App.Database.DeleteCourseAsync(course);
                Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
                await Navigation.PopAsync();
            }
        }
    }

}