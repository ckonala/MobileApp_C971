using MobileApp_C971_Chakradhar_Konala.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.LocalNotifications;
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
                        courseInstructorPhone = "8177275432",
                        courseInstructorEmail = "ckonala@wgu.edu",
                        courseNotes = "",
                        courseNotifications = true,
                        termID = termPrimaryKey
                    };

                    App.Database.CreateCourseTable();
                    await App.Database.InsertCourseAsync(course);

                    var tempcoursePrimaryKey = await App.Database.RetreiveCourseLastRow();
                    var coursePrimaryKey = tempcoursePrimaryKey[0].ID;

                    var objAssessment = new Assessments()
                    {
                        assessmentTitle = "Objective Assessment",
                        assessmentCode = "KVO1",
                        anticipatedDueDate = DateTime.Now.Date.AddDays(15),
                        assessmentNotifications = true,
                        courseID = coursePrimaryKey
                    };

                    var perfAssessment = new Assessments()
                    {
                        assessmentTitle = "Performance Assessment",
                        assessmentCode = "PRT1",
                        anticipatedDueDate = DateTime.Now.Date.AddDays(30),
                        assessmentNotifications = true,
                        courseID = coursePrimaryKey
                    };

                    App.Database.CreateAssessmentsTable();
                    await App.Database.InsertAssessmentsAsync(objAssessment);
                    await App.Database.InsertAssessmentsAsync(perfAssessment);
                }

                PerformDummyInsert = false;
            }

            var _termList = await App.Database.GetTermAsync();
            TermInformation.ItemsSource = _termList;
            var _courseList = await App.Database.GetCourseAsync();
            var _assessmentList = await App.Database.GetAssessmentsAsync();

            int courseCounter = 0;

            foreach (var course in _courseList)
            {
                if (course.courseNotifications)
                {
                    if (course.courseStartDate.Date.Equals(DateTime.Now.Date))
                    {
                        CrossLocalNotifications.Current.Show($"Course Notification",
                            $"{course.courseTitle} Stars Today!",courseCounter);
                        courseCounter++;
                    }
                    else
                    {
                        CrossLocalNotifications.Current.Show($"Course Notification",
                            $"{course.courseTitle} Stars Today!", courseCounter, course.courseStartDate.Date);
                        courseCounter++;
                    }
                    if (course.courseEndDate.Date.Equals(DateTime.Now.Date))
                    {
                        CrossLocalNotifications.Current.Show($"Course Notification",
                            $"{course.courseTitle} Ends Today!",courseCounter);
                        courseCounter++;
                    }
                    else
                    {
                        CrossLocalNotifications.Current.Show($"Course Notification",
                            $"{course.courseTitle} Ends Today!", courseCounter, course.courseEndDate.Date);
                        courseCounter++;
                    }
                }
            }

            foreach (var assessment in _assessmentList)
            {
                if (assessment.assessmentNotifications)
                {
                    if (assessment.anticipatedDueDate.Equals((DateTime.Now.Date)))
                    {
                        CrossLocalNotifications.Current.Show($"Assessment Notification",
                            $"{assessment.assessmentTitle} , {assessment.assessmentCode} is Due Today!", courseCounter);
                        courseCounter++;
                    }
                    else
                    {
                        CrossLocalNotifications.Current.Show($"Assessment Notification",
                            $"{assessment.assessmentTitle} , {assessment.assessmentCode} is Due Today!", courseCounter, assessment.anticipatedDueDate);
                        courseCounter++;
                    }
                }
            }

            base.OnAppearing();
        }

        private async void TermInformation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            await Navigation.PushAsync(new CourseViewPage(e.CurrentSelection.FirstOrDefault() as Term));

        }
    }
}
