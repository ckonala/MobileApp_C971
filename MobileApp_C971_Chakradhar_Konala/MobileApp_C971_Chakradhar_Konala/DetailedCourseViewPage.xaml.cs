using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileApp_C971_Chakradhar_Konala.Model;
using MobileApp_C971_Chakradhar_Konala.ViewModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp_C971_Chakradhar_Konala
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailedCourseViewPage : ContentPage
    {
        private Course course { get; set; }

        public DetailedCourseViewPage(Course course)
        {
            InitializeComponent();
            this.course = course;
            BindingContext = new CoursePageViewModel();
        }

        protected override async void OnAppearing()
        {
            CourseInfo.Text = course.courseTitle;
            courseStartDatePicker.Text = course.courseStartDate.Date.ToString("MMMM dd yyyy");
            courseendDatePicker.Text = course.courseEndDate.Date.ToString("MMMM dd yyyy"); ;
            courseSelect.Text = course.courseStatus;
            CourseInstructor.Text = course.courseInstructor;
            CourseInstructorNumber.Text = course.courseInstructorPhone;
            CourseInstructorEmail.Text = course.courseInstructorEmail;
            CourseNotes.Text = course.courseNotes;
            CourseNotification.Text = course.courseNotifications ? "Yes" : "No";

            base.OnAppearing();
        }


        private async void EditDeleteCourse_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditCoursePage(course));
        }

        private async void ShareNotes_OnClicked(object sender, EventArgs e)
        {
            if (!(course.courseNotes is null))
            {
                await Share.RequestAsync(new ShareTextRequest
                {
                    Text = course.courseNotes,
                    Title = "Share Notes"
                });
            }
            else
            {
                await DisplayAlert("Error", "Course Notes field is blank. Click on Edit course button and add notes. Please retry again", "Ok");
            }
        }
    }
}